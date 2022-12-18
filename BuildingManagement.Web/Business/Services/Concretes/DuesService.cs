using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Concretes
{
    public class DuesService : IDuesService
    {
        private readonly IHttpService httpService;

        public DuesService(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task<OperationResponse<Dues>> AddDues(DuesDto duesDto)
        {
            try
            {
                var result = await httpService.Post<OperationResponse<Dues>>("Dues/AddDues", duesDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<List<DuesDto>>> GetAllDueses()
        {
            try
            {
                var result = await httpService.Get<OperationResponse<List<DuesDto>>>("Dues/GetAllDueses");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<DuesDto>> GetDues(int id)
        {
            try
            {
                var result = await httpService.Get<OperationResponse<DuesDto>>($"Dues/GetDues?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<int>> RemoveDues(int id)
        {
            try
            {
                var result = await httpService.Delete<OperationResponse<int>>($"Dues/RemoveDues?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<Dues>> UpdateDues(DuesDto duesDto)
        {
            try
            {
                var result = await httpService.Put<OperationResponse<Dues>>("Dues/UpdateDues", duesDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
