namespace GasNetwork.ViewModels
{
    public class ConsumerViewModel : ViewModelBase, IViewModel
    {
        public string Name { get; set; } = "Потребитель";

        private Consumer? _data;
        public Consumer? Data
        {
            get => _data;
            set 
            { 
                _data = value; 
                OnPropertyChanged(); 
            }
        }

        private ConsumerService? ConsumerService { get; set; }
        public TreeNodeViewModel? TreeNodeVM { get; set; }

        public ConsumerViewModel(ConsumerService consumerService, TreeNodeViewModel treeNodeVM)
        {
            ConsumerService = consumerService;
            TreeNodeVM = treeNodeVM;

            TreeNodeVM.PropertyChanged += (sender, args) =>
            {
                var selectedNode = TreeNodeVM.SelectedNode;

                if (args.PropertyName == nameof(TreeNodeVM.SelectedNode) && selectedNode != null)
                {
                    if (selectedNode.Type == ENodeType.Consumer)
                        Data = ConsumerService.GetData(selectedNode.Id);

                    if (selectedNode.Type == ENodeType.Device && selectedNode.Parent != null)
                        Data = ConsumerService.GetData(selectedNode.Parent.Id);
                }
            };

            // обнуление текущих данных ConsumerViewModel при переходе на другое дерево
            //TreeNodeVM.OnTreeNodeChangedEvent += () => { Data = null; };
        }
    }
}