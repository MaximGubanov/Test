using System.Collections.Generic;

namespace GasNetwork.Models
{
    public class Tab
    {
        public string? Header { get; set; }
        public object? Content { get; set; }

        public static List<Tab> CreateTabList(List<IViewModel> viewModelList, List<string> tabList)
        {
            // Не понятен смысл это метода. Есди вызывыющас сторона имеет список VM и знает какие типы VM ей нужны
            // Нафига ей делегировать операцию по фильтарции кому-то?
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