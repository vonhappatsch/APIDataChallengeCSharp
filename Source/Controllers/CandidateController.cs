﻿using System;
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
    public class CandidateController : ControllerBase
    {
        private ICandidateService service;
        private IMapper mapper;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/candidate
        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? companyId = null, int? accelerationId = null)
        {
            if (companyId.HasValue)
            {
                return Ok(this.service.FindByCompanyId(companyId.Value).
                            Select(c => mapper.Map<CandidateDTO>(c)).
                            ToList());
            }
            else if (accelerationId.HasValue)
            {
                return Ok(this.service.FindByAccelerationId(accelerationId.Value).
                            Select(c => mapper.Map<CandidateDTO>(c)).
                            ToList());
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/candidate/{userId}/{accelerationId}/{companyId}
        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            return Ok(mapper.Map<CandidateDTO>(service.FindById(userId, accelerationId, companyId)));
        }

        // POST api/candidate
        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(mapper.Map<CandidateDTO>(this.service.Save(mapper.Map<Candidate>(value))));
            }
        }
    }
}
