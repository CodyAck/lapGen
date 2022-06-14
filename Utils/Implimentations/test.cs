using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Utils.Interfaces;

namespace lapGen.Utils.Implimentations
{
    

    public class test : ITest
    {
        public int id { get; set; }
        public int getID()
        {
            return id;
        }
    }
}