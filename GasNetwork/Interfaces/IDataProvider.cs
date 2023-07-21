using System.Collections.Generic;
using System.Threading.Tasks;

namespace GasNetwork.Interfaces
{
    public interface IDataProvider
    {
        public Task<List<T>> ExecuteDataAsync<T>(string sql);
    }
}
