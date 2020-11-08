using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace OpenWith
{
    public partial class MainWindow
    {
        public MainWindow() {
            InitializeComponent();
        }
        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            Debug.WriteLine("MainWindow::OnLoaded at thread "+Thread.CurrentThread.ManagedThreadId);
        }
    }
}
