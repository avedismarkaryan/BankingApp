public interface IAuthService
{
    AuthResponse? Register(RegisterRequest registerRequest);
    AuthResponse? Login(LoginRequest loginRequest);
}