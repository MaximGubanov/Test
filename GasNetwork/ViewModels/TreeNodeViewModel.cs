namespace GasNetwork.ViewModels
{
    public class TreeNodeViewModel : ViewModelBase
    {
        public SGSTree? SGSTree { get; set; }
        public TMRTree? TMRTree { get; set; }
        
        private Tree? _currentSelectedTree;
        public Tree? CurrentSelectedTree
        {
            get => _currentSelectedTree;
            set
            {
                _currentSelectedTree = value;
                OnPropertyChanged();
                OnSelectedTreeNodeChanged();
            }
        }
        
        private Node? _selectedNode;
        public Node? SelectedNode
        {
            get => _selectedNode;
            set
            {
                _selectedNode = value;
                OnPropertyChanged();
            }
        }

        private Device? _device;
        public Device? Device
        {
            get => _device;
            set
            {
                _device = value;
                OnPropertyChanged();
            }
        }

        public TreeNodeViewModel(SGSTree sgsTreeNode, TMRTree tmrTreeNode)
        {
            TMRTree = tmrTreeNode;
            MakeCurrentTreeNode(TMRTree);
            TMRTree?.Build();
            
            SGSTree = sgsTreeNode;
            MakeCurrentTreeNode(SGSTree);
            SGSTree?.Build();

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TreeNodeViewModel.SelectedNode))
                {
                    if (this.SelectedNode != null)
                    {
                        // сдесь явно нужена еще одна абстракция ViewModel не должна разбиратться в типах кторве есть в домене
                        // те или надо создать специальную ViewModel которая обслуживате только тип Device
                        // или сделать ApplicationService
                        // или на сам Device возложить обязанности то чтению

                        if (this.SelectedNode.Type == ENodeType.Device)
                        {
                            Device = DeviceRepository.RetrievAsync(this.SelectedNode.Id).Result;
                        }

                        if (this.SelectedNode.Type == ENodeType.Consumer)
                            Device = null;
                    }
                }
            };
        }

        private void MakeCurrentTreeNode(Tree treeNode)
        {
            this.CurrentSelectedTree = treeNode.BecomeTheCurrentTree();
        }

        public delegate void TreeNodeDelegate(TreeNodeViewModel treeNodeVM);
        public event TreeNodeDelegate? OnSelectedTreeNodeEvent;
        public void OnSelectedTreeNodeChanged()
        {
            OnSelectedTreeNodeEvent?.Invoke(this);
        }
    }
}