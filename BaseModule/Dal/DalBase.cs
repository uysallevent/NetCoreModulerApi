using Core.DataAccess.EntityFramework;
using Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace BaseModule.Dal
{
    public class DalBase<TEntity, TContext> : EfEntityRepositoryBase<TEntity, TContext>, IDalBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public DateTime GetSqlServerUtcNow()
        {
            using (var context = new TContext())
            using (var connection = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select SYSUTCDATETIME()";
                return (DateTime)command.ExecuteScalar();
            }
        }

    }
}
