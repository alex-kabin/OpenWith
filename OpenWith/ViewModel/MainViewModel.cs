using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using OpenWith.Model;

namespace OpenWith.ViewModel
{
    public class MainViewModel
    {
        public IReadOnlyList<OptionViewModel> Items { get; set; }

        public Theme Theme { get; set; }
        
        public string HeaderText { get; set; }

        public int CloseTimeout { get; set; }

        public ICommand CloseCommand { get; private set; }

        public Action CloseAction {
            set => CloseCommand = new ActionCommand(value);
        }
    }
}
