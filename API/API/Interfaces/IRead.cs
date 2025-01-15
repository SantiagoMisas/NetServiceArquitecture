using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IRead<T> where T : class
    {
        Task<IActionResult> getPerson(string Id);
        Task<IActionResult> getAll(string condition);
        Task<IActionResult> getAllCustom(string condition);
    }
}
