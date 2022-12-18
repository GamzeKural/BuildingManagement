using AutoMapper;
using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.DataAccess.Abstracts;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildigManagement.Business.Concretes
{
    public class DuesService : IDuesService
    {
        private readonly IRepository _repo;
        private readonly IMapper mapper;

        public DuesService(IRepository repo, IMapper map)
        {
            _repo = repo;
            mapper = map;
        }
        public OperationResponse<Dues> AddDues(DuesDto duesDto)
        {
            try
            {
                var dues = mapper.Map<Dues>(duesDto);

                var dueses = _repo.GetAll<Dues>().ToList();

                var result = new OperationResponse<Dues>();

                var isInMonth = false;

                var oldDues = dueses.Where(x => x.ApartmentId == duesDto.ApartmentId && x.Type.ToUpper().Trim() == duesDto.Type.ToUpper().Trim() && x.CreatedDate.AddMonths(1) > DateTime.Now).FirstOrDefault();


                if (oldDues != null)
                    isInMonth = (DateTime.Now - oldDues.CreatedDate).TotalDays > 30;
                else
                    isInMonth = true;

                if (isInMonth == true)
                {
                    dues.CreatedDate = DateTime.Now;
                    dues.IsPaid = false;

                    _repo.Add(dues);
                    _repo.SaveChanges();

                    result = OperationResponse<Dues>.CreateSuccesResponse(dues);
                    result.Message = "Successfully added.";
                }
                else
                {
                    result = OperationResponse<Dues>.CreateFailure("There is already an invoice / dues for this apartment this month.");
                }

                return result;

            }
            catch (Exception ex)
            {
                return OperationResponse<Dues>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<List<DuesDto>> GetAllDueses()
        {
            try
            {
                var dueses = _repo.GetAll<Dues>().ToList();

                var duesesDto = mapper.Map<List<DuesDto>>(dueses);

                var result = OperationResponse<List<DuesDto>>.CreateSuccesResponse(duesesDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<List<DuesDto>>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<DuesDto> GetDues(int id)
        {
            try
            {
                var dues = _repo.Get<Dues>(id);

                var duesDto = mapper.Map<DuesDto>(dues);

                var result = OperationResponse<DuesDto>.CreateSuccesResponse(duesDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<DuesDto>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<int> RemoveDues(int id)
        {
            try
            {
                var dues = _repo.Get<Dues>(id);
                _repo.Remove(dues);
                var response = _repo.SaveChanges();
                var result = OperationResponse<int>.CreateSuccesResponse(response);
                result.Message = "Successfully deleted.";
                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<int>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<Dues> UpdateDues(DuesDto duesDto)
        {
            try
            {
                var dues = mapper.Map<Dues>(duesDto);

                var dueses = _repo.GetAll<Dues>().ToList();

                var result = new OperationResponse<Dues>();

                var oldDues = dueses.Where(x => x.ApartmentId == duesDto.ApartmentId && x.Type.ToUpper().Trim() == duesDto.Type.ToUpper().Trim() && x.CreatedDate.AddMonths(1) > DateTime.Now).FirstOrDefault();

                if (oldDues == null || oldDues.Id == duesDto.Id)
                {
                    _repo.Update(dues);
                    var response = _repo.SaveChanges();

                    result = OperationResponse<Dues>.CreateSuccesResponse(dues);
                    result.Message = "Successfully updated.";
                }
                else
                {
                    result = OperationResponse<Dues>.CreateFailure("Failed to update.");

                }
                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<Dues>.CreateFailure(ex.Message);
            }
        }
    }
}
