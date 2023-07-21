using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace GasNetwork.Views
{
    public partial class MenuBarView : UserControl
    {
        public MenuBarView()
        {
            InitializeComponent();
        }

        private void ShowApplicationCloseDialog(object sender, RoutedEventArgs e)
        {
            //ApplicationCloseDialog dialogWindow = new ();

            //if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            //{
            //    dialogWindow.ShowDialog(desktop.MainWindow);
            //}
        }
        
    }
}
