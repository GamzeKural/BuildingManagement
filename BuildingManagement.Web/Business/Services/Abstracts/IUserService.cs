using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Abstracts
{
    public interface IUserService
    {
        Task<OperationResponse<List<UserDto>>> GetAllUsers();
        Task<OperationResponse<UserDto>> GetUser(int id);
        Task<OperationResponse<User>> AddUser(UserDto userDto);
        Task<OperationResponse<User>> UpdateUser(UserDto userDto);
        Task<OperationResponse<int>> RemoveUser(int id);
        Task<OperationResponse<List<RoleDto>>> GetAllRoles();
        Task<OperationResponse<RoleDto>> GetRole(int id);
        Task<OperationResponse<Role>> AddRole(RoleDto roleDto);
        Task<OperationResponse<Role>> UpdateRole(RoleDto roleDto);
        Task<OperationResponse<int>> RemoveRole(int id);
        Task<Token> Authenticate(AuthorizeDto authorizeDto);
    }
}
