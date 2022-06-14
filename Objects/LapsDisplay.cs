using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lapGen.Objects
{
    public class LapsDisplay
    {
        public int lap_I_D { get; set; }
        public string carName { get; set; }
        public int carNum { get; set; }
        public string raceName { get; set; }
        public string recordedDriverName { get; set; }
        public string lapTime {get; set;}

        public LapsDisplay()
        {
            this.carName = "";
            this.raceName = "";
            this.carNum = 0;
            this.recordedDriverName = "";
            this.lap_I_D = -1;
            this.lapTime = "00:00:00";
        }
    }
}