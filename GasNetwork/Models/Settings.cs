using System.Text.Json.Serialization;
using System;
using System.IO;
using System.Text.Json;
using GasNetwork.Extensions;
using System.Diagnostics;
using System.Collections.Generic;

namespace GasNetwork.Models
{
    // с каких это пор настройки приложения стали частью модели?
    public class Settings : NotifyPropertyChanged, ISettings
    {
        public string? Server { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }

        private string? _sgsServerPath;
        public string? SGSServerPath 
        {
            get => _sgsServerPath;
            set => _sgsServerPath = value;
        }

        private string? _tmrServerPath;
        public string? TMRServerPath
        {
            get => _tmrServerPath;
            set => _tmrServerPath = value;
        }

        public string? LocationDbSGSlocal { get; set; }
        public string? LocationDbTMRlocal { get; set; }
        public string? DefaultStartTree { get; set; }

        public void Load() 
        {
            try
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<Settings>(json)!;
                settings.CopyPropertiesTo(this);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public void Save() 
        {
            if (File.Exists(SettingsFilePath))
            {
                File.Copy(SettingsFilePath,
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    $"{DateTime.Now.ToString("d MMM yyyy~HHч mmмин ssсек")}-settings.txt"));
            }

            if (!File.Exists(SettingsFilePath)) File.Create(SettingsFilePath);

            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, json);
        }

        private string? cachedSettingsFilePath;

        [JsonIgnore]
        public string SettingsFilePath
        {
            get
            {
                return cachedSettingsFilePath ??= Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "settings.txt");
            }
        }
    }
}
