using System.Collections.Generic;

namespace GasNetwork.Models
{
    public class Tab
    {
        public string? Header { get; set; }
        public object? Content { get; set; }

        public static List<Tab> CreateTabList(List<IViewModel> viewModelList, List<string> tabList)
        {
            List<Tab> tabs = new();

            foreach (var vm in viewModelList)
            {
                if (tabList.Contains(vm.GetType().Name))
                    tabs.Add(new Tab() { Header = vm.Name, Content = vm });
            }

            return tabs;
        }
    }
}