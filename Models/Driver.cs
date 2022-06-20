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
    }
}