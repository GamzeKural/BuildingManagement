using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Abstracts
{
    public interface IDuesService
    {
        Task<OperationResponse<List<DuesDto>>> GetAllDueses();
        Task<OperationResponse<DuesDto>> GetDues(int id);
        Task<OperationResponse<Dues>> AddDues(DuesDto duesDto);
        Task<OperationResponse<Dues>> UpdateDues(DuesDto duesDto);
        Task<OperationResponse<int>> RemoveDues(int id);
    }
}
