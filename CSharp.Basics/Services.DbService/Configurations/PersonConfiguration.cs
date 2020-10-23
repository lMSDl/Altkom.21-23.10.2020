using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
    }
}
