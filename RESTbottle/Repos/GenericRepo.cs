using RESTbottle.Models;

namespace RESTbottle.Repos
{
    public class GenericRepo<T> : IRepository<T> where T : class
    {

        private List <T> _entities = new List<T>();
        public IReadOnlyList<T> GetReadOnlyRepo()
        {
            return _entities.AsReadOnly();
        }

        public Task Add(T entity)
        {
            _entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            _entities.RemoveAt(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> Get(int? birthYearBefore, int? birthYearAfter, string? name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            _entities = _entities.OrderBy(e => e.GetType().GetProperty("Id")?.GetValue(e)).ToList();
            return Task.FromResult(_entities.AsEnumerable());
        }

        public Task<T> GetById(int id)
        {
            if (id != null && id >= 0 && id < _entities.Count)
            {
                return Task.FromResult(_entities[id]);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid ID");
            }
        }

        public Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null) throw new InvalidOperationException("Entity type does not have an 'Id' property.");

            var idValue = idProp.GetValue(entity);
            if (idValue == null) throw new InvalidOperationException("'Id' value is null.");

            int id;
            try
            {
                id = Convert.ToInt32(idValue);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("'Id' value could not be converted to int.", ex);
            }

            var index = _entities.FindIndex(e =>
            {
                var v = idProp.GetValue(e);
                return v != null && Convert.ToInt32(v) == id;
            });

            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found.");
            }

            _entities[index] = entity;
            return Task.CompletedTask;
        }

        Task<IEnumerable<T>> IRepository<T>.GetRepo()
        {
            return Task.FromResult(_entities.AsEnumerable());
        }
    }
}
