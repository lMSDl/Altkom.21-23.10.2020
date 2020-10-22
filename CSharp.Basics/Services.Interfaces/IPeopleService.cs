using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPeopleService
    {
        void Create(Person person);
        Person Read(int id);
        IEnumerable<Person> Read();
        void Update(Person person);
    }
}
