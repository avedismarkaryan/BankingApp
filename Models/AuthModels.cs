public class RegisterRequest
{
    //RegisterRequest: client kayıt olurken gönderir (email + şifre)
    public string Email {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;
}

public class LoginRequest
{
    //LoginRequest: client giriş yaparken gönderir (email + şifre)
    public string Email {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;
}

public class AuthResponse
{
    //AuthResponse: API'nin client'a döndüğü cevap (token + email)
    public string Token {get;set;} = string.Empty;
    public string Email {get;set;} = string.Empty;
}
