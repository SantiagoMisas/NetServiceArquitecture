using System.Diagnostics;
using System.Text.Json;
using API.Interfaces;

namespace API.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public GenericService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<T>> GetEntitiesList(string url)
        {
            var entities = new List<T>();
            Uri uri = new Uri(url);

            Debug.WriteLine($"Requesting: {uri}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Response: {content}");
                    entities = JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
                else
                {
                    Debug.WriteLine($"No se han encontrado los datos. Código de estado: {response.StatusCode}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"HTTP Request Exception: {httpEx.Message}");
                throw;
            }
            catch (TaskCanceledException tcEx)
            {
                Debug.WriteLine($"HTTP Request Timed Out: {tcEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Se ha producido un error procesando tu solicitud: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }

            return entities;
        }

        public async Task<T> GetEntity(string url)
        {
            T entity = null;
            Uri uri = new Uri(url);

            Debug.WriteLine($"Requesting: {uri}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Response: {content}");
                    entity = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                }
                else
                {
                    Debug.WriteLine($"No se han encontrado los datos. Código de estado: {response.StatusCode}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"HTTP Request Exception: {httpEx.Message}");
                throw;
            }
            catch (TaskCanceledException tcEx)
            {
                Debug.WriteLine($"HTTP Request Timed Out: {tcEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Se ha producido un error procesando tu solicitud: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }

            return entity;
        }
    }
}