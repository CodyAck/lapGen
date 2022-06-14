using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using lapGen.Objects;

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
        public List<LapsDisplay> displayLaps { get; set; }

    

        public GeneralDataViewModel()
        {
            this.displayLaps = new List<LapsDisplay>();
            this.cars = new List<Car>();
            this.drivers = new List<Driver>();
           
            this.laps = new List<Lap>();
            

            this.newCar = new Car();
            this.newDriver = new Driver();
            this.newLap = new Lap();
            
        }
        public void GenerateLapDisplayData(){
        }
        [NonAction]  
        public SelectList ToSelectList(IEnumerable<object> table, string valueField)  
        {  
            List<SelectListItem> list = new List<SelectListItem>();  
            string temp1 = table.GetType().ToString();
            temp1 = temp1.Substring(temp1.IndexOf("[") + 1 );
            temp1 = temp1.Substring(0,temp1.Length - 1 );
            //string temp2 = typeof(Track).ToString();
            if(temp1.Equals(typeof(Driver).ToString()) )
            {
                foreach (Driver row in table)  
                {  
                    if(valueField.Equals("Name"))
                    {
                        list.Add(new SelectListItem()  
                        {  
                            Text = row.name, 
                            Value = row.ID.ToString()  
                        });  
                    }
                } 
            }
            
            if(temp1.Equals(typeof(Lap).ToString()))
            {
                foreach (Lap row in table)  
                {  
                    if(valueField.Equals("ID"))
                    {
                        list.Add(new SelectListItem()  
                        {  
                            Text = "Lap ID: "+  row.ID.ToString() + "| Lap#: "+row.lapNumber.ToString() 
                            +"| Driver: " +(drivers.Where(driver => driver.ID == row.recordedDriverID).Select(driver => driver.name).ToList())[0].ToString()
                            +"| Car: " +(cars.Where(car => car.ID == row.carID).Select(car => car.name).ToList())[0].ToString()
                            + "| LapTime: " + row.recordedLapTime.ToString(), 
                            Value = row.ID.ToString()  
                        });  
                    }
                } 
            }
            if(temp1.Equals(typeof(Car).ToString()))
            {
                foreach (Car row in table)  
                {  
                    if(valueField.Equals("Name"))
                    {
                        list.Add(new SelectListItem()  
                        {  
                            Text = row.name, 
                            Value = row.ID.ToString()  
                        });  
                    }
                } 
            }
            
            return new SelectList(list, "Value", "Text");  
        }  
    }
}