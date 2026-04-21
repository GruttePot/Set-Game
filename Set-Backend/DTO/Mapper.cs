using AutoMapper;
using Set_Backend.Models;

namespace Set_Backend.DTO;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Game, GameDTO>().ReverseMap();
        CreateMap<Player, PlayerDTO>().ReverseMap();
        CreateMap<Card, CardDTO>().ReverseMap();
        CreateMap<FoundSet, FoundSetDTO>().ReverseMap();
    }      
}