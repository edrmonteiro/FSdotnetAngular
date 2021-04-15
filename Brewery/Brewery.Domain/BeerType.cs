using System;
using System.Collections.Generic;

namespace Brewery.Domain
{
    public class BeerType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<BeerStyle> Styles { get; set; }
    }
}
