using AuthModule.Security.JWT;
using Core.Utilities.Results;

namespace AuthModule.Interfaces
{
    public interface IAuthBusinessBase
    {
        IDataResult<AccessToken> RefreshTokenLogin(int userId, string refreshToken);
    }
}