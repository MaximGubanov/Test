using System.Collections.Generic;

namespace GasNetwork.ViewModels
{
    public class FlowViewModel : ViewModelBase
    {
        private List<Flow>? _data;
        public List<Flow>? Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        public TreeNodeViewModel? TreeNodeVM { get; set; }

        public FlowViewModel(TreeNodeViewModel treeNodeVM)
        {
            TreeNodeVM = treeNodeVM;

            TreeNodeVM.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TreeNodeVM.Device))
                {
                    Data = TreeNodeVM.Device?.FlowList;
                }

                if (args.PropertyName == nameof(TreeNodeViewModel.CurrentSelectedTree))
                {
                    if (TreeNodeVM.CurrentSelectedTree?.Name != "SGS")
                    {
                        Data = null;
                    }
                }
            };
        }

        public delegate void FlowViewModelDelegate(Flow flow);
        public event FlowViewModelDelegate? OnFlowChangedEvent;
        private void ClickFlowButton(Flow flow)
        { 
            OnFlowChangedEvent?.Invoke(flow);
        }      
    }
}