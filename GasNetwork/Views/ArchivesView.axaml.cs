using Avalonia.Controls;

namespace GasNetwork.Views;

public partial class ArchivesView : UserControl
{
    public ArchivesView()
    {
        InitializeComponent();
        //TODO: временная заглушка для выпадающего списка
        RangeTemplates.Items = new[]
        {
            "Этот час", "Сегодня", "Эта неделя", "Этот месяц", "Этот год",
            "Прошлый час", "Вчера", "Прошлая неделя", "Прошлый месяц", "Прошлый год",
            "Период как в \"Потреблении\""
        };
    }
}