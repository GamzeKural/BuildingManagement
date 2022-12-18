using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildigManagement.Business.Abstracts
{
    public interface ITokenService
    {
        public Token Authenticate(AuthorizeDto user);
    }
}
