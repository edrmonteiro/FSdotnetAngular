using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.Domain.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
