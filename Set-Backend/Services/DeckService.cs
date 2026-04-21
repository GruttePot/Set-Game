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
       
       public async Task<List<Card>> ShuffleDeckAsync(List<Card> cards)
       {
           var shuffled = cards.OrderBy(_ => _random.Next()).ToList();
           return await Task.FromResult(shuffled);
       }

       public async Task<Card> DealCardAsync(List<Card> deck)
       {
           if (deck.Count == 0)
           {
               throw new InvalidOperationException("No Card left");
           }
        
           var card = deck[0];
           deck.RemoveAt(0);
           return await Task.FromResult(card);
       }
       
       public async Task<Card> DrawCardIfNotSetAsync(List<Card> deck, List<Card> cards)
       {
           var sets = _setService.FindAllSets(cards);

           if (sets.Count == 0)
           {
               return await DealCardAsync(deck);
           }
        
           throw new InvalidOperationException("Valid set, cannot draw card");
       }
} 





         