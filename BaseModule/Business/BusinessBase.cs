using BaseModule.Dal;
using BaseModule.Interfaces;
using Core.Entities;
using Core.Utilities.ExpressionGenerator;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseModule.Business
{
    public class BusinessBase<T> : ControllerBase, IBusinessBase<T>
    where T : class, IEntity, new()
    {
        private readonly IDalBase<T> _dalBase;

        public BusinessBase(IDalBase<T> dalBase)
        {
            _dalBase = dalBase;
        }

        public virtual async Task<IDataResult<T>> InsertAsync(T entity)
        {
            var result = await _dalBase.AddAsync(entity);
            return new SuccessDataResult<T>(result);
        }

        public virtual async Task<IDataResult<IEnumerable<T>>> BulkInsertAsync(IEnumerable<T> entities)
        {
            var result = await _dalBase.AddRangeAsync(entities);
            return new SuccessDataResult<IEnumerable<T>>(result);
        }

        public virtual async Task<IDataResult<IEnumerable<T>>> BulkUpdateAsync(IEnumerable<T> entities)
        {
            var result = await _dalBase.UpdateRangeAsync(entities);
            return new SuccessDataResult<IEnumerable<T>>(result);
        }

        public virtual async Task<IDataResult<T>> GetAsync(T entity)
        {
            var filter = ExpressionGenerator<T, T>.Generate(entity);
            var result = await _dalBase.GetAsync(filter);
            return new SuccessDataResult<T>(result);
        }

        public virtual async Task<IDataResult<T>> FindAsync(int Id)
        {
            var result = await _dalBase.FindAsync(Id);
            if (result != null)
            {
                return new SuccessDataResult<T>(result);
            }
            return new SuccessDataResult<T>();
        }

        public virtual async Task<IDataResult<List<T>>> GetAllAsync(T entity = null)
        {
            var filter = entity != null ? ExpressionGenerator<T, T>.Generate(entity) : null;
            var result = await _dalBase.GetAllAsync(filter);
            if (result != null && result.Any())
            {
                return new SuccessDataResult<List<T>>(result);
            }
            return new SuccessDataResult<List<T>>();
        }

        public virtual async Task<IDataResult<T>> UpdateAsync(T entity)
        {
            var result = await _dalBase.UpdateAsync(entity);
            return new SuccessDataResult<T>(result);
        }

        public virtual async Task<IDataResult<int>> DeleteAsync(T entity)
        {
            var result = await _dalBase.DeleteAsync(entity);
            return new SuccessDataResult<int>(result);
        }

        public virtual async Task<IDataResult<int>> DeleteAsync(int id)
        {
            var item = await _dalBase.FindAsync(id);
            if (item != null)
            {
                return await DeleteAsync(item);
            }
            else
            {
                return new SuccessDataResult<int>(0);
            }
        }

        public virtual async Task<IDataResult<Dictionary<int, int>>> DeleteAsync(List<int> ids)
        {
            var result = new Dictionary<int, int>();
            foreach (var item in ids)
            {
                var removeResult = await DeleteAsync(item);
                result.Add(item, removeResult.Data);
            }
            return new SuccessDataResult<Dictionary<int, int>>(result);
        }

        public virtual async Task<IDataResult<Dictionary<T, int>>> DeleteAsync(List<T> entities)
        {
            var result = new Dictionary<T, int>();
            foreach (var item in entities)
            {
                var removeResult = await DeleteAsync(item);
                result.Add(item, removeResult.Data);
            }
            return new SuccessDataResult<Dictionary<T, int>>(result);
        }
    }
}