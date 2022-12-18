using AutoMapper;
using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.DataAccess.Abstracts;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildigManagement.Business.Concretes
{
    public class UserService : IUserService
    {
        private readonly IRepository _repo;
        private readonly IMapper mapper;

        public UserService(IRepository repo, IMapper map)
        {
            _repo = repo;
            mapper = map;
        }

        public OperationResponse<List<UserDto>> GetAllUsers()
        {
            try
            {
                var users = _repo.GetAll<User>().ToList();

                var usersDto = mapper.Map<List<UserDto>>(users);

                var result = OperationResponse<List<UserDto>>.CreateSuccesResponse(usersDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<List<UserDto>>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<UserDto> GetUser(int id)
        {
            try
            {
                var user = _repo.Get<User>(id);

                var userDto = mapper.Map<UserDto>(user);

                var result = OperationResponse<UserDto>.CreateSuccesResponse(userDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<UserDto>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<User> AddUser(UserDto userDto)
        {
            try
            {
                var user = mapper.Map<User>(userDto);

                var users = _repo.GetAll<User>().ToList();
                var isExist = users.Any(x => x.IdentityNumber.ToUpper().Trim() == userDto.IdentityNumber.ToUpper().Trim()) ||
                              users.Any(x => x.Mail.ToUpper().Trim() == userDto.Mail.ToUpper().Trim()) ||
                              users.Any(x => x.UserName.ToUpper().Trim() == userDto.UserName.ToUpper().Trim());

                var result = new OperationResponse<User>();

                if (!isExist)
                {
                    user.IdentityNumber = user.IdentityNumber.Trim();
                    user.Mail = user.Mail.Trim();
                    user.UserName = user.UserName.Trim();
                    _repo.Add(user);
                    _repo.SaveChanges();

                    result = OperationResponse<User>.CreateSuccesResponse(user);
                    result.Message = "Successfully added.";
                }
                else
                {
                    result = OperationResponse<User>.CreateFailure("This user already exists.");
                }

                return result;

            }
            catch (Exception ex)
            {
                return OperationResponse<User>.CreateFailure(ex.Message);
            }

        }

        public OperationResponse<User> UpdateUser(UserDto userDto)
        {
            try
            {
                var user = mapper.Map<User>(userDto);

                var users = _repo.GetAll<User>().ToList();
                var oldUser = users.Where(x => x.IdentityNumber.ToUpper().Trim() == userDto.IdentityNumber.ToUpper().Trim() || x.Mail.ToUpper().Trim() == userDto.Mail.ToUpper().Trim() || x.UserName.ToUpper().Trim() == userDto.UserName.ToUpper().Trim()).FirstOrDefault();

                var result = new OperationResponse<User>();

                if (oldUser == null || oldUser.Id == userDto.Id)
                {
                    user.IdentityNumber = user.IdentityNumber.Trim();
                    user.Mail = user.Mail.Trim();
                    user.UserName = user.UserName.Trim();
                    _repo.Update(user);
                    var response = _repo.SaveChanges();

                    result = OperationResponse<User>.CreateSuccesResponse(user);
                    result.Message = "Successfully updated.";
                }
                else
                {
                    result = OperationResponse<User>.CreateFailure("This user already exists.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<User>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<int> RemoveUser(int id)
        {
            try
            {
                var user = _repo.Get<User>(id);
                _repo.Remove(user);
                var response = _repo.SaveChanges();
                var result = OperationResponse<int>.CreateSuccesResponse(response);
                result.Message = "Successfully deleted.";
                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<int>.CreateFailure(ex.Message);
            }

        }

        public OperationResponse<List<RoleDto>> GetAllRoles()
        {
            try
            {
                var roles = _repo.GetAll<Role>().ToList();

                var rolesDto = mapper.Map<List<RoleDto>>(roles);

                var result = OperationResponse<List<RoleDto>>.CreateSuccesResponse(rolesDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<List<RoleDto>>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<RoleDto> GetRole(int id)
        {
            try
            {
                var role = _repo.Get<Role>(id);

                var roleDto = mapper.Map<RoleDto>(role);

                var result = OperationResponse<RoleDto>.CreateSuccesResponse(roleDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<RoleDto>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<Role> AddRole(RoleDto roleDto)
        {
            try
            {
                var role = mapper.Map<Role>(roleDto);

                var roles = _repo.GetAll<Role>().ToList();

                var result = new OperationResponse<Role>();

                var isExist = roles.Any(x => x.Name.ToUpper().Trim() == roleDto.Name.ToUpper().Trim());

                if (!isExist)
                {
                    role.Name = role.Name.Trim();
                    _repo.Add(role);
                    _repo.SaveChanges();

                    result = OperationResponse<Role>.CreateSuccesResponse(role);
                    result.Message = "Successfully added.";
                }
                else
                {
                    result = OperationResponse<Role>.CreateFailure("This role is already exist.");
                }

                return result;

            }
            catch (Exception ex)
            {
                return OperationResponse<Role>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<Role> UpdateRole(RoleDto roleDto)
        {
            try
            {
                var role = mapper.Map<Role>(roleDto);

                var roles = _repo.GetAll<Role>().ToList();

                var result = new OperationResponse<Role>();

                var oldData = roles.Where(x => x.Name.ToUpper().Trim() == roleDto.Name.ToUpper().Trim()).FirstOrDefault();

                if (oldData == null || oldData.Id == roleDto.Id)
                {
                    role.Name = role.Name.Trim();
                    _repo.Update(role);
                    var response = _repo.SaveChanges();

                    result = OperationResponse<Role>.CreateSuccesResponse(role);
                    result.Message = "Successfully updated.";
                }
                else
                {
                    result = OperationResponse<Role>.CreateFailure("This role is already exist.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<Role>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<int> RemoveRole(int id)
        {
            try
            {
                var role = _repo.Get<Role>(id);
                _repo.Remove(role);
                var response = _repo.SaveChanges();
                var result = OperationResponse<int>.CreateSuccesResponse(response);
                result.Message = "Successfully deleted.";
                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<int>.CreateFailure(ex.Message);
            }
        }
    }
}
