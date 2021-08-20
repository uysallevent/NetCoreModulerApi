using AuthModule.Interfaces;
using AuthModule.Models;
using BaseModule.Dal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AuthModule.Dal
{
    public class UserTokenDal<TContext> : DalBase<UserToken, TContext>, IUserTokenDal
        where TContext : DbContext, new()
    {
        public UserToken GetUserAndRefreshToken(int userId, string refreshToken)
        {
            using (var context = new TContext())
            {
                var result = from ut in context.Set<UserToken>()
                             join ua in context.Set<UserAccount>() on ut.UserId equals ua.Id
                             where ut.Status == 1 && ut.UserId == userId && ut.RefreshToken == refreshToken
                             select new UserToken()
                             {
                                 Id = ut.Id,
                                 RefreshToken = refreshToken,
                                 RefreshTokenExpirationDate = ut.RefreshTokenExpirationDate,
                                 Status = ut.Status,
                                 UserId = ut.UserId,
                                 UserAccount = ua
                             };
                return result.FirstOrDefault();
            }
        }

        public int DisableActiveRecord(int userId)
        {
            using (var context = new TContext())
            using (var connection = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Auth.UserToken SET Status=0 WHERE UserId = @UserId";
                command.Parameters.AddWithValue("UserId", userId);
                return command.ExecuteNonQuery();
            }
        }
    }
}