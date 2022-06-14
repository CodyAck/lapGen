using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lapGen.Models;

//follow guide exept for injections

namespace lapGen.persistance
{
    public class DataCon : DbContext
    {
        public DataCon (DbContextOptions<DataCon> options):base(options)
        {

        }
        public DbSet<Driver> Drivers{get; set;}
        public DbSet<Car> Cars{get; set;}
        
        public DbSet<Lap> Laps{get; set;}
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>().ToTable("Driver");
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Lap>().ToTable("Lap");

        }
    }

}