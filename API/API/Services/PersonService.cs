using API.ApplicationDb;
using API.Dtos;
using API.DTOS;
using API.Interfaces;
using API.Models;
using API.Repositories;
using API.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class PersonService: IEdit<PersonDto>, IRead<PersonDto>
    {
        private readonly IGenericRepository<Person, string> _personRepository;
        private readonly IGenericRepository<Address, string> _addressRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly Logger _logger;


        public PersonService(
            IGenericRepository<Person, string> personRepository, 
            IGenericRepository<Address, string> addressRepository,
            ApplicationDbContext context,
            Logger logger,
            IMapper mapper)
        {
            _personRepository=personRepository;
            _addressRepository=addressRepository;
            _context=context;
            _logger=logger;
            _mapper=mapper;

        }


        public async Task<IActionResult> createPerson(PersonDto personDto)
        {
            try
            {
                if (personDto != null)
                {
                    //var person = fromPersonDtoToPersonEntity(personDto);
                    Person person = _mapper.Map<Person>(personDto);

                    _logger.getMessage("creation mapeo person-------------", person);

                    if (person.Address != null)
                    {
                        _logger.getMessage("creation mapeada address-------------", person.Address);
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
                _logger.getMessage($"Error: {ex.Message}\n{ex.StackTrace}");
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
            try
            {

                var foundPerson = await _personRepository.GetByIdAsync(Id);

                if (foundPerson == null)
                {
                    return new NotFoundObjectResult($"Person has not be found with the id {Id}");
                }

                await _personRepository.DeleteAsync(foundPerson.Id.ToString());
                await _personRepository.SaveChangesAsync();

                return new OkObjectResult($"The Person has been deleted {Id}"); ;
            }
            catch (Exception ex)
            {
                _logger.getMessage(ex);
                return new StatusCodeResult(500);
            }
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
                _logger.getMessage($"Condicion De Busqueda",condition);
                var filteredPeople = (await _personRepository
                     .Include(a => a.Address) 
                     .Where(a => EF.Functions.Like(a.Name, $"%{condition}%") && a.Address != null)
                     .ToListAsync())
                     .Select(a => _mapper.Map<PersonDto>(a));

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

                _logger.getMessage(error.Message);
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> getAllCustom(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {

                return new BadRequestObjectResult("Condition cannot be null or empty.");
            }
            try {

                List<PersonDto> foundPeople = await _context.Person
                    .Where(e => e.Address != null && e.Email == condition.ToLower())
                    .Include(p => p.Address)
                    .Select(m => _mapper.Map<PersonDto>(m))
                    .ToListAsync();

                if (foundPeople == null)
                {
                    return new NotFoundObjectResult("Not found");
                }

                return new OkObjectResult(foundPeople);

            } 
            
            catch (Exception ex) {

                _logger.getMessage(ex);
                return new StatusCodeResult(500);
            
            }
        }


        public async Task<IActionResult> getPerson(string Id)
        {
            if (string.IsNullOrEmpty(Id)) {
                return new BadRequestObjectResult("Invalid data");
            }

            try {

                Person? foundPerson = (await _personRepository.GetFilteredAsync(new FilterParameter<Person>
                {
                    Filter = a => a.Id == Id && a.Address != null,
                    Include = query => query.Include(a => a.Address)
                })).FirstOrDefault();

                if (foundPerson == null) {
                    return new NotFoundObjectResult($"The person has not be found {Id}");
                }
                else {
                
                PersonDto personDto = _mapper.Map<PersonDto>(foundPerson);
                return new OkObjectResult(personDto);
                
                }
            } catch (Exception error) {

                _logger.getMessage(error.Message);
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

                await _personRepository.UpdateAsync(foundPerson); 
                await _personRepository.SaveChangesAsync();

                    if (personDto.Address != null) {
                        if (foundPerson.Address != null)
                        {
                            foundPerson.Address.AddressText = personDto.Address.AddressText;
                            foundPerson.Address.Latitude = personDto.Address.Latitude;
                            foundPerson.Address.Longitude = personDto.Address.Longitude;
                            await _addressRepository.UpdateAsync(foundPerson.Address);
                            await _addressRepository.SaveChangesAsync();
                        }
                        else
                        {
                            Address newAddress = new Address
                            {
                                Id = Guid.NewGuid().ToString(),
                                AddressText = personDto.Address.AddressText,
                                Latitude = personDto.Address.Latitude,
                                Longitude = personDto.Address.Longitude
                            };

                            await _addressRepository.AddAsync(newAddress);
                            await _addressRepository.SaveChangesAsync();

                            foundPerson.AddressId = newAddress.Id;
                            foundPerson.Address = newAddress;
                        }
                    }
                            await _personRepository.UpdateAsync(foundPerson);
                            await _personRepository.SaveChangesAsync();

                return new OkObjectResult($"Person has been updated {Id}");

                } catch (Exception error) {
                _logger.getMessage(error.Message);
                    return new StatusCodeResult(500);
                }
        }
    }
}
