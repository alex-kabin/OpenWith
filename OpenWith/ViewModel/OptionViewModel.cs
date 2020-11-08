using System;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors.Core;

namespace OpenWith.ViewModel
{
    public class OptionViewModel
    {
        private static readonly Key[] Keys = { Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.D0 };

        private int _number;
        public int Number {
            get => _number;
            set {
                _number = value;
                Key = _number < 1 || _number >= Keys.Length ? Key.None : Keys[_number - 1];
            }
        }

        public Key Key { get; private set; }

        public string Name { get; set; }
        
        public ICommand Command { get; private set; }
        
        public Action Action { 
            set => Command = new ActionCommand(value);
        }

        public Func<ImageSource> IconSupplier { private get; set; }

        private ImageSource _icon;
        public ImageSource Icon {
            get {
                if (_icon == null && IconSupplier != null) {
                    _icon = IconSupplier();
                    _icon?.Freeze();
                }
                return _icon;
            }
        }
    }
}
