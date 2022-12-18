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

    public class ApartmentService : IApartmentService
    {
        private readonly IRepository _repo;
        private readonly IMapper mapper;

        public ApartmentService(IRepository repo, IMapper map)
        {
            _repo = repo;
            mapper = map;
        }

        public OperationResponse<Apartment> AddApartment(ApartmentDto apartmentDto)
        {
            try
            {
                var apartment = mapper.Map<Apartment>(apartmentDto);

                var apartments = _repo.GetAll<Apartment>().ToList();

                var user = _repo.Get<User>(apartment.UserId);

                var isExist = apartments.Any(x => x.Block == apartmentDto.Block && x.FloorLocation == apartmentDto.FloorLocation && x.Number == apartmentDto.Number);



                var result = new OperationResponse<Apartment>();

                if (!isExist)
                {
                    apartment.ApartmentInfo = apartment.Block + " " + apartment.Number + " " + user.FirstName + " " + user.LastName;

                    _repo.Add(apartment);
                    _repo.SaveChanges();

                    result = OperationResponse<Apartment>.CreateSuccesResponse(apartment);
                    result.Message = "Successfully added.";
                }
                else
                {
                    return OperationResponse<Apartment>.CreateFailure("This apartment already exists.");
                }

                return result;

            }
            catch (Exception ex)
            {
                return OperationResponse<Apartment>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<List<ApartmentDto>> GetAllApartments()
        {
            try
            {
                var apartments = _repo.GetAll<Apartment>().ToList();

                var apartmentsDto = mapper.Map<List<ApartmentDto>>(apartments);

                var result = OperationResponse<List<ApartmentDto>>.CreateSuccesResponse(apartmentsDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<List<ApartmentDto>>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<ApartmentDto> GetApartment(int id)
        {
            try
            {
                var apartment = _repo.Get<Apartment>(id);

                var apartmentDto = mapper.Map<ApartmentDto>(apartment);

                var result = OperationResponse<ApartmentDto>.CreateSuccesResponse(apartmentDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<ApartmentDto>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<int> RemoveApartment(int id)
        {
            try
            {
                var apartment = _repo.Get<Apartment>(id);
                _repo.Remove(apartment);
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

        public OperationResponse<Apartment> UpdateApartment(ApartmentDto apartmentDto)
        {
            try
            {
                var apartment = mapper.Map<Apartment>(apartmentDto);

                var apartments = _repo.GetAll<Apartment>().ToList();

                var result = new OperationResponse<Apartment>();

                var oldData = apartments.Where(x => x.Block == apartmentDto.Block && x.FloorLocation == apartmentDto.FloorLocation && x.Number == apartmentDto.Number).FirstOrDefault();

                if (oldData == null || oldData.Id == apartmentDto.Id)
                {
                    _repo.Update(apartment);
                    var response = _repo.SaveChanges();

                    result = OperationResponse<Apartment>.CreateSuccesResponse(apartment);
                    result.Message = "Successfully updated.";
                }
                else
                {
                    return OperationResponse<Apartment>.CreateFailure("This apartment already exists.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<Apartment>.CreateFailure(ex.Message);
            }
        }
    }
}
