using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using lapGen.Objects;

namespace lapGen.Views.ViewModels
{
    public class ReportingViewModel
    {
        public Car newCar { get; set; }
        public DataTable carTable { get; set; }
        public List<Car> cars {get; set;}

        public Driver newDriver{get;set;}
        public List<Driver> drivers{get; set;}

        public Lap newLap { get; set; }
        public List<Lap> laps {get; set;}
        public List<LapsDisplay> displayLaps { get; set; }

        public string CalculateAverageLapTime(int id, string averageBy = "driver")
        {
            string returnValue = "00:00:00";
            int count = 0;
            if(averageBy.Equals("driver"))
            {
                foreach (Lap item in laps)
                {   
                    if(item.recordedDriverID == id)
                    {
                        count++;
                        if(item.recordedLapTime.Equals("00:00:00"))
                        {
                            continue;
                        }
                        if(returnValue.Equals("00:00:00"))
                        {
                            returnValue =  item.recordedLapTime;
                        }
                        else 
                        {
                            returnValue = AddTimes(returnValue, item.recordedLapTime);
                        }
                    }
                }
            }

            else if(averageBy.Equals("car"))
            {
                foreach (Lap item in laps)
                {   
                    if(item.carID == id)
                    {
                        count++;
                        if(item.recordedLapTime.Equals("00:00:00"))
                        {
                            continue;
                        }
                        if(returnValue.Equals("00:00:00"))
                        {
                            returnValue =  item.recordedLapTime;
                        }
                        else 
                        {
                            returnValue = AddTimes(returnValue, item.recordedLapTime);
                        }
                    }
                }
            }
            
            if(returnValue.Equals("00:00:00"))
            {
                return returnValue;
            }
            return DivideTimes(returnValue, count);
        }
        public string DivideTimes(string time, int divider)
        {
            string returnString = "";
            string[] Data = time.Split(":");
            int hours = Convert.ToInt32(Data[0]);
            int minutes = Convert.ToInt32(Data[1]);
            int seconds = Convert.ToInt32(Data[2]);
            seconds += minutes * 60;
            seconds += hours * 3600;

            seconds = seconds/divider;

            hours =  seconds/3600;
            minutes = (seconds  - (hours*3600)) / 60;
            seconds = seconds - (minutes*60 + hours*3600); 

            if(seconds > 60)
            {
                seconds = 0;
                minutes++;
            }
            if(minutes > 60)
            {
                minutes = 0;
                hours++;
            }
            
            if(hours < 10)
            returnString += "0" + hours +":";
            else
            returnString += hours +":";

            if(minutes < 10)
            returnString +=  "0" + minutes +":";
            else
            returnString += minutes +":";

            if(seconds < 10)
            returnString += "0" + seconds;
            else
            returnString += seconds;

            return returnString;

        }
        public string AddTimes(string sum, string additive)
        {
            string[] sumData = sum.Split(":");
            int shours = Convert.ToInt32(sumData[0]);
            int sminutes = Convert.ToInt32(sumData[1]);
            int sseconds = Convert.ToInt32(sumData[2]);
            

            string[] additiveData = additive.Split(":");
            int ahours = Convert.ToInt32(additiveData[0]);
            int aminutes = Convert.ToInt32(additiveData[1]);
            int aseconds = Convert.ToInt32(additiveData[2]);
            
            string returnString = "";
            
            sseconds += aseconds;
            sminutes += aminutes;
            shours += ahours;

            
            if(sseconds > 60)
            {
                sseconds = 0;
                sminutes++;
            }
            if(sminutes > 60)
            {
                sminutes = 0;
                shours++;
            }
            
            if(shours < 10)
            returnString += "0" + shours +":";
            else
            returnString += shours +":";

            if(sminutes < 10)
            returnString += "0" + sminutes +":";
            else
            returnString += sminutes +":";

            if(sseconds < 10)
            returnString += "0" + sseconds;
            else
            returnString += sseconds;

            

            return returnString;
        }
    }
}