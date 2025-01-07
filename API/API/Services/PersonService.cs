using API.Dtos;
using API.DTOS;
using API.Interfaces;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class PersonService: IEdit<PersonDto>, IRead<PersonDto>
    {
        private readonly IGenericRepository<Person, string> _personRepository;
        private readonly IGenericRepository<Address, string> _addressRepository;

        public PersonService(
            IGenericRepository<Person, string> personRepository, IGenericRepository<Address, string> addressRepository)
        {
            _personRepository=personRepository;
            _addressRepository=addressRepository;
        }


        public async Task<IActionResult> createPerson(PersonDto personDto)
        {
            try
            {
                if (personDto != null)
                {
                    var person = fromPersonDtoToPersonEntity(personDto);
                    Console.WriteLine("creation mapeo person-------------", person);

                    if (person.Address != null)
                    {
                        Console.WriteLine("creation mapeada address-------------", person.Address);
                        await _addressRepository.AddAsync(person.Address);
                        await _addressRepository.SaveChangesAsync();
                    }

                    if (person != null) {

                        await _personRepository.AddAsync(person);
                        await _personRepository.SaveChangesAsync();
                        return new OkObjectResult($"Person has been created {person.Id}");
                    }
                }
                return new BadRequestObjectResult("Invalid data");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                return new ObjectResult(new { message = "An error occurred", details = ex.Message })
                {
                    StatusCode = 500
                };
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

        public Person fromPersonDtoToPersonEntity(PersonDto personDto)
        {
            Address address = null; 

            if (personDto != null && personDto.Address != null)
            {
                address = new Address
                {
                    Id=Guid.NewGuid().ToString(),
                    AddressText = personDto.Address.AddressText,
                    Latitude = personDto.Address.Latitude,
                    Longitude = personDto.Address.Longitude
                };
            }

            var person = new Person
            {
                Id=Guid.NewGuid().ToString(),
                Name=personDto.Name,
                Email=personDto.Email,
                PhoneNumber=personDto.PhoneNumber,
                AddressId = address?.Id, 
                Address = address
            };

            return person;
        }

        public PersonDto fromPersonEntityToPersonDto(Person person)
        {
            AddressDto address = null;

            if (person != null && person.Address != null)
            {

                address = new AddressDto
                {
                    Id=person.AddressId,
                    AddressText = person.Address.AddressText,
                    Latitude = person.Address.Latitude,
                    Longitude = person.Address.Longitude
                };
            }
            var personDto = new PersonDto
            {
                Id=person.Id,
                Name=person.Name,
                Email=person.Email,
                PhoneNumber=person.PhoneNumber,
                AddressId = person.AddressId,
                Address = address
            };

            return personDto;
        }

        public async Task<IActionResult> getAll(string condition)
        {
            if (string.IsNullOrEmpty(condition)) {

                return new BadRequestObjectResult("Condition cannot be null or empty.");
            }

            try 
            {
                Console.WriteLine($"Condicion De Busqueda",condition);
                var filteredPeople = (await _personRepository
                     .Include(a => a.Address) 
                     .Where(a => EF.Functions.Like(a.Name, $"%{condition}%") && a.Address != null)
                     .ToListAsync())
                     .Select(a => fromPersonEntityToPersonDto(a));

                // EJEMPLO CON FILTER PARAMETER
                //var parameters = new FilterParameter<Person>
                //{
                //    Filter = a => a.Name.Contains(condition) && a.Address != null,
                //    Include = query => query.Include(a => a.Address)
                //};

                //var filteredPeople = (await _personRepository.GetFilteredAsync(parameters))
                //    .Select(a => fromPersonEntityToPersonDto(a))
                //    .ToList();


                if (filteredPeople == null)
                {
                    return new NotFoundObjectResult("Not found");
                };

                return new OkObjectResult(filteredPeople);
            } catch (Exception error) {

                return new StatusCodeResult(500);
            }
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
