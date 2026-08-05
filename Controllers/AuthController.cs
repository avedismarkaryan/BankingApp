using Microsoft.AspNetCore.Mvc;

[ApiController] //Bu bir attribute (C#'ta köşeli parantezle yazılan, sınıf veya metoda ek davranış kazandıran etiketler).
[Route("api/auth")]

public class AuthController : ControllerBase
{
    //Ctrl'de iki endpoint olacak 1- POST /api/auth/register (kayıt ol) ve POST /api/auth/login (giriş yap)

    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public IActionResult Register (RegisterRequest registerRequest)
    {
        if (string.IsNullOrWhiteSpace(registerRequest.Email))
            return BadRequest();
        if (string.IsNullOrWhiteSpace(registerRequest.Password))
            return BadRequest();
        
        var result = _service.Register(registerRequest);
        if (result == null)
            return BadRequest();

        return Ok(result);
    }

    [HttpPost("login")]
    public IActionResult Login (LoginRequest loginRequest)
    {
        var result = _service.Login(loginRequest);
        if (result == null)
            return Unauthorized();

        return Ok(result);
    }



}