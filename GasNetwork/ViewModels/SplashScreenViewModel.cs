using ReactiveUI;
using System.Threading;

namespace GasNetwork.ViewModels
{
    public class SplashScreenViewModel : ReactiveObject
    {
        public string StartupMessage
        {
            get => _startupMessage;
            set { this.RaiseAndSetIfChanged(ref _startupMessage, value); }
        }
        private string _startupMessage = "Запуск приложения...";

        public void Cancel()
        {
            StartupMessage = "Cancelling...";
            _cts.Cancel();
        }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public CancellationToken CancellationToken => _cts.Token;
    }
}