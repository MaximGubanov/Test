using Dapper.Transaction;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasNetwork.Services
{
    public class DataProviderService : IDataProvider
    {
        public static ISettings? Settings { get; set; }
        private static string? _connectionString;
        public static string? ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        private TreeNodeViewModel? TreeNodeVM { get; set; }

        public DataProviderService(ISettings settings)
        {
            Settings = settings;
        }

        public static void SetConnectionString<T>(T objectTreeNode) 
            where T : Tree
        {
            ConnectionString =
                $"Server={Settings!.Server}; " +
                $"User Id={Settings!.User}; " +
                $"Password={Settings!.Password}; " +
                $"Database={objectTreeNode.DatabasePath}";
        }

        async public Task<List<T>> ExecuteDataAsync<T>(string sql)
        {
            List<T> data;
            
            try
            {
                await using (var conn = new FbConnection(ConnectionString))
                {
                    conn.Open();
                    using var transaction = conn.BeginTransaction();
                    data = transaction.Query<T>(sql).ToList();
                    transaction.Commit();
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<T>();
            }
        }
    }
}