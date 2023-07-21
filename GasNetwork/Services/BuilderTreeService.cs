﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GasNetwork.Services
{
    public class BuilderTreeService : Node, IBuilderTree
    {
        private static List<Node>? DataFromDb { get; set; }
        public IDataProvider? Db { get; }
        public BuilderTreeService(IDataProvider db) => Db = db;

        // сервисы не должны возвращать ObservableCollection  это подразумеват что у сервиса может быть состояние
        // а StateFull сервисы это не часто не хорошо,
        // а в конкретном случае вообне нет смысла в ObservableCollection если кому-то надо следить за измененеим это коллекиции пусть сам 
        // оборачивает ее в ObservableCollection
        public ObservableCollection<Node> Run(string sql)
        {
            DataFromDb = Db?.ExecuteDataAsync<Node>(sql).Result;
            Nodes = new ObservableCollection<Node>();

            DataFromDb?.ForEach(row =>
            {
                int startLevel = 0;

                if (row.Level == startLevel)
                {
                    row.Parent = this;
                    row.Type = ENodeType.Consumer;
                    row.IconPath = "/Assets/img/1.png"; // бизнес слой вообзе не должен ничего занть о файловаой стркутуре вашего приложения
                    // или это должн инкапсулироваться в класс Nore или вообще разрешатья на вышестоящих уровнях
                    FillChildNode(row);
                    Nodes?.Add(row);
                }
            });

            if (Nodes != null) //делаем сортировку по умолчанию, по возрастанию
                Nodes = new ObservableCollection<Node>(Nodes.OrderBy(node => node.Name).ToList());

            return Nodes ?? new ObservableCollection<Node>();
        }

        private void FillChildNode(Node node)
        {
            int level = node.Level + 1;
            var childNodes = DataFromDb?.FindAll(child => child.Level == level && child.Path.StartsWith(node.Path));

            if (childNodes?.Count != 0 && childNodes is not null)
            {
                node.Nodes = new ObservableCollection<Node>(childNodes);

                foreach (Node n in node.Nodes)
                {
                    n.Parent = node;

                    if (!n.Path.Contains("d:"))
                    {
                        n.Type = ENodeType.Consumer;
                        n.IconPath = "/Assets/img/1.png";
                        FillChildNode(n);
                    }
                    else
                    {
                        n.Type = ENodeType.Device;
                        n.IconPath = "/Assets/img/3.png";
                    }
                }
            }
        }
    }
}