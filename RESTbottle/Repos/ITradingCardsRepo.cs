using RESTbottle.Models;
namespace RESTbottle.Repos
{
    public interface ITradingCardsRepo
    {
        List<TradingCards> GetAllTradingCards();
        TradingCards? GetTradingCardById(int id);
        TradingCards AddTradingCard(TradingCards card);
        TradingCards DeleteTradingCard(TradingCards card);
        TradingCards? DeleteByIdTradingCard(int id);
        TradingCards? UpdateTradingCard(int id, TradingCards card);

    }
}
