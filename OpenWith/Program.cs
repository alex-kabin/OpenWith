using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using OpenWith.Filters.impl;
using OpenWith.Services;
using OpenWith.Services.impl;

namespace OpenWith
{
    public static class Program
    {
        [STAThread]
        public static void Main() {
            var assembly = Assembly.GetExecutingAssembly();

            IDialogService dialogService = new MessageBoxDialogService(
                assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title
            );

            var dir = Path.GetDirectoryName(assembly.Location);

            IConfigProvider configProvider = null;
            var yamlFile = Path.Combine(dir, "OpenWith.yaml");
            if (File.Exists(yamlFile)) {
                configProvider = new YamlConfigProvider(yamlFile);
            } else {
                var jsonFile = Path.Combine(dir, "OpenWith.json");
                if (File.Exists(jsonFile)) {
                    configProvider = new JsonConfigProvider(jsonFile);
                }
            }
            if (configProvider == null) {
                dialogService.Error("Config not found");
                return;
            }

            var app = new OpenWithApp(
                configProvider,
                dialogService,
                new FilterFactory()
                        .WithFilter("name", NameFilter.Build)
                        .WithFilter("ext", ExtensionFilter.Build)
                        .WithFilter("size", SizeFilter.Build)
                        .WithFilter("magic", MagicBytesFilter.Build),
                new DefaultProcessStarter(),
                new FileIconProvider()
            );

            //Stopwatch stopwatch = Stopwatch.StartNew();

            var args = Environment.GetCommandLineArgs();
            app.Run(args);

            //File.AppendAllText(Path.Combine(dir, "Stat.txt"), $"{DateTime.Now} : {args[1]} : {stopwatch.ElapsedMilliseconds}ms\n");
        }
    }
}
