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
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService service;
        private readonly IMapper mapper;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/acceleration
        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId.HasValue)
            {
                return Ok(this.service.FindByCompanyId(companyId.Value).
                            Select(a => mapper.Map<AccelerationDTO>(a)).
                            ToList());
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/acceleration/{id}
        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            return Ok(mapper.Map<AccelerationDTO>(service.FindById(id)));
        }

        // POST api/acceleration
        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(mapper.Map<AccelerationDTO>(this.service.Save(mapper.Map<Acceleration>(value))));
            }
        }
    }
}
