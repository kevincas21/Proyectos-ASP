using APIClientes.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Data
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option):base (option)
        {

        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
