namespace DevFreela.Core.Service
{
    public interface IAuthService
    {
         string GenerateJwtToken(string email, string role);
    }
}