using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Data.Entities;
using Tester.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Tester.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task RegisterAsync(UserModel userModel)
    {
        var existingUser = await _userRepository
            .GetQueryable()
            .FirstOrDefaultAsync(u => u.Login == userModel.Login);

        if (existingUser != null)
            throw new Exception("User already exists");

        userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

        var user = _mapper.Map<User>(userModel);
        await _userRepository.CreateAsync(user);
    }

    public async Task<AuthModel?> AuthenticateAsync(LoginModel loginRequest)
    {
        var user = await _userRepository
            .GetQueryable()
            .FirstOrDefaultAsync(u => u.Login == loginRequest.Login);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            return null;

        var token = GenerateJwtToken(user);

        var authModel = _mapper.Map<AuthModel>(user);

        return authModel;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

