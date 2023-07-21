using System.Collections.Generic;

namespace GasNetwork.ViewModels
{
    public class BaseContentViewModel : ViewModelBase
    {
        public FlowViewModel? FlowVM { get; set; }
        public TreeNodeViewModel? TreeNodeVM { get; set; }
        public MeteringUnitViewModel? MeteringUnitVM { get; set; }
        public ConsumptionViewModel? ConsumptionVM { get; set; }
        public ArchivesViewModel? ArchivesVM { get; set; }
        public DataComletenessViewModel? DataComletenessVM { get; set; }

        private List<Tab>? _tabs;
        public List<Tab>? Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        public BaseContentViewModel(
            FlowViewModel flowVM,
            TreeNodeViewModel treeNodeVM,
            MeteringUnitViewModel meteringUnitVM,
            ConsumptionViewModel consumptionVM,
            ArchivesViewModel archivesVM,
            DataComletenessViewModel dataCompletenessVM)
        {
            FlowVM = flowVM;
            TreeNodeVM = treeNodeVM;
            MeteringUnitVM = meteringUnitVM;
            ConsumptionVM = consumptionVM;
            ArchivesVM = archivesVM;
            DataComletenessVM = dataCompletenessVM;

            Tabs = Tab.CreateTabList(
                new List<IViewModel> { MeteringUnitVM, ConsumptionVM, ArchivesVM, DataComletenessVM },
                TreeNodeVM.CurrentSelectedTree?.TabList!);

            TreeNodeVM.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TreeNodeVM.CurrentSelectedTree))
                {
                    if (TreeNodeVM.CurrentSelectedTree?.TabList != null)
                    {
                        Tabs = Tab.CreateTabList(
                            new List<IViewModel> { MeteringUnitVM, ConsumptionVM, ArchivesVM, DataComletenessVM },
                            TreeNodeVM.CurrentSelectedTree?.TabList!);
                    }
                }
            };
        }
    }
}