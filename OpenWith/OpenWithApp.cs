using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using OpenWith.Filters;
using OpenWith.Model;
using OpenWith.Services;
using OpenWith.Services.impl;
using OpenWith.ViewModel;

namespace OpenWith
{
    public class OpenWithApp
    {
        private readonly IConfigProvider _configProvider;
        private readonly IDialogService _dialogService;
        private readonly IFilterFactory _filterFactory;
        private readonly IProcessStarter _processStarter;
        private readonly IIconProvider _iconProvider;
        private Config _config = null;

        public OpenWithApp(
            IConfigProvider configProvider, 
            IDialogService dialogService, 
            IFilterFactory filterFactory,
            IProcessStarter processStarter,
            IIconProvider iconProvider
        ) {
            _configProvider = configProvider;
            _dialogService = dialogService;
            _filterFactory = filterFactory;
            _processStarter = processStarter;
            _iconProvider = iconProvider;
        }

        public void Run(string[] args) {
            if (args == null || args.Length < 2) {
                return;
            }

            var target = args[1];
            if (string.IsNullOrEmpty(target)) {
                _dialogService.Warn("Empty target argument");
                return;
            }

            if (_config == null) {
                try {
                    _config = _configProvider.GetConfig();
                } catch (Exception ex) {
                    _dialogService.Error("Failed reading config:\n" + ex);
                    return;
                }
            }

            if (_config?.Mappings == null || !_config.Mappings.Any()) {
                _dialogService.Info("No mappings in config");
                return;
            }

            List<Mapping> matchedMappings;
            try {
                matchedMappings =
                        _config.Mappings
                               .Where(m =>
                                              m.Filters != null
                                              && m.Filters
                                                  .Select(f => _filterFactory.GetFilter(f.Key, f.Value))
                                                  .Where(f => f != null)
                                                  .All(f => f.Accepts(target))
                               )
                               .OrderBy(m => m.Priority).ThenBy(m => m.Name)
                               .Take(10)
                               .ToList();

                if (!matchedMappings.Any()) {
                    matchedMappings =
                            _config.Mappings
                                   .Where(m => m.Filters == null)
                                   .OrderBy(m => m.Priority).ThenBy(m => m.Name)
                                   .Take(10)
                                   .ToList();
                }
            }
            catch (Exception ex) {
                _dialogService.Error(ex.ToString());
                return;
            }

            if (!matchedMappings.Any()) {
                return;
            }

            var stringExpander = new VarsStringExpander(_config.Vars);

            bool StartMapping(Mapping mapping) {
                if (mapping != null) {
                    var command = stringExpander.Expand(mapping.Command);
                    try {
                        _processStarter.Start(
                            command,
                            mapping.Admin,
                            Path.GetDirectoryName(target),
                            stringExpander.Expand(mapping.Args),
                            args
                        );
                        return true;
                    }
                    catch (Exception ex) {
                        _dialogService.Error($"Error starting '{command}':\n" + ex);
                    }
                }
                return false;
            }

            if (matchedMappings.Count == 1) {
                var mapping = matchedMappings.First();
                StartMapping(mapping);
                return;
            }

            MainWindow mainWindow = null;
            mainWindow = new MainWindow {
                AllowsTransparency = _config.Opacity < 1.0,
                Opacity = _config.Opacity,
                DataContext = new MainViewModel {
                    Theme = _config.Theme,
                    HeaderText = string.Format(_config.HeaderTemplate, target, Path.GetFileName(target)),
                    Items = matchedMappings.Select((m, i) => new OptionViewModel {
                        Number = i+1,
                        Name = m.Name,
                        IconSupplier = () => _iconProvider.GetIcon(stringExpander.Expand(m.Icon ?? m.Command)).Result,
                        Action = () => { if (StartMapping(m)) mainWindow.Close(); }
                    }).ToList(),
                    CloseTimeout = _config.AutoCloseTimeout*1000,
                    CloseAction = () => mainWindow.Close()
                }
            };
            
            var app = new Application {
                ShutdownMode = ShutdownMode.OnMainWindowClose,
                MainWindow = mainWindow
            };
            
            mainWindow.Show();
            app.Run();
        }
    }
}
