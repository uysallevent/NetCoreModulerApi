using AuthModule.Interfaces;
using AuthModule.Models;
using BaseModule.Dal;
using Microsoft.EntityFrameworkCore;

namespace AuthModule.Dal
{
    public class UserAccountDal<TContext> : DalBase<UserAccount, TContext>, IUserAccountDal
        where TContext : DbContext, new()
    {
    }
}