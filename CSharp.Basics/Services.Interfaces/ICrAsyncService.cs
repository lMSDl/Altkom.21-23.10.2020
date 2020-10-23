using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICrAsyncService<T>
    {
        Task CreateAsync(T person);
        Task<T> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
    }
}
