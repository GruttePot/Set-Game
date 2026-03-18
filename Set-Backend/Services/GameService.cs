using Set_Backend.Repositories;
using Set_Backend.Models;

namespace Set_Backend.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly Random _random = new Random();
    
    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        return await _gameRepository.GetGamesAsync();
    }
    
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _gameRepository.GetGameByIdAsync(id);
    }
    
    public async Task<Game> CreateGameAsync(Game game)
    {
        return await _gameRepository.CreateGameAsync(game);
    }
    
    public async Task<Game> UpdateGameAsync(Game game)
    {
        return await _gameRepository.UpdateGameAsync(game);
    }
    
    public async Task DeleteGameAsync(Game game)
    {
        await _gameRepository.DeleteGameAsync(game);
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
    
    public async Task<bool> ValidateSetAsync(List<Card> cards)
    {
        if (cards.Count != 3)
        {
            return await Task.FromResult(false);
        }
        
        return await Task.FromResult(IsValidSet(cards[0], cards[1], cards[2]));
    }

    public bool IsValidSet(Card card1, Card card2, Card card3)
    {
        return IsAttributeValid(card1.Colour, card2.Colour, card3.Colour) &&
               IsAttributeValid(card1.Shape, card2.Shape, card3.Shape) &&
               IsAttributeValid(card1.Filling, card2.Filling, card3.Filling) &&
               IsAttributeValid(card1.Number, card2.Number, card3.Number);
    }

    private bool IsAttributeValid<T>(T attr1, T attr2, T attr3)
    {
        var attributes = new[] { attr1, attr2, attr3 };
        var distinctCount = attributes.Distinct().Count();
        
        return distinctCount == 1 || distinctCount == 3;
    }

    public Task<List<Card>> FindAvailableSetsAsync(Deck deck)
    {
        var sets = FindAllSets(deck.Cards);
        
        if (sets.Count > 0)
        {
            return Task.FromResult(sets.SelectMany(set => set).ToList());
        }
        
        return Task.FromResult(new List<Card>());
    }
    
    public async Task<List<Card>> GetHintAsync(Deck deck)
    {
        var sets = FindAllSets(deck.Cards);
        
        if (sets.Count > 0)
        {
            return await Task.FromResult(sets[_random.Next(sets.Count)].ToList());
        }
        
        return await Task.FromResult(new List<Card>());
    }

    public async Task<Card> DrawCardIfNotSetAsync(Deck deck, List<Card> cards)
    {
        if (!IsValidSet(cards[0], cards[1], cards[2]))
        {
            return await DealCardAsync(deck);
        }
        
        throw new InvalidOperationException("Valid set, cannot draw card");
    }

    public List<Card[]> FindAllSets(List<Card> tableCards)
    {
        var validSets = new List<Card[]>();
        var cardCount = tableCards.Count;

        for (int i = 0; i < cardCount - 2; i++)
        {
            for (int j = i + 1; j < cardCount - 1; j++)
            {
                for (int k = j + 1; k < cardCount; k++)
                {
                    if (IsValidSet(tableCards[i], tableCards[j], tableCards[k]))
                    {
                        validSets.Add(new[] { tableCards[i], tableCards[j], tableCards[k] });
                    }
                }
            }
        }
        return validSets;
    }
}