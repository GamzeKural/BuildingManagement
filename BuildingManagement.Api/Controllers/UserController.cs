using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BuildingManagement.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private ITokenService tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(AuthorizeDto userdata)
        {
            var token = tokenService.Authenticate(userdata);

            if (token == null)
            {
                return Unauthorized("Invalid e-mail or password.");
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpGet("GetAllUsers")]
        public ActionResult<OperationResponse<List<UserDto>>> GetAllUsers()
        {
            try
            {
                var result = userService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpGet("GetUser")]
        public ActionResult<OperationResponse<UserDto>> GetUser(int id)
        {
            try
            {
                var result = userService.GetUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPost("AddUser")]
        public ActionResult<OperationResponse<UserDto>> AddUser(UserDto userDto)
        {
            try
            {
                var result = userService.AddUser(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult<OperationResponse<UserDto>> UpdateUser(UserDto userDto)
        {
            try
            {
                var result = userService.UpdateUser(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpDelete("RemoveUser")]
        public ActionResult<OperationResponse<UserDto>> RemoveUser(int id)
        {
            try
            {
                var result = userService.RemoveUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        //Role

        [HttpGet("GetAllRoles")]
        public ActionResult<OperationResponse<List<RoleDto>>> GetAllRoles()
        {
            try
            {
                var result = userService.GetAllRoles();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetRole")]
        public ActionResult<OperationResponse<RoleDto>> GetRole(int id)
        {
            try
            {
                var result = userService.GetRole(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPost("AddRole")]
        public ActionResult<OperationResponse<RoleDto>> AddRole(RoleDto roleDto)
        {
            try
            {
                var result = userService.AddRole(roleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPut("UpdateRole")]
        public ActionResult<OperationResponse<RoleDto>> UpdateRole(RoleDto roleDto)
        {
            try
            {
                var result = userService.UpdateRole(roleDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpDelete("RemoveRole")]
        public ActionResult<OperationResponse<RoleDto>> RemoveRole(int id)
        {
            try
            {
                var result = userService.RemoveRole(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }
    }
}
