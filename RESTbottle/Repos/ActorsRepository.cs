using RESTbottle.Models;

namespace RESTbottle.Repos
{
    public class ActorsRepository
    {
        private List<Actor> _actors = new List<Actor>();

        private int _nextId = 1;

        public IReadOnlyCollection<Actor> GetCopyOfList()
        {
            return _actors.AsReadOnly(); // Return a read-only copy of the list 
        }

        public List<Actor> Get(int? birthYearBefore, int? birthYearAfter, string? name)
        {
            var query = _actors.AsQueryable();

            if (birthYearBefore.HasValue)
            {
                query = query.Where(a => a.BirthYear.Year < birthYearBefore.Value);
            }

            if (birthYearAfter.HasValue)
            {
                query = query.Where(a => a.BirthYear.Year > birthYearAfter.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }

            return query.ToList();
        }

        public Actor? GetActorById(int id)
        {
            return _actors.FirstOrDefault(a => a.Id == id);
        }

        public Actor AddNewActor(Actor actor)
        {
            actor.Id = _nextId++;
            _actors.Add(actor);
            return actor;
        }

        public Actor? DeleteActorById(int id) {
            var actor = GetActorById(id);
            if (actor != null) {
                _actors.Remove(actor);
            }
            return actor;
        } 

        public Actor? UpdateActor(int id, Actor updatedActor)
        {
            var actor = GetActorById(id);
            if (actor != null)
            {
                actor.Name = updatedActor.Name;
                actor.BirthYear = updatedActor.BirthYear;
            }
            return actor;
        }  
    }
}
