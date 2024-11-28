using HomeInc.Domain.Entities;
using HomeInc.Domain.Interfaces;
using HomeInc.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HomeInc.Application.Services
{
    public class AuthenticateServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateServices(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Método para autenticar al usuario y generar un JWT
        public async Task<string> Authenticate(string userName, string password)
        {
            var usuario = await _userRepository.GetUserByUsernameAsync(userName);

            if (usuario == null || !VerifyPasswordHash(password, usuario.Password))
            {
                return null;
            }

            var token = GenerateJwtToken(usuario);
            return token;
        }

        // Método para verificar el hash de la contraseña
        private bool VerifyPasswordHash(string enteredPassword, string storedPasswordHash)
        {
            return enteredPassword == storedPasswordHash;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: _configuration["JwtSettings:Issuer"],
             audience: _configuration["JwtSettings:Audience"],
             claims: claims,
             expires: DateTime.Now.AddHours(1),
             signingCredentials: creds
         );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
