using RESTbottle.Models;
using System.Collections.ObjectModel;
using System.Xml.XPath;

namespace RESTbottle.Repos
{
    public class BottlesRepositoryList : IBottlesRepository
    {

        private List<Bottle> _bottles = new List<Bottle>();

        private int _nextId = 1;

        public BottlesRepositoryList(bool includesTestData = true)
        {
            if (includesTestData)
            {
                AddBottle(new Bottle { Volume = 500, Name = "A Test Bottle 1" });
                AddBottle(new Bottle { Volume = 750, Name = " BTest Bottle 2" });
                AddBottle(new Bottle { Volume = 1000, Name = "C Test Bottle 3" });
            }
        }
        public IEnumerable<Bottle> Get(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null)
        {
            IEnumerable<Bottle> result = _bottles.ToList();

            if (minVolume != null)
            {
                result = result.Where(b => b.Volume >= minVolume);
            }

            if (nameStartsWith != null)
            {
                result = result.Where(b => b.Name != null && b.Name.StartsWith(nameStartsWith));
            }

            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                // No sort requested — return filtered result as-is
                return result;
            }

            var so = sortOrder!.Trim().ToLowerInvariant();
            switch (so)
            {
                case "name":
                case "name_asc":
                    result = result.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    result = result.OrderByDescending(b => b.Name);
                    break;
                case "volume":
                case "volume_asc":
                    result = result.OrderBy(b => b.Volume);
                    break;
                case "volume_desc":
                    result = result.OrderByDescending(b => b.Volume);
                    break;
                default:
                    throw new ArgumentException($"Invalid sortOrder value: {sortOrder}. Valid values are: name_asc, name_desc, volume_asc, volume_desc.");
            }

            return result;
        }

        public IEnumerable<Bottle> GetV2(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null)
        {
            IEnumerable<Bottle> result = _bottles.ToList();

            if (minVolume != null)
            {
                result = result.Where(b => b.Volume >= minVolume);
            }

            if (nameStartsWith != null)
            {
                result = result.Where(b => b.Name != null && b.Name.StartsWith(nameStartsWith));
            }

            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                // No sort requested — return filtered result as-is
                return result;
            }

            var so = sortOrder!.Trim().ToLowerInvariant();
            switch (so)
            {
                case "name":
                case "name_asc":
                    result = result.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    result = result.OrderByDescending(b => b.Name);
                    break;
                case "volume":
                case "volume_asc":
                    result = result.OrderBy(b => b.Volume);
                    break;
                case "volume_desc":
                    result = result.OrderByDescending(b => b.Volume);
                    break;
                default:
                    throw new ArgumentException($"Invalid sortOrder value: {sortOrder}. Valid values are: name_asc, name_desc, volume_asc, volume_desc.");
            }

            return result;
        }

        public IReadOnlyCollection<Bottle> GetCopyOfList()
        {
            return _bottles.AsReadOnly(); // Return a read-only copy of the list 
        }

        public List<Bottle> GetAllBottles()
        {
            return _bottles.ToList();
        }

        public Bottle AddBottle(Bottle bottle)
        {
            bottle.Id = _nextId++;
            _bottles.Add(bottle);
            return bottle;
        }

        public Bottle? GetBottleById(int id)
        {
            return _bottles.FirstOrDefault(b => b.Id == id);
        }


        public Bottle? DeleteByIdBottle(int id)
        {
            Bottle? bottleToDelete = GetBottleById(id);
            if (bottleToDelete != null)
            {
                _bottles.Remove(bottleToDelete);
            }
            return bottleToDelete;
        }

        public Bottle? UpdateBottle(int id, Bottle updatedBottle)
        {
            Bottle? bottle = GetBottleById(id);

            if (bottle != null)
            {

                bottle.Volume = updatedBottle.Volume;
                bottle.Name = updatedBottle.Name;

            }
            return bottle;
        }
        public Bottle DeleteBottle(Bottle bottle)
        {
            _bottles.Remove(bottle);
            return bottle;
        }

        public IEnumerable<Bottle> Get(string? nameStartsWith = null)
        {
            throw new NotImplementedException();
        }
    }
}
