using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HebdoProgSemaine3.Models
{
    public class HebdoChallDbContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        //public DbSet<produit> Produit { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=hebdochall3");
                
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.CLI_CODE).HasName("CLI_CODE");
            //modelBuilder.Entity<produit>().HasKey(p => p.PRO_CODE).HasName("PRO_CODE");
        }




    }
}
