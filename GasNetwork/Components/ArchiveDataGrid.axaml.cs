using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Media.Imaging;
using GasNetwork.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GasNetwork.Components;
//TODO: код не универсальный, разная ответственность, нужно сделать DataGrid универсальным,
//так чтобы класс принимал любой объект для построения DataGrid, а так же сделать возможным
//настройку колонок
public partial class ArchiveDataGrid : UserControl
{
    public static readonly StyledProperty<List<Archive>> DataProperty =
        AvaloniaProperty.Register<ArchiveDataGrid, List<Archive>>(
            nameof(Data),
            defaultValue: new List<Archive>());

    public List<Archive> Data
    {
        get => GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    private UserControl? RootControl { get; set; }
    private Dictionary<string, string>? ColumnName { get; set; }

    public ArchiveDataGrid()
    {
        InitializeComponent();
        RootControl = Root;
        ColumnName = new DataGridColumn().Dictonary;
        DataProperty.Changed.Subscribe(x => OnDataChanging(x.Sender, x.NewValue.GetValueOrDefault<List<Archive>>()!));
    }

    private void OnDataChanging(IAvaloniaObject o, List<Archive> archives)
    {
        DataGrid dataGrid = new();
        var startDate = (DataContext as ArchivesViewModel)?.StartDatePicker;
        var endDate = (DataContext as ArchivesViewModel)?.EndDatePicker;

        try
        {
            if (startDate >= endDate) throw new Exception("Начальная дата не может быть больше конечной даты");

            //для исследования типа достаточно взять только 1-ю запись из БД
            var reflectedTypeOfArchive = archives.Count > 0 ?
                archives?[0].GetType() : throw new Exception("Не найдено данных за выбранный период!");

            var props = reflectedTypeOfArchive?.GetProperties(); //получаю все св-ва типа
            string valueFromDict;

            if (props != null)
                foreach (var prop in props)
                { 
                    if (ColumnName!.TryGetValue(prop.Name, out valueFromDict!))
                    {
                        if (valueFromDict.EndsWith("Icon"))
                        {
                            var templateColumn = new DataGridTemplateColumn();
                            var dataTemplate = new FuncDataTemplate<EventArchive>((value, namescope) =>
                            {
                                var pathIcon = value.IconPath?.ConvetToIconPath();
                                var img = new Bitmap(pathIcon);

                                return new Image() { Source = img, Width = 18 };
                            });
                            templateColumn.CellTemplate = dataTemplate;
                            dataGrid.Columns.Add(templateColumn);
                        } 
                        else if (valueFromDict == "Статус замка")
                        {
                            var templateColumn = new DataGridTemplateColumn();
                            var dataTemplate = new FuncDataTemplate<ChangesArchive>((value, namescope) =>
                            {
                                var convertedValue = value.StatusLock.ConvertStatusLockToString("Flowsic");
                                return new TextBlock() { Text = convertedValue.ToString() };
                            });
                            templateColumn.CellTemplate = dataTemplate;
                            templateColumn.Header = valueFromDict;
                            dataGrid.Columns.Add(templateColumn);
                        }
                        else
                        {
                            var textColumn = new DataGridTextColumn();
                            textColumn.Header = $"{ColumnName[prop.Name]}";
                            textColumn.Binding = new Binding($"{prop.Name}");
                            dataGrid.Columns.Add(textColumn);
                        }
                    }
                }
            dataGrid.Items = Data;

            RootControl!.Content = Data?.Count > 0 ?
                dataGrid : new TextBlock() { Text = "Не найдено данных за выбранный период!" };

            dataGrid.IsVisible = Data?.Count > 0 ?
                true : false;
        }
        catch (Exception ex)
        {
            RootControl!.Content = new TextBlock() { Text = ex.Message };
        }
    }
}

//TODO: временный класс-заглушка
public class DataGridColumn
{
    public Dictionary<string, string>? Dictonary { get; set; }
    public DataGridColumn()
    {
        Dictonary = new Dictionary<string, string>()
            {
                // для интервального архива
                { nameof(IntervalArchive.IntervalArchDate), "Дата" },
                { nameof(IntervalArchive.StandardTotalVol), "Vст. общ.[м3]" },
                { nameof(IntervalArchive.StandardUnperturbedVol), "Vст. невозм.[м3]" },
                { nameof(IntervalArchive.WorkingTotalVol), "Vраб. общ.[м3]" },
                { nameof(IntervalArchive.WorkingUnperturbedVol), "Vраб. невозм.[м3]" },
                { nameof(IntervalArchive.GasPressure), "P газа, [бар]" },
                { nameof(IntervalArchive.GasTemperature), "T газа, [C]" },
                { nameof(IntervalArchive.AccumPerHourVol), "V трев., [бар]" },
                { nameof(IntervalArchive.AlarmTimer), "Таймер тревог" },
                { nameof(IntervalArchive.CountingTime), "Время счёта" },
                { nameof(IntervalArchive.AccumStandardVol), "Vст. потр.[м3]" },
                { nameof(IntervalArchive.AccumWorkingVol), "Vраб. потр.[м3]" },
                // для архива событий
                { nameof(EventArchive.IconPath), "EventIcon" },
                { nameof(EventArchive.EventArchDate), "Дата" },
                { nameof(EventArchive.ArchiveRecordNumber), "Арх. №" },
                { nameof(EventArchive.EventCode), "Код события" },
                { nameof(EventArchive.EventArchDescription), "Событие, вызвавшее архивирование" },
                { nameof(EventArchive.BorderEvent), "Граница события" },
                // для архива изменений
                { nameof(ChangesArchive.GlobalNumber), "Глоб. №" },
                { nameof(ChangesArchive.ArchiveNumber), "Арх. №" },
                { nameof(ChangesArchive.ChangeArchDate), "Дата" },
                { nameof(ChangesArchive.Address), "Адрес" },
                { nameof(ChangesArchive.Lable), "Метка" },
                { nameof(ChangesArchive.ChangeArchDescription), "Описание параметра" },
                { nameof(ChangesArchive.OldValue), "Старое значение" },
                { nameof(ChangesArchive.NewValue), "Новое значение" },
                { nameof(ChangesArchive.StatusLock), "Статус замка" },
            };
    }
}