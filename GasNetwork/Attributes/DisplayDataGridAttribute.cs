using System;

namespace GasNetwork.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public class DisplayDataGridAttribute : Attribute
{
    public string HeaderName { get; set; }
    public DataGridColumnType ColumnType { get; set; }
    public string ConverterParameter { get; set; }
    public int Order { get; set; }

    public DisplayDataGridAttribute()
    {
        HeaderName = "";
        ColumnType = DataGridColumnType.Text;
        ConverterParameter = "";
    }
}