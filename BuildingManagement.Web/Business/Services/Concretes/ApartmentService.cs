using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Concretes
{
    public class ApartmentService : IApartmentService
    {
        private readonly IHttpService httpService;

        public ApartmentService(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task<OperationResponse<Apartment>> AddApartment(ApartmentDto apartmentDto)
        {
            try
            {
                var result = await httpService.Post<OperationResponse<Apartment>>("Apartment/AddApartment", apartmentDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<List<ApartmentDto>>> GetAllApartments()
        {
            try
            {
                var result = await httpService.Get<OperationResponse<List<ApartmentDto>>>("Apartment/GetAllApartments");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<ApartmentDto>> GetApartment(int id)
        {
            try
            {
                var result = await httpService.Get<OperationResponse<ApartmentDto>>($"Apartment/GetApartment?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<int>> RemoveApartment(int id)
        {
            try
            {
                var result = await httpService.Delete<OperationResponse<int>>($"Apartment/RemoveApartment?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<Apartment>> UpdateApartment(ApartmentDto apartmentDto)
        {
            try
            {
                var result = await httpService.Put<OperationResponse<Apartment>>("Apartment/UpdateApartment", apartmentDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
