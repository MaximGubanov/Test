namespace GasNetwork.Services
{
    public class ConsumerService
    {
        public Consumer? Data { get; set; }
        public IDataProvider? Db { get; }
        public ConsumerService(IDataProvider db) => Db = db;
        public Consumer GetData(int id)
        {
            try
            {
                var consumer = Db?.ExecuteDataAsync<Consumer>(
                    $"SELECT " +
                        $"ID as Id, " +
                        $"NAME as Name, " +
                        $"PARENTID as ParentId, " +
                        $"ADDRESS as Address, " +
                        $"PHONE as Phone, " +
                        $"PERSON as Person, " +
                        $"DUTY as Duty, " +
                        $"KIND as Kind, " +
                        $"CONTRACTNUM as ContractNum, " +
                        $"SITECODE as SiteCode, " +
                        $"ROWVERSION as RowVersion, " +
                        $"INN, " +
                        $"EMAIL as Email " +
                    $"FROM cust " +
                    $"WHERE ID = {id};").Result;
                /// Метод ExecuteData возвращает результат типа List<T>, но т.к. делаем запрос, 
                /// чтобы получить одну запись, найденую по id, то извлекаем запись из списка
                /// по индексу [0] 
                if (consumer != null) return consumer[0];
            }
            catch { }

            return new Consumer();
        }
    }
}