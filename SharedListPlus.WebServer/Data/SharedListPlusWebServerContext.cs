using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedListPlus.Library.Models;

namespace SharedListPlus.WebServer.Data
{
    public class SharedListPlusWebServerContext : DbContext
    {
        public SharedListPlusWebServerContext (DbContextOptions<SharedListPlusWebServerContext> options)
            : base(options)
        {
        }

        public DbSet<SharedListPlus.Library.Models.Person> Person { get; set; }

        public DbSet<SharedListPlus.Library.Models.Group> Group { get; set; }

        public DbSet<SharedListPlus.Library.Models.ListItem> ListItem { get; set; }
    }
}
