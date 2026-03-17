using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Set_Backend.Models;
using Set_Backend.DTO;
using Set_Backend.Repositories;

namespace Set_Backend.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    
    public PlayerService(IPlayerRepository playerRepository, IConfiguration configuration, IMapper mapper )
    {
        _playerRepository = playerRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<PlayerDTO?> ValidatePlayer(string name, string passwordHash)
    {
        var player = await _playerRepository.GetPlayerByNameAsync(name);
        
        if (player == null || player.PasswordHash != passwordHash)
        {
            return null;
        }
        return _mapper.Map<PlayerDTO>(player);
    }

    public string GenerateJwtToken(PlayerDTO playerDto)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, playerDto.Id.ToString()),
            new Claim(ClaimTypes.Name, playerDto.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            signingCredentials: creds,
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7)
        );
            
        return new JwtSecurityTokenHandler().WriteToken(token);    
    }
}