using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GasNetwork.Components;

public partial class DataGridCustom : UserControl
{
    public static readonly StyledProperty<List<IDataGridRepresenter>> SourceDataProperty =
        AvaloniaProperty.Register<DataGridCustom, List<IDataGridRepresenter>>(
            nameof(SourceData),
            defaultValue: new List<IDataGridRepresenter>());

    public List<IDataGridRepresenter> SourceData
    {
        get => GetValue(SourceDataProperty);
        set => SetValue(SourceDataProperty, value);
    }

    private UserControl? UserControl { get; set; }
    private DataGrid DataGridControl { get; set; }

    public DataGridCustom()
    {
        InitializeComponent();
        UserControl = CustomUserControl;
        DataGridControl = new DataGrid();

        SourceDataProperty
            .Changed
            .Subscribe(x =>
                OnSourceDataChanged(x.Sender, x.NewValue.GetValueOrDefault<List<IDataGridRepresenter>>()));
    }

    private void OnSourceDataChanged(IAvaloniaObject o, List<IDataGridRepresenter> sourceData)
    {
        DataGridControl.Items = null;
        DataGridControl.Columns.Clear();

        if (sourceData is not null || sourceData.Count > 0)
        {
            CreateDataGridColumns(sourceData[0]); //для исслед. типа нужен только один эл-т из списка, берём 1-й
            DataGridControl.Items = SourceData;
            UserControl.Content = DataGridControl;
        }
        else
        {
            UserControl.Content = new TextBlock() { Text = "Данные не получены"};
        }
    }

    private void CreateDataGridColumns(IDataGridRepresenter objectSource)
    {
        PropertyInfo[] properties = objectSource
            .GetPropertiesByOrderAttribute<IDataGridRepresenter, DisplayDataGridAttribute>();

        foreach (var property in properties)
        {   // Вариант 1: получение атрибута в документации Microsoft
            // var attr = (DisplayDataGridColumnAttribute)Attribute.GetCustomAttribute(prop,
            //     typeof(DisplayDataGridColumnAttribute), true);

            // Вариант 2:
            var attribute = property.GetCustomAttribute<DisplayDataGridAttribute>();

            if (property is not null && attribute is not null)
                DataGridControl.Columns.Add(CreateDataGridTemplateColumn(objectSource, property, attribute));
        }
    }

    private DataGridTemplateColumn CreateDataGridTemplateColumn(
        object obj, 
        PropertyInfo property, 
        DisplayDataGridAttribute attribute)
    {
        var templateColumn = new DataGridTemplateColumn();
        var propertyValue = property.GetValue(obj);

        templateColumn.Header = attribute?.HeaderName;
        templateColumn.CellTemplate = new FuncDataTemplate<IDataGridRepresenter>(
            (value, namescope) =>
        {
            Control content = new();
            //TODO: решение с if-ами не самое лучшее решение
            if (attribute.ColumnType == DataGridColumnType.Text)
                content = new TextBlock()
                {
                    [!TextBlock.TextProperty] = new Binding() { Path = property.Name }
                };

            if (attribute.ColumnType == DataGridColumnType.Icon)
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                //TODO: разобраться с иконкаим в Columns
                var bitmap = new Bitmap(assets?.Open(new Uri("avares://GasNetwork/Assets/img/1.png")));
                content = new Image() { Source = bitmap, Width = 16 };
            }

            if (attribute.ColumnType == DataGridColumnType.DateTime)
            {
                content = new TextBlock()
                {
                    [!TextBlock.TextProperty] =
                        new Binding()
                        {
                            Path = property.Name,
                            Converter = DateValueConverter.Instance,
                            ConverterParameter = attribute.ConverterParameter,
                        },
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }
            
            return content;
        });
        
        return templateColumn;
    }
}