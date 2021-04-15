using Brewery.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.Repository.Contracts
{
    public interface IBreweryRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<List<T>> GetAllAsync<T>() where T : class;
        List<T> GetAll<T>() where T : class;
        Task<T> GetItemAsync<T>(int id) where T : class;

        Task<LoginDto> RegisterUser(UserDto userDto);
        Task<LoginDto> LoginUser(UserDto userDto);
        Task<UserPassChangeDto> ChangePassword(UserPassChangeDto userPassChangeDto);
        Task<(StatusDto, List<UserDto>)> GetAllUsers(string userAdmin);
        Task<(StatusDto, UserDto)> AddUser(UserDto userDto, string userAdmin);
        Task<(StatusDto, UserDto)> RemoveUser(UserDto userDto, string userAdmin);
        Task<(StatusDto, UserDto)> AddAdminStatus2User(UserDto userDto, string userAdmin);
        Task<(StatusDto, UserDto)> RemoveAdminStatusFromUser(UserDto userDto, string userAdmin);
    }
}
