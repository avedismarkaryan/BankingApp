using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService (AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public AuthResponse? Register(RegisterRequest registerRequest)
    {
        //email daha önce kayıtlı mı diye kontrol.
        if(_context.Users.Any(u=> u.Email == registerRequest.Email))
            return null;
        
        var user = new User
        {
            Email = registerRequest.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),//şifreyi hashleyip saklarız. düz metin olarak tutmayız
            Role = "User"
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        var token = GenerateToken(user);
        return new AuthResponse { Token = token, Email = user.Email };
    }

    public AuthResponse? Login (LoginRequest loginRequest)
    {
        //kullanıcıyı buluruz.
        var user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

        if (user == null)
            return null;
        
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password,user.PasswordHash))
        {//gönderilen şifreyi hash ile karşılaştırırz.
            return null;
        }

        var token = GenerateToken(user);
        return new AuthResponse {Token = token , Email = user.Email};

    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    





}