using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using lapGen.Objects;
using lapGen.Utils;

namespace lapGen.Views.ViewModels
{
    public class GeneralDataViewModel
    {
        public Car newCar { get; set; }
        public DataTable carTable { get; set; }
        public List<Car> cars {get; set;}

        public Driver newDriver{get;set;}
        public List<Driver> drivers{get; set;}

        public Lap newLap { get; set; }
        public List<Lap> laps {get; set;}

        public List<Record> records {get;set;}
        public Record newRecord {get; set;}
        public string timeIN{get;set;}
        public LapUtilitys utilsRef = new LapUtilitys();
        public GeneralDataViewModel()
        {
            this.cars = new List<Car>();
            this.drivers = new List<Driver>();
            this.laps = new List<Lap>();
            

            this.newCar = new Car();
            this.newDriver = new Driver();
            this.newLap = new Lap();
            this.newRecord = new Record();

            timeIN = "";
        }
        //[NonAction]  
        // public SelectList ToSelectList(IEnumerable<object> table, string valueField)  
        // {  
            
    }
}