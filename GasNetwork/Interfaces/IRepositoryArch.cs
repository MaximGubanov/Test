using System.Collections.Generic;

namespace GasNetwork.Interfaces
{
    public interface IRepositoryArch
    {
        public static List<Archive> RetrievAsync<T>(T archive)
        {
            return new List<Archive>();
        }
    }
}