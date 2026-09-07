using Microsoft.AspNetCore.SignalR;

namespace RESTbottle.Repos
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetRepo();
        Task<IEnumerable<T>> Get(int? birthYearBefore, int? birthYearAfter, string? name);

        Task<T> GetById (int id); 

        Task<IEnumerable<T>> GetAll();

        Task Add(T entity);
        Task Delete (int id);
        Task Update (T entity);
    }
}
