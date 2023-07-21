using FirebirdSql.Data.FirebirdClient;

namespace GasNetwork.Interfaces;

public interface IConnector
{
    public FbConnection FbConnection { get; set; }
}

public interface IDBConnector
{
    public IConnector CreateDBConnectorFactory();
}