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
        public DbSet<Record> Records{get; set;}
        public DbSet<Lap> Laps{get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // var lapEntity = modelBuilder.Entity<Lap>();
            // lapEntity.HasIndex(x =>x.lapNumber).IsUnique(true);
        }
    }

}