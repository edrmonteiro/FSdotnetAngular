using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.Domain.Dtos
{
    public class LoginDto
    {
        public int StatusCode { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        public UserDto User { get; set; }
    }
}
