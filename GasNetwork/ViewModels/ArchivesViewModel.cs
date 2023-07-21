using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GasNetwork.ViewModels
{
    public class ArchivesViewModel : ViewModelBase, IViewModel
    {
        private List<Archive>? _archiveButtonList;
        public List<Archive>? ArchiveButtonList
        {
            get => _archiveButtonList;
            set
            {
                _archiveButtonList = value;
                OnPropertyChanged();
            }
        }

        private List<Archive>? _dataFromArchive;
        public List<Archive>? DataFromArchive
        {
            get => _dataFromArchive;
            set
            {
                _dataFromArchive = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date = DateTime.Now.AddDays(-1.0);
        public DateTime StartDatePicker
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endDatePicker = DateTime.Now;
        public DateTime EndDatePicker
        {
            get => _endDatePicker;
            set
            {
                _endDatePicker = value.Hour != 0 || value.Minute != 0 || value.Second != 0 ?
                    value : value.AddHours(23).AddMinutes(59).AddSeconds(59.99);
            }
        }

        public TreeNodeViewModel? TreeNodeVM { get; set; }
        public string Name { get; set; } = "Архивы";

        public ArchivesViewModel(TreeNodeViewModel treeNode)
        {
            TreeNodeVM = treeNode;

            TreeNodeVM.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TreeNodeViewModel.Device))
                {
                    ArchiveButtonList = TreeNodeVM.Device?.ArchiveButtonList;
                }
            };
        }

        private async void ClickArchiveButton(IArchive archiveObject)
        {
            await Task.Run(() =>
            {
                DataFromArchive = archiveObject.GetDataAsync(StartDatePicker, EndDatePicker).Result;
            });
        }
    }
}