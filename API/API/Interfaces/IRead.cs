using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IRead<T> where T : class
    {
        Task<IActionResult> getPerson(string Id);
        Task<IActionResult> getAll(string condition);
        Task<IActionResult> includeCustom(string condition);
        Task<IActionResult> filterParameterCustom(string name, string addressText, string email);
        Task<IActionResult> queryParameterCustom(string? name, string? email, string? addressText);

    }
}
