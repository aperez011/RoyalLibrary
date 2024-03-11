using RL.Entity.DTOs;

namespace RL.Utility.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(AuthResponse resp);
    }
}
