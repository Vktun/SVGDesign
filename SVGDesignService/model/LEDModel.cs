using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGDesignService.model
{
    public class LEDModel
    {
        public List<Led> LED { get; set; }
        public class Led
        {
            public int id { get; set; }
            public string name { get; set; }
            public double x { get; set; }
            public double y { get; set; }
        }
    }
}
