using System.Collections.Generic;

namespace GasNetwork.ViewModels
{
    public class DeviceParamsViewModel : ViewModelBase, IViewModel
    {
        public string Name { get; set; } = "Параметры прибора";

        private List<IDataGridRepresenter>? _data;
        public List<IDataGridRepresenter>? Data 
        { 
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        public TreeNodeViewModel? TreeNodeVM { get; set; }
        private FlowViewModel? FlowVM { get; set; }

        public DeviceParamsViewModel(TreeNodeViewModel treeNodeVM, FlowViewModel flowVM) 
        {
            TreeNodeVM = treeNodeVM;
            FlowVM = flowVM;

            TreeNodeVM.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TreeNodeVM.Device))
                {
                    int id = int.Parse(TreeNodeVM.Device!.Id.ToString());
                    Data = new List<IDataGridRepresenter>(DeviceRepository
                        .RetrieveDeviceParameterAsync(new Flow { ParentId = id, FlowNumber = 1 }).Result);
                }
            };

            FlowVM.OnFlowChangedEvent += (flow) => 
            {
                Data = new List<IDataGridRepresenter>(DeviceRepository.RetrieveDeviceParameterAsync(flow).Result);
            };
        }
    }
}