using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;

namespace BuildigManagement.Business.Abstracts
{
    public interface IUserService
    {
        OperationResponse<List<UserDto>> GetAllUsers();
        OperationResponse<UserDto> GetUser(int id);
        OperationResponse<User> AddUser(UserDto userDto);
        OperationResponse<User> UpdateUser(UserDto userDto);
        OperationResponse<int> RemoveUser(int id);        
        OperationResponse<List<RoleDto>> GetAllRoles();
        OperationResponse<RoleDto> GetRole(int id);
        OperationResponse<Role> AddRole(RoleDto roleDto);
        OperationResponse<Role> UpdateRole(RoleDto roleDto);
        OperationResponse<int> RemoveRole(int id);
    }
}
