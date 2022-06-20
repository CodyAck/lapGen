using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lapGen.Utils
{
    public class LapUtilitys
    {
#region time
        //Takes In time as Hours: Minutes: Seconds , example: 01:20:10
        public int ConvertTimeInputToSeconds(string input)
        {
            if(input == null)
            {
                return 0;
            }
            if(input.Trim().Equals(""))
            {
                return 0;
            }
            string[] data = input.Split(":");
            int hours = Convert.ToInt32(data[0]);
            int minutes = Convert.ToInt32(data[1]);
            int seconds = Convert.ToInt32(data[2]);

            seconds += minutes * 60;
            seconds += hours * (60*60);
            return seconds;
        }
        public string ConvertSecondsToTimeString(int input)
        {
            int hours = input / 3600;
            int minutes =  (input - (hours * 3600))/60;
            int seconds = input - ((hours * 3600)+(minutes * 60));
            string returnString = "";
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
            returnString +="0" + hours +":";
            else
            returnString += hours +":";

            if(minutes < 10)
            returnString += "0" + minutes +":";
            else
            returnString += minutes +":";

            if(seconds < 10)
            returnString += "0" + seconds;
            else
            returnString += seconds;


            return returnString;
        }
        public string CalculateAverageLapTime(int id,List<Record> records,string averageBy = "driver")
        {
            int totalSeconds = 0;
            int count = 0;
            if(averageBy.Equals("driver"))
            {
                foreach (Record item in records)
                {   
                    if(item.driverID == id)
                    {
                        count++;
                        if(item.timeInSeconds == 0)
                        {
                            continue;
                        }
                        else 
                        {
                            totalSeconds += item.timeInSeconds;
                        }
                    }
                }
            }

            else if(averageBy.Equals("car"))
            {
                foreach (Record item in records)
                {   
                    if(item.carID == id)
                    {
                        count++;
                        if(item.timeInSeconds == 0)
                        {
                            continue;
                        }
                        else 
                        {
                            totalSeconds += item.timeInSeconds;
                        }
                    }
                }
            }
            if(count == 0)
            {
                return "00:00:00";
            }
            return ConvertSecondsToTimeString(totalSeconds / count);
        }
#endregion
        
        public SelectList GenDropDownList(IEnumerable<object> table, string valueField)
        {
            List<SelectListItem> list = new List<SelectListItem>();  
            string temp1 = table.GetType().ToString();
            temp1 = temp1.Substring(temp1.IndexOf("[") + 1 );
            temp1 = temp1.Substring(0,temp1.Length - 1 );
            if(temp1.Equals(typeof(Driver).ToString()) )
            {
                foreach (Driver row in table)  
                {  
                    if(!row.active)
                    {
                        continue;
                    }
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
                            Text = "Lap ID: "+  row.ID.ToString() + "| Lap#: "+row.lapNumber.ToString(),
                            Value = row.ID.ToString()  
                        });  
                    }
                } 
            }
            if(temp1.Equals(typeof(Record).ToString()))
            {
                foreach (Record row in table)  
                {  
                    if(valueField.Equals("ID"))
                    {
                        list.Add(new SelectListItem()  
                        {  
                            Text = "Lap#: "+row.lap.lapNumber.ToString() 
                            + "| Driver:" + row.driver.name + "| Car:"+ row.car.number
                            + "| Time:"+ConvertSecondsToTimeString(row.timeInSeconds),
                            Value = row.ID.ToString()  
                        });  
                    }
                } 
            }
            if(temp1.Equals(typeof(Car).ToString()))
            {
                foreach (Car row in table)  
                {  if(!row.active)
                    {
                        continue;
                    }
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