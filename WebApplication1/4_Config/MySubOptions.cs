using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4_Config
{
    public class MySubOptions
    {
        public MySubOptions()
        {
            // Set default values.
            SubOption11 = "value1_from_ctor";
            SubOption22 = 5;
        }

        public string SubOption11 { get; set; }
        public int SubOption22 { get; set; }
    }
}
