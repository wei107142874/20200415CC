using Basic.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Basic.IRepository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> QueryById(object id);

        Task<long> Add(TEntity model);

        Task<int> Add(IEnumerable<TEntity> listEntity);

        Task<bool> DeleteById(long id);

        Task Delete(TEntity model);

        Task<bool> DeleteByIds(long[] ids);

        Task<bool> Update(TEntity model);

        Task<List<TEntity>> Query();

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
    }
}
