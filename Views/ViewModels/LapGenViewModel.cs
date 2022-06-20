using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.Objects;
using lapGen.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lapGen.Views.ViewModels
{
    public class LapGenViewModel
    {
        public Lap newLap { get; set; }
        public List<Lap> laps {get; set;}
        public List<LapsDisplay> displayLaps { get; set; }
        public List<Car> cars {get; set;}
        public List<Driver> drivers{get; set;}
        public List<Record> records{get; set;}
        public LapUtilitys utilsRef = new LapUtilitys();

        public LapGenViewModel()
        {
            this.displayLaps = new List<LapsDisplay>();
            this.laps = new List<Lap>();
            this.newLap = new Lap();
        }
    }

    
}