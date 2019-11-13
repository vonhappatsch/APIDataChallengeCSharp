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
    public class UserController : ControllerBase
    {
        private IUserService service;
        private readonly IMapper mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {            
            if (accelerationName != null)
            {
                return Ok(this.service.FindByAccelerationName(accelerationName).
                            Select(u => mapper.Map<UserDTO>(u)).
                            ToList());
            }
            else if (companyId.HasValue)
            {
                return Ok(this.service.FindByCompanyId(companyId.Value).
                                Select(u => mapper.Map<UserDTO>(u)).
                                ToList());
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            return Ok(mapper.Map<UserDTO>(service.FindById(id)));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(mapper.Map<UserDTO>(this.service.Save(mapper.Map<User>(value))));
            }
        }   
     
    }
}
