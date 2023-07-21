using System.ComponentModel;

namespace GasNetwork.Interfaces
{
    public interface ISettings : INotifyPropertyChanged
    {
        public string? Server { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? SGSServerPath { get; set; }
        public string? TMRServerPath { get; set; }
        public string? LocationDbSGSlocal { get; set; }
        public string? LocationDbTMRlocal { get; set; }
        public string? DefaultStartTree { get; set; }

        void Save();

        void Load();
    }
}
