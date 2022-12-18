using BuildigManagement.Business.Abstracts;
using BuildingManagement.DataAccess.Abstracts;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuildigManagement.Business.Concretes
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration config;
        private readonly IRepository repo;
        public TokenService(IConfiguration config, IRepository repo)
        {
            this.config = config;
            this.repo = repo;
        }
        public Token Authenticate(AuthorizeDto user)
        {
            var users = repo.GetAll<User>().ToList();

            if (!users.Any(x => (x.UserName == user.UserName || x.Mail == user.UserName) && x.Password == user.Password))
            {
                return null;
            }

            var userData = users.FirstOrDefault(x => (x.UserName == user.UserName || x.Mail == user.UserName) && x.Password == user.Password);

            var role = repo.GetAll<Role>().FirstOrDefault(x => x.Id == userData.RoleId);

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, role.Name) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token { VerifiedToken = tokenHandler.WriteToken(token) };

        }
    }
}
