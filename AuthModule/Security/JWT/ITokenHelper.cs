using AuthModule.Dtos;
using AuthModule.Models;
using System.Collections.Generic;

namespace AuthModule.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(UserAccount user, List<OperationClaim> operationClaims);
    }
}