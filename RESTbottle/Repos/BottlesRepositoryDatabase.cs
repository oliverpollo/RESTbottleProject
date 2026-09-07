using RESTbottle.Models;

namespace RESTbottle.Repos
{
    public class BottlesRepositoryDatabase : IBottlesRepository
    {
        public Bottle AddBottle(Bottle bottle)
        {
            throw new NotImplementedException();
        }

        public Bottle? DeleteByIdBottle(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bottle> Get(string? nameStartsWith = null)
        {
            throw new NotImplementedException();
        }

        public List<Bottle> GetAllBottles()
        {
            throw new NotImplementedException();
        }

        public Bottle? GetBottleById(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Bottle> GetCopyOfList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bottle> GetV2(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null)
        {
            throw new NotImplementedException();
        }

        public Bottle? UpdateBottle(int id, Bottle updatedBottle)
        {
            throw new NotImplementedException();
        }
    }
}
