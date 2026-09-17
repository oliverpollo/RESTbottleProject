using RESTbottle.Models;

namespace RESTbottle.Repos
{
    public class TradingCardRepo : ITradingCardsRepo
    {
        private readonly List<TradingCards> _cards = new()
        {
            new TradingCards
            {
                Id = 1,
                PlayerName = "Harry Kane",
                Attack = 100,
                Defense = 27,
                TeamName = "Bayern",
                Rarity = "Legendary"
            },

            new TradingCards
            {
                Id = 2,
                PlayerName = "Michael Olise",
                Attack = 97,
                Defense = 14,
                TeamName = "Bayern",
                Rarity = "Rare"
            },

            new TradingCards
            {
                Id = 3,
                PlayerName = "Luis Diaz",
                Attack = 85,
                Defense = 50,
                TeamName = "Bayern",
                Rarity = "Epic"
            }
        };

        public List<TradingCards> GetAllTradingCards()
        {
            return _cards;
        }

        public TradingCards? GetTradingCardById(int id)
        {
            return _cards.FirstOrDefault(card => card.Id == id);
        }

        public TradingCards AddTradingCard(TradingCards card)
        {
            card.Id = _cards.Max(c => c.Id) + 1;
            _cards.Add(card);

            return card;
        }

        public TradingCards? UpdateTradingCard(int id, TradingCards card)
        {
            TradingCards? existingCard =
                _cards.FirstOrDefault(c => c.Id == id);

            if (existingCard == null)
            {
                return null;
            }

            existingCard.PlayerName = card.PlayerName;
            existingCard.Attack = card.Attack;
            existingCard.Defense = card.Defense;
            existingCard.TeamName = card.TeamName;
            existingCard.Rarity = card.Rarity;

            return existingCard;
        }

        public TradingCards? DeleteByIdTradingCard(int id)
        {
            TradingCards? card =
                _cards.FirstOrDefault(c => c.Id == id);

            if (card == null)
            {
                return null;
            }

            _cards.Remove(card);

            return card;
        }

        public TradingCards DeleteTradingCard(TradingCards card)
        {
            _cards.Remove(card);

            return card;
        }
    }
}
