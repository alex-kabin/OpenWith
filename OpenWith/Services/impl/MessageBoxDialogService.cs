using System.Windows;

namespace OpenWith.Services.impl
{
    public class MessageBoxDialogService : IDialogService
    {
        private readonly string _prefix;

        public MessageBoxDialogService(string prefix = null) {
            _prefix = prefix;
        }

        private string FormatCaption(string caption) {
            return string.IsNullOrWhiteSpace(_prefix) ? caption : $"{_prefix} : {caption}";
        }

        public void Info(string text, string caption) {
            MessageBox.Show(text, FormatCaption(caption), MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        public void Warn(string text, string caption) {
            MessageBox.Show(text, FormatCaption(caption), MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        
        public void Error(string text, string caption) {
            MessageBox.Show(text, FormatCaption(caption), MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
