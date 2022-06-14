using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lapGen.Utils.Interfaces;

namespace lapGen.Utils.Implimentations
{
    public class FakeTest : ITest
    {
        //public int id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int id {get; set;}
        public int getID()
        {
            return 1;
            //throw new NotImplementedException();
        }
    }
}