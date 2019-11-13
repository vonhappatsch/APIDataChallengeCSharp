using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyService service;
        private readonly IMapper mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/company
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue)
            {
                return Ok(this.service.FindByAccelerationId(accelerationId.Value).
                            Select(c => mapper.Map<CompanyDTO>(c)).
                            ToList());
            }
            else if (userId.HasValue)
            {
                return Ok(this.service.FindByUserId(userId.Value).
                            Select(c => mapper.Map<CompanyDTO>(c)).
                            ToList());
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/company/{id}
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            return Ok(mapper.Map<CompanyDTO>(service.FindById(id)));
        }

        // POST api/company
        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<CompanyDTO>(this.service.Save(mapper.Map<Company>(value))));
            }
        }
    }
}
