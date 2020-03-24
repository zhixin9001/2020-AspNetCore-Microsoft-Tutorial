using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4_Config
{
    public interface IStarship { }
    public class Starship : IStarship
    {
        public string Name { get; set; }
        public string Registry { get; set; }
        public string Class { get; set; }
        public decimal Length { get; set; }
        public bool Commissioned { get; set; }
        public ShipLog ShipLog { get; set; }
        public string[] Array { get; set; }
    }
    public class ShipLog
    {
        public string ID { get; set; }
    }
}
