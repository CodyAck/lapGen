using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.Objects;
using lapGen.Utils;

namespace lapGen.Views.ViewModels
{
    public class ReportingViewModel
    {
        public Car newCar { get; set; }
        public DataTable carTable { get; set; }
        public List<Car> cars {get; set;}

        public Driver newDriver{get;set;}
        public List<Driver> drivers{get; set;}

        public List<Record> records{get; set;}

        public Lap newLap { get; set; }
        public List<Lap> laps {get; set;}
        public List<LapsDisplay> displayLaps { get; set; }
        public LapUtilitys utilsRef = new LapUtilitys();
    }
}