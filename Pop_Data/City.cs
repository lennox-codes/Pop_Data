using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pop_Data
{
    internal class City
    {
        public bool IsNew { get; set; } = true;
        
        public string CityName { get; set; } = string.Empty;

        public double Population { get; set; }
    }
}