using System.Collections.Generic;

namespace GasNetwork.ViewModels
{
    public class MeteringUnitViewModel : ViewModelBase, IViewModel
    {
        public ConsumerViewModel? ConsumerVM { get; set; }
        public ComplexViewModel? ComplexVM { get; set; }
        public MeterViewModel? MeterVM { get; set; }
        public MeteringDeviceViewModel? MeteringDeviceVM { get; set; }
        public DeviceParamsViewModel? DeviceParamVM { get; set; }
        public SurveyViewModel? SurveyVM { get; set; }
        public string Name { get; set; } = "Узел учёта";

        private List<Tab>? _tabs;
        public List<Tab>? Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        public TreeNodeViewModel? TreeNodeVM { get; set; }

        public MeteringUnitViewModel(
            TreeNodeViewModel treeNodeVM,
            ConsumerViewModel consumerVM,
            ComplexViewModel complexVM,
            MeterViewModel meterVM,
            MeteringDeviceViewModel meteringDeviceVM,
            DeviceParamsViewModel devParamVM,
            SurveyViewModel surveyVM)
        {
            TreeNodeVM = treeNodeVM;
            ConsumerVM = consumerVM;
            ComplexVM = complexVM;
            MeterVM = meterVM;
            MeteringDeviceVM = meteringDeviceVM;
            DeviceParamVM = devParamVM;
            SurveyVM = surveyVM;

            Tabs = Tab.CreateTabList(
                new List<IViewModel> { ConsumerVM!, ComplexVM!, MeterVM!, MeteringDeviceVM!, DeviceParamVM!, SurveyVM!},
                TreeNodeVM.CurrentSelectedTree?.TabList!);

            TreeNodeVM.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TreeNodeViewModel.CurrentSelectedTree))
                {
                    if (TreeNodeVM.CurrentSelectedTree?.TabList != null)
                    {
                        Tabs = Tab.CreateTabList(
                            new List<IViewModel> { ConsumerVM!, ComplexVM!, MeterVM!, MeteringDeviceVM!, DeviceParamVM!, SurveyVM! },
                            TreeNodeVM.CurrentSelectedTree?.TabList!);
                    }
                }
            };
        }
    }
}