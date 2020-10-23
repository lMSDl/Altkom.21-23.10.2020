using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICrService<T>
    {
        void Create(T person);
        Person Read(int id);
        IEnumerable<T> Read();
    }
}
