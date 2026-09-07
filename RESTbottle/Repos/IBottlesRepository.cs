using RESTbottle.Models;

namespace RESTbottle.Repos
{
    public interface IBottlesRepository
    {
        Bottle AddBottle(Bottle bottle);
        Bottle? DeleteByIdBottle(int id);
        IEnumerable<Bottle> Get(string? nameStartsWith = null);
        List<Bottle> GetAllBottles();
        Bottle? GetBottleById(int id);
        IReadOnlyCollection<Bottle> GetCopyOfList();
        IEnumerable<Bottle> GetV2(string? nameStartsWith = null, double? minVolume = null, string? sortOrder = null);
        Bottle? UpdateBottle(int id, Bottle updatedBottle);
    }
}