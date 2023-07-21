using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GasNetwork.Interfaces
{
    public interface IArchive
    {
        public Task<List<Archive>> GetDataAsync(DateTime start, DateTime end);
    }
}
