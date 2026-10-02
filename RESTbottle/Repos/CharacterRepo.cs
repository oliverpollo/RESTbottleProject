using RESTbottle.Models;

namespace RESTbottle.Repos
{
    public class CharacterRepo : ICharacterRepo
    {
        private readonly List<Characters> _characters = new()
        {
            new Characters  
            {
                Id = 1,
                CharacterName = "Nott",
                Level = 12,
                Class = "Rogue"
            },

             new Characters
            {
                Id = 2,
                CharacterName = "Caleb",
                Level = 20,
                Class = "Wizard"
            },

             new Characters
            {
                Id = 3,
                CharacterName = "Trent",
                Level = 100,
                Class = "Wizard"
            }
        };

        public List<Characters> GetAllCharacters()
        {
            return _characters;
        }

        public Characters? GetCharacterById(int id)
        {
            return _characters.FirstOrDefault(character => character.Id == id);
        }

        public Characters AddCharacter(Characters character)
        {
            character.Id = _characters.Max(c => c.Id) + 1;
            _characters.Add(character);

            return character;
        }

        public Characters? UpdateCharacter(int id, Characters character)
        {
            Characters? existingCharacter =
                _characters.FirstOrDefault(c => c.Id == id);

            if (existingCharacter == null)
            {
                return null;
            }

            existingCharacter.CharacterName = character.CharacterName;
            existingCharacter.Level = character.Level;
            existingCharacter.Class = character.Class;

            return existingCharacter;
        }

        public Characters? DeleteByIdCharacter(int id)
        {
            Characters? character =
                _characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                return null;
            }

            _characters.Remove(character);

            return character;
        }

        public Characters DeleteCharacter(Characters character)
        {
            _characters.Remove(character);

            return character;
        }
    };        
    
}
