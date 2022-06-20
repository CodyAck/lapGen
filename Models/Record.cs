using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lapGen.Models
{
    public class Record
    {
        public int ID { get; set; }
        public int timeInSeconds { get; set; }//in seconds

        public int carID { get; set; }
        public Car car { get; set; }

        public int driverID { get; set; }
        public Driver driver { get; set; }

        public int lapID {get;set;}
        public Lap lap { get; set; }
    }
}