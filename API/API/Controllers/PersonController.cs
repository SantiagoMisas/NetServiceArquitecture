using API.DTOS;
using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        public readonly IEdit<PersonDto> _iEditPerson;
        public readonly IRead<PersonDto> _iReadPerson;
        public PersonController(
            IEdit<PersonDto> iEditPerson,
            IRead<PersonDto> iReadPerson) 
        {
            _iEditPerson=iEditPerson;
            _iReadPerson=iReadPerson;
        }

        [HttpPost("createPerson")]
        public async Task<IActionResult> createPerson([FromBody] PersonDto personDto)
        {
            if (personDto == null)
            {
                return BadRequest("the person can not be null or empty");
            }
            return await _iEditPerson.createPerson(personDto);
        }

        [HttpPut("updatePerson/{Id}")]
        public async Task<IActionResult> upatePerson(string Id, [FromBody] PersonDto personDto)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest("the person can not be null or empty");
            }
            return await _iEditPerson.updatePerson(Id, personDto);
        }

        [HttpDelete("deletePerson/{Id}")]
        public async Task<IActionResult> createPerson(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest("the person can not be null or empty");
            }
            return await _iEditPerson.deletePerson(Id);
        }

        [HttpGet("getPerson/{Id}")]
        public async Task<IActionResult> getPerson(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest("the person can not be null or empty");
            }
            return await _iReadPerson.getPerson(Id);
        }



    }
}
