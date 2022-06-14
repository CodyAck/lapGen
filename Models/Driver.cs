using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lapGen.Models
{
    public class Driver
    {
        public int ID { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public int rank { get; set; }

        public Driver(){
            //this.ID = -1;
            this.active = true;
            this.name = "";
            this.rank = -1;
        }
        public void Clone(Driver other) {
            this.ID = other.ID;
            this.name = other.name;
            this.rank = other.rank;
        }
    }
}