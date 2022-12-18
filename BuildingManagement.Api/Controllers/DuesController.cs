using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BuildingManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DuesController : ControllerBase
    {
        private IDuesService duesService;

        public DuesController(IDuesService duesService)
        {
            this.duesService = duesService;
        }

        [HttpGet("GetAllDueses")]
        public ActionResult<OperationResponse<List<DuesDto>>> GetAllDueses()
        {
            try
            {
                var result = duesService.GetAllDueses();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpGet("GetDues")]
        public ActionResult<OperationResponse<DuesDto>> GetDues(int id)
        {
            try
            {
                var result = duesService.GetDues(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPost("AddDues")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResponse<DuesDto>> AddDues(DuesDto duesDto)
        {
            try
            {
                var result = duesService.AddDues(duesDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPut("UpdateDues")]
        public ActionResult<OperationResponse<DuesDto>> UpdateDues(DuesDto duesDto)
        {
            try
            {
                var result = duesService.UpdateDues(duesDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpDelete("RemoveDues")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResponse<DuesDto>> RemoveDues(int id)
        {
            try
            {
                var result = duesService.RemoveDues(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }
    }
}
