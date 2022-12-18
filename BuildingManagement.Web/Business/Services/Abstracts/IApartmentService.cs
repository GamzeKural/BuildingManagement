using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Abstracts
{
    public interface IApartmentService
    {
        Task<OperationResponse<List<ApartmentDto>>> GetAllApartments();
        Task<OperationResponse<ApartmentDto>> GetApartment(int id);
        Task<OperationResponse<Apartment>> AddApartment(ApartmentDto apartmentDto);
        Task<OperationResponse<Apartment>> UpdateApartment(ApartmentDto apartmentDto);
        Task<OperationResponse<int>> RemoveApartment(int id);
    }
}
