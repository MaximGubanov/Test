using BenchmarkDotNet.Attributes;
using GasNetwork.Models;
using System.Collections.ObjectModel;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class TreeBenchmark
    {
        private static readonly DataProvider db = new DataProvider();

        private static readonly string sql = 
            "SELECT " +
                "NODETYPE as Type, " +
                "OBJECTID as Id, " +
                "NODENAME as Name, " +
                "NODEPATH as Path, " +
                "NODELEVEL as Level " +
            "FROM sgs_tree_node_order";

        private static readonly IEnumerable<Node> data = db.ExecuteData<Node>(sql);
        private static ObservableCollection<Node>? _filler = new ObservableCollection<Node>();
        
        [Benchmark]
        public void TreeBuildTest()
        {
            _filler = new BuilderTree(data).StartFilling();
        }

        //[Benchmark]
        public void GetDataFromDBTest()
        {
            db.ExecuteData<Node>(sql);
        }

        [Benchmark]
        public void SortRootNodeTest()
        {
            new ContentVM().SortRootNode(_filler);
        }
    }
}