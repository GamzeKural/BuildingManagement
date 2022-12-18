using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BuildingManagement.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private IApartmentService apartmentService;

        public ApartmentController(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
        }

        [AllowAnonymous]
        [HttpGet("GetAllApartments")]
        public ActionResult<OperationResponse<List<ApartmentDto>>> GetAllApartments()
        {
            try
            {
                var result = apartmentService.GetAllApartments();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpGet("GetApartment")]
        public ActionResult<OperationResponse<ApartmentDto>> GetApartment(int id)
        {
            try
            {
                var result = apartmentService.GetApartment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPost("AddApartment")]
        public ActionResult<OperationResponse<ApartmentDto>> AddApartment(ApartmentDto apartmentDto)
        {
            try
            {
                var result = apartmentService.AddApartment(apartmentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPut("UpdateApartment")]
        public ActionResult<OperationResponse<ApartmentDto>> UpdateApartment(ApartmentDto apartmentDto)
        {
            try
            {
                var result = apartmentService.UpdateApartment(apartmentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpDelete("RemoveApartment")]
        public ActionResult<OperationResponse<ApartmentDto>> RemoveApartment(int id)
        {
            try
            {
                var result = apartmentService.RemoveApartment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }
    }
}
