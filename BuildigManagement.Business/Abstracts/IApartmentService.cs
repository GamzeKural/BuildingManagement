using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;

namespace BuildigManagement.Business.Abstracts
{
    public interface IApartmentService
    {
        OperationResponse<List<ApartmentDto>> GetAllApartments();
        OperationResponse<ApartmentDto> GetApartment(int id);
        OperationResponse<Apartment> AddApartment(ApartmentDto apartmentDto);
        OperationResponse<Apartment> UpdateApartment(ApartmentDto apartmentDto);
        OperationResponse<int> RemoveApartment(int id);
    }
}
