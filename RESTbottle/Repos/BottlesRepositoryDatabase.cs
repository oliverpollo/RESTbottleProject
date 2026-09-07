using RESTbottle.EFCore;
using RESTbottle.Models;
using System.Runtime.CompilerServices;

namespace RESTbottle.Repos
{
    public class BottlesRepositoryDatabase : IBottlesRepository
    {

        private BottlesDBContext _context;
        public Bottle AddBottle(Bottle bottle)
        {
            _context.Bottles.Add(bottle);
            _context.SaveChanges();
            return bottle;
        }

        public Bottle? DeleteByIdBottle(int id)
        {
            var bottle = _context.Bottles.Find(id);
            if (bottle != null)
            {
                _context.Bottles.Remove(bottle);
                _context.SaveChanges();
            }
            return bottle;
        }

        public IEnumerable<Bottle> Get(string? nameStartsWith = null)
        {
            return _context.Bottles.Where(b => nameStartsWith == null || b.Name.StartsWith(nameStartsWith)).ToList();
        }

        public List<Bottle> GetAllBottles()
        {
            return _context.Bottles.ToList();
        }

        public Bottle? GetBottleById(int id)
        {
            return _context.Bottles.FirstOrDefault() ?? null;
        }

        public IReadOnlyCollection<Bottle> GetCopyOfList()
        {
            return _context.Bottles.ToList().AsReadOnly();
        }

        public IEnumerable<Bottle> GetV2(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null)
        {
            return _context.Bottles
                .Where(b => (nameStartsWith == null || b.Name.StartsWith(nameStartsWith)) &&
                            (minVolume == null || b.Volume >= minVolume))
                .OrderBy(b => sortOrder == "desc" ? -b.Volume : b.Volume)
                .ToList();
        }

        public Bottle? UpdateBottle(int id, Bottle updatedBottle)
        {
            throw new NotImplementedException();
        }
    }
}
