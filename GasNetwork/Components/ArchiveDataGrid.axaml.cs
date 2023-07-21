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
//TODO: ��� �� �������������, ������ ���������������, ����� ������� DataGrid �������������,
//��� ����� ����� �������� ����� ������ ��� ���������� DataGrid, � ��� �� ������� ���������
//��������� �������
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
            if (startDate >= endDate) throw new Exception("��������� ���� �� ����� ���� ������ �������� ����");

            //��� ������������ ���� ���������� ����� ������ 1-� ������ �� ��
            var reflectedTypeOfArchive = archives.Count > 0 ?
                archives?[0].GetType() : throw new Exception("�� ������� ������ �� ��������� ������!");

            var props = reflectedTypeOfArchive?.GetProperties(); //������� ��� ��-�� ����
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
                        else if (valueFromDict == "������ �����")
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
                dataGrid : new TextBlock() { Text = "�� ������� ������ �� ��������� ������!" };

            dataGrid.IsVisible = Data?.Count > 0 ?
                true : false;
        }
        catch (Exception ex)
        {
            RootControl!.Content = new TextBlock() { Text = ex.Message };
        }
    }
}

//TODO: ��������� �����-��������
public class DataGridColumn
{
    public Dictionary<string, string>? Dictonary { get; set; }
    public DataGridColumn()
    {
        Dictonary = new Dictionary<string, string>()
            {
                // ��� ������������� ������
                { nameof(IntervalArchive.IntervalArchDate), "����" },
                { nameof(IntervalArchive.StandardTotalVol), "V��. ���.[�3]" },
                { nameof(IntervalArchive.StandardUnperturbedVol), "V��. ������.[�3]" },
                { nameof(IntervalArchive.WorkingTotalVol), "V���. ���.[�3]" },
                { nameof(IntervalArchive.WorkingUnperturbedVol), "V���. ������.[�3]" },
                { nameof(IntervalArchive.GasPressure), "P ����, [���]" },
                { nameof(IntervalArchive.GasTemperature), "T ����, [C]" },
                { nameof(IntervalArchive.AccumPerHourVol), "V ����., [���]" },
                { nameof(IntervalArchive.AlarmTimer), "������ ������" },
                { nameof(IntervalArchive.CountingTime), "����� �����" },
                { nameof(IntervalArchive.AccumStandardVol), "V��. ����.[�3]" },
                { nameof(IntervalArchive.AccumWorkingVol), "V���. ����.[�3]" },
                // ��� ������ �������
                { nameof(EventArchive.IconPath), "EventIcon" },
                { nameof(EventArchive.EventArchDate), "����" },
                { nameof(EventArchive.ArchiveRecordNumber), "���. �" },
                { nameof(EventArchive.EventCode), "��� �������" },
                { nameof(EventArchive.EventArchDescription), "�������, ��������� �������������" },
                { nameof(EventArchive.BorderEvent), "������� �������" },
                // ��� ������ ���������
                { nameof(ChangesArchive.GlobalNumber), "����. �" },
                { nameof(ChangesArchive.ArchiveNumber), "���. �" },
                { nameof(ChangesArchive.ChangeArchDate), "����" },
                { nameof(ChangesArchive.Address), "�����" },
                { nameof(ChangesArchive.Lable), "�����" },
                { nameof(ChangesArchive.ChangeArchDescription), "�������� ���������" },
                { nameof(ChangesArchive.OldValue), "������ ��������" },
                { nameof(ChangesArchive.NewValue), "����� ��������" },
                { nameof(ChangesArchive.StatusLock), "������ �����" },
            };
    }
}