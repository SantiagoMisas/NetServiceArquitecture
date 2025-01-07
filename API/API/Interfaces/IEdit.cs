using Microsoft.AspNetCore.Mvc;
using API.DTOS;

namespace API.Interfaces

{
    public interface IEdit<T> where T : class
    {
        Task<IActionResult> createPerson(T entity);
        Task<IActionResult> updatePerson(string Id, T entity);
        Task<IActionResult> deletePerson(string Id);

    }
}
