using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }


        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             if (!optionsBuilder.IsConfigured)
             {
                 optionsBuilder.UseSqlServer(GetConnectionString());
             }
         }

         public String GetConnectionString()
         {
             var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
             IConfiguration configuration = builder.Build();
             return configuration.GetConnectionString("Default");
         }*/
    }
}
