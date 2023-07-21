using System;
using System.Collections.Generic;
using GasNetwork.Attributes;

namespace GasNetwork.Models;

public class Consumption
{
    [DisplayDataGrid(
        ColumnType = DataGridColumnType.Icon,
        Order = 1)]
    public string? WarningIcon { get; set; }

    [DisplayDataGrid(HeaderName = "Vст. общ., [м3]", Order = 3)]
    public Double VolumeStableOverall { get; set; }

    [DisplayDataGrid(HeaderName = "Vст. возм., [м3]", Order = 4)]
    public Double VolumeStablePerturbed { get; set; }

    [DisplayDataGrid(HeaderName = "Vст. невозм., [м3]", Order = 5)]
    public Double VolumeStableUnperturbed { get; set; }

    [DisplayDataGrid(HeaderName = "Vраб. общ., [м3]", Order = 6)]
    public Double TotalWorkingVolume { get; set; }

    [DisplayDataGrid(HeaderName = "P, [кгс/см2]", Order = 7)]
    public Double Pressure { get; set; }

    [DisplayDataGrid(HeaderName = "T, [град. С]", Order = 8)]
    public Double Temperature { get; set; }

    [DisplayDataGrid(HeaderName = "Коэф. корр.", Order = 9)]
    public Double CorrectionFactor { get; set; }

    [DisplayDataGrid(HeaderName = "Полнота данных, [%]", Order = 10)]
    public Double DataCompleteness { get; set; }
}

public class YearConsumption : Consumption, IDataGridRepresenter
{
    public string Title { get; set; } = "Годы";

    [DisplayDataGrid(
        HeaderName = "Год", 
        ColumnType = DataGridColumnType.DateTime,
        ConverterParameter = "yyyy",
        Order = 2)]
    public DateTime? Year { get; set; }

}
public class MonthlyConsumption : Consumption, IDataGridRepresenter
{
    public string Title { get; set; } = "Месяцы";
}

public class DaylyConsumption : Consumption, IDataGridRepresenter
{
    public string Title { get; set; } = "Сутки";
}

public class HourConsumption : Consumption, IDataGridRepresenter
{
    public string Title { get; set; } = "Часы";
}