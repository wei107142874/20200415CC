using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basic.IRepository
{
    public interface IUnitOfWork
    {
        Task Commit();

        Task RollBack();

        Task BeginTran();
    }
}
