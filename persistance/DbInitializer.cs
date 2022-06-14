using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Models;

namespace lapGen.persistance
{
    public class DbInitializer
    {
        public static void Initialize(DataCon con)
        {
            var drivers = new Driver[]
            {
                new Driver{name = "Cal Naughton Jr", rank = new Random().Next(1,100)},
                new Driver{name = "Jean Girard", rank = new Random().Next(1,100)},
                new Driver{name = "Ricky Bobby", rank = new Random().Next(1,100)},
                new Driver{name = "Glenn", rank = new Random().Next(1,100)},
                new Driver{name = "Texas Walker Ranger", rank = new Random().Next(1,100)},
            };
            var cars = new Car[]
            {
                new Car{name = "The Wonder Bread",number = 1},
                new Car{name = "Busch", number = 4},
                new Car{name = "Ups", number = 6},
                new Car{name = "Mobil 1", number = 16},
                new Car{name = "Panther", number = 2},
            };
            

            foreach( Car item in cars) 
            {
                int numberOfRandomDivers = new Random().Next(1,drivers.Length);
                if(numberOfRandomDivers == drivers.Length)
                {
                    foreach(Driver d in drivers )
                    {
                        item.possibleDrivers.Add(d);
                    }
                    
                }
                else
                {
                    for(int i = 0;i<numberOfRandomDivers;i++)
                    {
                        int randomDriver = new Random().Next(0,drivers.Length-2);
                        // if(item.drivers.Contains(drivers[randomDriver]))
                        // {
                        //     //skip
                        // }
                        // else
                        // {
                            item.possibleDrivers.Add(drivers[0]);
                        //}
                        
                    }
                }
                item.SetAsStockCar();
            }

            var laps = new Lap[]
            {
                new Lap{recordedLapTime="00:03:34:14", recordedDriverID = 1, carID = 1, lapNumber = 1}
            };
            

            if(!con.Drivers.Any())
            {
                con.Drivers.AddRange(drivers);
                con.SaveChanges();
            }
            if(!con.Cars.Any())
            {
                con.Cars.AddRange(cars);
                con.SaveChanges();
            }
            
            if(!con.Laps.Any())
            {
                con.Laps.AddRange(laps);
                con.SaveChanges();
            }
        

        
        }
    }
}