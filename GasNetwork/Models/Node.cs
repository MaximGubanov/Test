using System.Collections.ObjectModel;
using System.Linq;

namespace GasNetwork.Models
{
    // Сомневаюсь что это модель вряд ли в терминах газорасределения есть такое понятие как узел дерева.
    // 22 ссылки на объект это повод задуматься очень высокая зацепленность и кандидат на божественность.
    public class Node : NotifyPropertyChanged 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ENodeType Type { get; set; } // тип узла в дереве
        public string? Path { get; set; }
        public int Level { get; set; }
        public int Event { get; set; }
        public string? IconPath { get; set; }
        public Node? Parent { get; set; }
        private bool OrderStateDescending { get; set; } = true;
        private ObservableCollection<Node>? _nodes;
        public ObservableCollection<Node>? Nodes
        {
            get => _nodes;
            set
            {
                _nodes = value;
                OnPropertyChanged();
            }
        }

        public void AddNewNode(string name)
        {
            Nodes?.Add(new Node { Id = 1888, Name = name, Nodes = new ObservableCollection<Node>(), Parent = this });
        }

        public void RemoveNode(int id)
        {
            var removable = Parent?.Nodes?.First(x => x.Id == id);

            if (removable != null) 
                Parent?.Nodes?.Remove(removable);
        }

        public void SortByName(ObservableCollection<Node> sortable)
        {
            if (sortable is not null)
            {
                if (OrderStateDescending)
                {
                    Nodes = new ObservableCollection<Node>(sortable.OrderByDescending(node => node.Name).ToList());
                    OrderStateDescending = false;
                }
                else
                {
                    Nodes = new ObservableCollection<Node>(sortable.OrderBy(node => node.Name).ToList());    
                    OrderStateDescending = true;
                }
            }
        }
    }
}