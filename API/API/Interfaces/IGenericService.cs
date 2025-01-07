namespace API.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        public Task<List<T>> GetEntitiesList(string Url);

        public Task<T> GetEntity(string Url);
    }
}
