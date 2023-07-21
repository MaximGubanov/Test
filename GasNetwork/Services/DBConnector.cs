using Dapper;
using FirebirdSql.Data.FirebirdClient;

namespace GasNetwork.Services;

public class CreatorSGSConnector : IDBConnector
{
    public IConnector CreateDBConnectorFactory() => new SGSConnector(settings);
}

public class CreatorTMRConnector : IDBConnector
{
    public IConnector CreateDBConnectorFactory() => new TMRConnector();
}

public class SGSConnector : IConnector
{
    public FbConnection FbConnection { get; set; } = new FbConnection();
    public ISettings Settings { get; set; }
    public SGSConnector(ISettings settings)
    {
        // И тут строка подключения???
        // 2 объета которые владеют одной и той же иформацией повод сделать рефакториг
        Settings = settings;
        FbConnection.ConnectionString = 
            $"Server={Settings!.Server}; " +
            $"User Id={Settings!.User}; " +
            $"Password={Settings!.Password}; " +
            $"Database={Settings!.SGSServerPath}";
    }
}

public class TMRConnector : IConnector
{
    public FbConnection FbConnection { get; set; }
}

public class DBConnector : IConnector
{
    public ISettings Settings { get; set; }
    public string? ConnectionString { get; set; }
    public FbConnection FbConnection { get; set; }

    public DBConnector(ISettings settings)
    {
        Settings = settings;
        FbConnection = new FbConnection();
    }
}