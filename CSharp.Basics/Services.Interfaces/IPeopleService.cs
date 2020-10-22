using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICrudService<T>
    {
        void Create(T person);
        Person Read(int id);
        IEnumerable<T> Read();
        void Update(T person);
        void Delete(int id);
    }
}
