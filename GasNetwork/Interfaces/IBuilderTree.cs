using System.Collections.ObjectModel;

namespace GasNetwork.Interfaces
{
    public interface IBuilderTree
    {
        public IDataProvider? Db { get; }
        public ObservableCollection<Node> Run(string sql);
    }
}