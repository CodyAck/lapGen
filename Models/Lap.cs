using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lapGen.Models
{
    public class Lap
    {
        public Lap()
        {
            this.records = new HashSet<Record>();
        }
        public int ID { get; set; }
        public int lapNumber {get; set;}
        public ICollection<Record> records { get; set; }
    }
}