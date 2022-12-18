using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IHttpService httpService;

        public UserService(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        #region User
        public async Task<OperationResponse<User>> AddUser(UserDto userDto)
        {
            try
            {
                var result = await httpService.Post<OperationResponse<User>>("User/AddUser", userDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<List<UserDto>>> GetAllUsers()
        {
            try
            {
                var result = await httpService.Get<OperationResponse<List<UserDto>>>("User/GetAllUsers");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<UserDto>> GetUser(int id)
        {
            try
            {
                var result = await httpService.Get<OperationResponse<UserDto>>($"User/GetUser?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<int>> RemoveUser(int id)
        {
            try
            {
                var result = await httpService.Delete<OperationResponse<int>>($"User/RemoveUser?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<User>> UpdateUser(UserDto userDto)
        {
            try
            {
                var result = await httpService.Put<OperationResponse<User>>("User/UpdateUser", userDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Role
        public async Task<OperationResponse<Role>> AddRole(RoleDto roleDto)
        {
            try
            {
                var result = await httpService.Post<OperationResponse<Role>>("User/AddRole", roleDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<List<RoleDto>>> GetAllRoles()
        {
            try
            {
                var result = await httpService.Get<OperationResponse<List<RoleDto>>>("User/GetAllRoles");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<RoleDto>> GetRole(int id)
        {
            try
            {
                var result = await httpService.Get<OperationResponse<RoleDto>>($"User/GetRole?id={id}");

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<int>> RemoveRole(int id)
        {
            try
            {
                var result = await httpService.Delete<OperationResponse<int>>($"User/RemoveRole?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<Role>> UpdateRole(RoleDto roleDto)
        {
            try
            {
                var result = await httpService.Put<OperationResponse<Role>>("User/UpdateRole", roleDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Authenticate

        public async Task<Token> Authenticate(AuthorizeDto authorizeDto)
        {
            try
            {
                var result = await httpService.Post<Token>("User/Authenticate", authorizeDto, false);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
