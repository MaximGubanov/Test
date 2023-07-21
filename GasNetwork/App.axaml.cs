using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GasNetwork.Views;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace GasNetwork
{
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; } = new();
        public static ISettings Settings => ServiceProvider!.GetService<ISettings>();
        public static Window MainWindow => ((ClassicDesktopStyleApplicationLifetime)Current!.ApplicationLifetime!).MainWindow!;

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override async void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var splashScreenVM = new SplashScreenViewModel();
                var splashScreen = new SplashScreen
                {
                    DataContext = splashScreenVM
                };

                desktop.MainWindow = splashScreen;
                splashScreen.Show();
                try
                {
                    splashScreenVM.StartupMessage = "Запуск приложения...";
                    await Task.Delay(1, splashScreenVM.CancellationToken);
                    /*
                    splashScreenVM.StartupMessage = "Searching for devices...";
                    await Task.Delay(1000, splashScreenVM.CancellationToken);
                    splashScreenVM.StartupMessage = "Connecting to device #1...";
                    await Task.Delay(2000, splashScreenVM.CancellationToken);
                    splashScreenVM.StartupMessage = "Configuring device...";
                    await Task.Delay(2000, splashScreenVM.CancellationToken);
                    */
                }
                catch (TaskCanceledException)
                {
                    splashScreen.Close();
                    return;
                }

                Settings.Load();

                var mainWindow = ServiceProvider?.GetService<MainWindow>();

                if(mainWindow != null)
                    mainWindow.DataContext = ServiceProvider?.GetService<MainWindowViewModel>();
                
                desktop.MainWindow = mainWindow;
                mainWindow?.Show();
                splashScreen.Close();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}