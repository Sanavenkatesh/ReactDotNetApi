using Microsoft.EntityFrameworkCore;
using ReactDotNetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetApi.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
