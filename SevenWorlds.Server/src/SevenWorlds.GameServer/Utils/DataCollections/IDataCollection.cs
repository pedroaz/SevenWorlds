using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Utils.DataCollections
{
    public interface IDataCollection<T>
    {
        void Add(T data);
        T FindById(int id);
        void Remove(int id);
        IEnumerable<T> GetAll();
    }
}
