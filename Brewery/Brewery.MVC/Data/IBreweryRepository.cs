using Brewery.MVC.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.MVC.Data
{
    public interface IBreweryRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<LoginDto> RegisterUser(UserDto userDto);
        Task<LoginDto> LoginUser(UserDto userDto);
        Task<UserPassChangeDto> ChangePassword(UserPassChangeDto userPassChangeDto);
    }
}
