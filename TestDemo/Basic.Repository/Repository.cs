using Basic.Common;
using Basic.Common.Seesion;
using Basic.Entites;
using Basic.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Basic.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly EFDbContext _dbContext;
        private readonly AppSession _appSession;

        public Repository(EFDbContext dbContext, AppSession appSession)
        {
            this._dbContext = dbContext;
            this._appSession = appSession;
        }

        /// <summary>
        /// 添加操作默认设置
        /// </summary>
        private void AddDefaultSet(TEntity entity)
        {
            DateTime now = DateTime.Now;
            entity.CreationTime = now;
            entity.ModificationTime = now;
            entity.CreationId = _appSession.UserId;
            entity.ModificationId = _appSession.UserId;
            entity.DeleteId = 0;
            entity.IsDelete = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>id</returns>
        public async Task<long> Add(TEntity entity)
        {
            AddDefaultSet(entity);

            var m= await _dbContext.AddAsync(entity);
            int i= await _dbContext.SaveChangesAsync();
            return m.Entity.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Add(IEnumerable<TEntity> listEntity)
        {
            await _dbContext.AddRangeAsync(listEntity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(long id)
        {
            var entity =await _dbContext.Set<TEntity>().FirstAsync(x => x.Id == id);
            _dbContext.Remove(entity);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteByIds(long[] ids)
        {
            var entity =await _dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
            _dbContext.RemoveRange(entity);
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public Task<List<TEntity>> Query()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> QueryById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
