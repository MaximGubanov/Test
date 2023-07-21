using System;

namespace GasNetwork.Models;

public class DeviceParam : IDataGridRepresenter
{
    [DisplayDataGrid(HeaderName = "Раздел")]
    public string? Section { get; set; }
    
    [DisplayDataGrid(
        HeaderName = "Дата считывания", 
        ColumnType = DataGridColumnType.DateTime)]
    public DateTime? ReadDate { get; set; }
    
    [DisplayDataGrid(HeaderName = "Адрес")]
    public string? Address { get; set; }
    
    [DisplayDataGrid(HeaderName = "Метка")]
    public string? Lable { get; set; }
    
    [DisplayDataGrid(HeaderName = "Знвчение")]
    public string? ParamValue { get; set; }
    
    [DisplayDataGrid(HeaderName = "Ед. измерения")]
    public string? Unit { get; set; }
    
    [DisplayDataGrid(HeaderName = "Описание")]
    public string? Description { get; set; }

    public string Title { get; set; } = "Параметры прибора";
}