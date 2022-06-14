using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lapGen.Models
{
    public class Lap
    {
        public int ID { get; set; }
        public int lapNumber {get; set;}
        
        // car fkey
        public int carID { get; set; }
        //public ICollection<Car> car {get; set;}
        //
        public int recordedDriverID { get; set; }
        public string recordedLapTime {get; set;}//{Hours:Mins:Sec:NanoSec}

        public Lap(){
            //this.ID = -1;
            this.recordedDriverID = 0;
            this.recordedLapTime = "00:00:00";
            this.carID = 0;
            //this.car = new List<Car>();
        }
    }
}