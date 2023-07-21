namespace GasNetwork.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MenuDataContext MenuDataContext { get; set; } /// TODO: убрать из этой VM
        public TreeNodeViewModel? TreeNode { get; set; }
        public BaseContentViewModel? BaseContentVM { get; set; }
        
        //TODO: возможно нужно убрать FlowViewModel и MeteringUnitViewModel
        public FlowViewModel? FlowVM { get; set; }
        public MeteringUnitViewModel? MeteringUnitVM { get; set; }

        public MainWindowViewModel(
            TreeNodeViewModel treeNode,
            BaseContentViewModel baseContent)
        {
            MenuDataContext = new MenuDataContext(); // TODO: убрать из этой MainWindowViewModel
            TreeNode = treeNode;
            BaseContentVM = baseContent;
        }
    }
}