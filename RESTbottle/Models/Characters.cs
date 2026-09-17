namespace RESTbottle.Models
{
    public class Characters
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int Level { get; set; }
        public string Class { get; set; } = string.Empty;
    }
}
