
using System.Security.Claims;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface ITokenService
    {
        public string GenerateToken(IList<Claim> claims);
    }
}
