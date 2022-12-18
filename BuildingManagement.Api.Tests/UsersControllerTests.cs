using BuildigManagement.Business.Abstracts;
using BuildingManagement.Api.Controllers;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BuildingManagement.Api.Tests
{
    public class UsersControllerTests
    {
        private readonly UserController userController;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<ITokenService> tokenServiceMock;

        public UsersControllerTests()
        {
            this.userController = new UserController(userServiceMock.Object, tokenServiceMock.Object);
            this.userServiceMock = new Mock<IUserService>();
            this.tokenServiceMock = new Mock<ITokenService>();
        }

        [Fact]
        public void GetAll_ShouldReturnOk_WhenExistUser()
        {
            var users = CreateUserList();
            var expectedData = ModelToUserResultList(users);

            userServiceMock.Setup(c => c.GetAllUsers()).Returns(users);
        }

        private List<User> CreateUserList()
        {
            return new List<User>
            {
                new User
                {
                    Id=1,
                    FirstName="Admin",
                    LastName="Admin",
                    IdentityNumber="12345678901",
                    Mail="admin@gmail.com",
                    UserName="admin",
                    Password="123456",
                    Phone="05554443322",
                    CarInfo="Yok",
                    RoleId=1
                },
                new User
                {
                    Id=2,
                    FirstName="Admin2",
                    LastName="Admin2",
                    IdentityNumber="12345678902",
                    Mail="admin@gmail.com2",
                    UserName="admin2",
                    Password="123452",
                    Phone="05554443322",
                    CarInfo="Yok2",
                    RoleId=1
                }
            };
        }

        private List<UserDto> ModelToUserResultList(List<User> users)
        {
            var listUsers = new List<UserDto>();
            foreach (var item in users)
            {
                var user = new UserDto
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    IdentityNumber = item.IdentityNumber,
                    Mail = item.Mail,
                    UserName = item.UserName,
                    Password = item.Password,
                    Phone = item.Phone,
                    CarInfo = item.CarInfo,
                    RoleId = item.RoleId
                };
                listUsers.Add(user);
            }
            return listUsers;
        }
    }
}
