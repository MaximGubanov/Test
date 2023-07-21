using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GasNetwork.Models
{
    public abstract class Tree
    {
        private protected abstract string? SQL { get; set; }
        public abstract string? DatabasePath { get; set; }
        public abstract string? Name { get; set; }
        public abstract EDBName DbName { get; set; }
        public abstract List<string>? TabList { get; set; }
        public ObservableCollection<Node>? Nodes { get; set; }
        private protected IBuilderTree? builder;
        private protected ISettings? _settings;

        public Tree(IBuilderTree builder, ISettings settings)
        {
            this.builder = builder;
            this._settings = settings;
        }

        public void Build()
        {
            if (SQL != null) Nodes = builder?.Run(SQL);
        }

        public Tree BecomeTheCurrentTree()
        {
            DataProviderService.SetConnectionString(this);
            return this;
        }
    }

    public class SGSTree : Tree
    {
        private protected override string? SQL { get; set; } =
            "SELECT NODETYPE as Type, OBJECTID as Id, NODENAME as Name, NODEPATH as Path, NODELEVEL as Level " +
            "FROM sgs_tree_node_order " +
            "ORDER BY NODEPATH ASC, NODELEVEL ASC;";
        public override string? DatabasePath { get; set; }
        public override string? Name { get; set; } = "SGS";
        public override EDBName DbName { get; set; } = EDBName.SGS;
        public override List<string>? TabList { get; set; }

        public SGSTree(IBuilderTree builder, ISettings settings) : base(builder, settings)
        {
            DatabasePath = _settings?.SGSServerPath;

            TabList = new List<string>
            {
                nameof(MeteringUnitViewModel),
                nameof(ConsumptionViewModel),
                nameof(ArchivesViewModel),
                nameof(DataComletenessViewModel),
                nameof(ConsumerViewModel),
                nameof(ComplexViewModel),
                nameof(MeterViewModel),
                nameof(MeteringDeviceViewModel),
                nameof(DeviceParamsViewModel),
                nameof(SurveyViewModel)
            };
        }
    }

    public class TMRTree : Tree
    {
        private protected override string? SQL { get; set; } =
            "SELECT NODETYPE as Type, OBJECTID as Id, NODENAME as Name, NODEPATH as Path, NODELEVEL as Level " +
            "FROM tmr_tree_node_order " +
            "ORDER BY NODEPATH ASC, NODELEVEL ASC;";
        public override string? DatabasePath { get; set; }
        public override string? Name { get; set; } = "TMR";
        public override EDBName DbName { get; set; } = EDBName.TMR;
        public override List<string>? TabList { get; set; }

        public TMRTree(IBuilderTree builder, ISettings settings) : base(builder, settings)
        {
            DatabasePath = _settings?.TMRServerPath;

            TabList = new List<string>
            {
                nameof(MeteringUnitViewModel),
                nameof(ConsumptionViewModel),
                nameof(ArchivesViewModel),
                nameof(ConsumerViewModel),
                nameof(MeterViewModel),
                nameof(SurveyViewModel)
            };
        }
    }
}