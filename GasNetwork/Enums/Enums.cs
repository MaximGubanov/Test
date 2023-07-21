namespace GasNetwork.Enums
{
    public enum EDBName
    {
        SGS, // SGSDataBaseName
        TMR  // TMRDataBaseName 
    }

    public enum ENodeType
    {
        Consumer, // тип "Потребитель"
        Device    // тип "Счётчик/Прибор/Device"
    }

    public enum EArchType
    { //порядок менять нельзя!
        Actual,
        Monthly, //[1]
        Dayly,   //[2]
        Interval,//[3]
        Change,
        Event
    }
    
    public enum DataGridColumnType
    {
        Text,
        Icon,
        DateTime,
    }
    public enum DataGridValueConverter
    {
        BitmapConverter,
        StringConverter,
        DateTimeConverter,
        NoneConverter,
    }
}