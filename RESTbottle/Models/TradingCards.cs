namespace RESTbottle.Models
{
    public class TradingCards
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
    }

    
}
