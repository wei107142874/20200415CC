using Basic.Entites;
using Basic.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext _db;

        public UnitOfWork(EFDbContext db)
        {
            this._db = db;
        }
        IDbContextTransaction tran;
        public async Task BeginTran()
        {
            tran =await _db.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                await tran.CommitAsync();
            }
            catch (Exception)
            {

                await RollBack();
            }
            finally
            {
               await Dispose();
            }
        }

        public async Task RollBack()
        {
            await tran.RollbackAsync();
            await Dispose();
        }

        private async Task Dispose()
        {
            await tran.DisposeAsync();
        }
    }
}
