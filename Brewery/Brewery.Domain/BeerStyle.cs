using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewery.Domain
{
    public class BeerStyle
    {
        public int Id { get; set; }
        public string Style { get; set; }
        public int? TypeId { get; set; }
        public BeerType Type { get; set; }
        public string ABV { get; set; }
        public string IBU { get; set; }
        public string InitialDensity { get; set; }
        public string FinalDensity { get; set; }
        public string SRM { get; set; }
    }
}
