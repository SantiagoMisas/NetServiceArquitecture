using API.DTOS;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class PersonService: IEdit<PersonDto>, IRead<PersonDto>
    {
        private readonly IGenericRepository<Person, string> _personRepository;

        public PersonService(
            IGenericRepository<Person, string> personRepository) 
        {
            _personRepository=personRepository;
        }
      

        public async Task<IActionResult> createPerson(PersonDto personDto)
        {
            try
            {
                if (personDto != null)
                {
                    var person = fromPersonDtoToPersonEntity(personDto);

                    if (person != null) {

                        await _personRepository.AddAsync(person);
                        await _personRepository.SaveChangesAsync();
                        return new OkObjectResult($"Person has been created {person.Id}");
                    }
                }
                return new BadRequestObjectResult("Invalid data");
            }
            catch (Exception error)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> deletePerson(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return new BadRequestObjectResult("Invalid data");
            }

            var foundPerson = await _personRepository.GetByIdAsync(Id);

            if (foundPerson == null)
            {
                return new NotFoundObjectResult($"Person has not be found with the id {Id}");
            }

            await _personRepository.DeleteAsync(foundPerson.Id.ToString());
            await _personRepository.SaveChangesAsync();

            return new OkObjectResult($"The Person has been deleted {Id}"); ;
        }

        public Person fromPersonDtoToPersonEntity(PersonDto personDto) {

            var person = new Person
            {
                Id=Guid.NewGuid().ToString(),
                Name=personDto.Name,
                Email=personDto.Email,
                PhoneNumber=personDto.PhoneNumber,
                AddressId = personDto.AddressId
            };

            return person;
        }

        public PersonDto fromPersonEntityToPersonDto(Person person)
        {

            var personDto = new PersonDto
            {
                Id=person.Id,
                Name=person.Name,
                Email=person.Email,
                PhoneNumber=person.PhoneNumber,
                AddressId = person.AddressId
            };

            return personDto;
        }

        public Task<IActionResult> getAll(string condition)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> getPerson(string Id)
        {
            if (string.IsNullOrEmpty(Id)) {
                return new BadRequestObjectResult("Invalid data");
            }

            try { 
            
            var foundPerson = await _personRepository.GetByIdAsync(Id);
                if (foundPerson == null) {
                    return new NotFoundObjectResult($"The person has not be found {Id}");
                }
                else {
                
                PersonDto personDto = fromPersonEntityToPersonDto(foundPerson);
                return new OkObjectResult(personDto);
                
                }
            } catch (Exception error) {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> updatePerson(string Id, PersonDto personDto)
        {
                if (personDto == null || string.IsNullOrEmpty(Id)) {
                    return new BadRequestObjectResult($"Invalid data");
                }
                try
                {       
                    var foundPerson = await _personRepository.GetByIdAsync(Id);

                if (foundPerson == null) 
                {
                    return new NotFoundObjectResult($"The person has not be found {Id}");
                }
                foundPerson.Name = personDto.Name;
                foundPerson.Email = personDto.Email;
                foundPerson.PhoneNumber = personDto.PhoneNumber;
                foundPerson.AddressId = personDto.AddressId;

                await _personRepository.UpdateAsync(foundPerson); 
                await _personRepository.SaveChangesAsync();

                return new OkObjectResult($"Person has been updated {Id}");
                    
                } catch (Exception error) {
                    return new StatusCodeResult(500);
                }
        }
    }
}
