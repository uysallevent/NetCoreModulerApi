using Core.Entities;
using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseModule.Interfaces
{
    public interface IBusinessBase<T>
    where T : class, IEntity, new()
    {
        Task<IDataResult<T>> InsertAsync(T entity);

        Task<IDataResult<IEnumerable<T>>> BulkInsertAsync(IEnumerable<T> entities);

        Task<IDataResult<IEnumerable<T>>> BulkUpdateAsync(IEnumerable<T> entities);

        Task<IDataResult<T>> GetAsync(T entity);

        Task<IDataResult<T>> FindAsync(int Id);

        Task<IDataResult<List<T>>> GetAllAsync(T entity = null);

        Task<IDataResult<T>> UpdateAsync(T entity);

        Task<IDataResult<int>> DeleteAsync(T entity);

        Task<IDataResult<int>> DeleteAsync(int id);

        Task<IDataResult<Dictionary<int, int>>> DeleteAsync(List<int> ids);

        Task<IDataResult<Dictionary<T, int>>> DeleteAsync(List<T> entities);
    }
}