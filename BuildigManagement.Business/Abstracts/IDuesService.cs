using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;

namespace BuildigManagement.Business.Abstracts
{
    public interface IDuesService
    {
        OperationResponse<List<DuesDto>> GetAllDueses();
        OperationResponse<DuesDto> GetDues(int id);
        OperationResponse<Dues> AddDues(DuesDto duesDto);
        OperationResponse<Dues> UpdateDues(DuesDto duesDto);
        OperationResponse<int> RemoveDues(int id);
    }
}
