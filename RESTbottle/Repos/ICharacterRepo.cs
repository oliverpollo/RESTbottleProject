using RESTbottle.Models;
namespace RESTbottle.Repos
{
    public interface ICharacterRepo
    {

        List<Characters> GetAllCharacters();
        Characters? GetCharacterById(int id);
        Characters AddCharacter(Characters character);
        Characters DeleteCharacter(Characters character);
        Characters? DeleteByIdCharacter(int id);
        Characters? UpdateCharacter(int id, Characters character);
    }
}
