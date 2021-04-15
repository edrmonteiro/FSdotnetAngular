using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewery.Domain
{
    public class BeerType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<BeerStyle> Styles { get; set; }
    }

}
