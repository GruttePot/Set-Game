using Set_Backend.Models;

namespace Set_Backend.Services;

public class DeckService : IDeckService
{
       
       private readonly ISetService _setService;
       private readonly Random _random = new Random();
       
       public DeckService(ISetService setService)
        {
            _setService = setService;
        }
       
       public async Task<Deck> ShuffleDeckAsync(Deck deck)
       {
           deck.Cards = deck.Cards.OrderBy(_ => _random.Next()).ToList();
           return await Task.FromResult(deck);
       }

       public async Task<Card> DealCardAsync(Deck deck)
       {
           if (deck.Cards.Count == 0)
           {
               throw new InvalidOperationException("No Card left");
           }
        
           var card = deck.Cards[0];
           deck.Cards.RemoveAt(0);
           return await Task.FromResult(card);
       }
       
       public async Task<Card> DrawCardIfNotSetAsync(Deck deck, List<Card> cards)
       {
           var sets = _setService.FindAllSets(cards);

           if (sets.Count == 0)
           {
               return await DealCardAsync(deck);
           }
        
           throw new InvalidOperationException("Valid set, cannot draw card");
       }
} 





         