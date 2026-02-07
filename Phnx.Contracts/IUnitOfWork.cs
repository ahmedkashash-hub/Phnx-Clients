using Phnx.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken);
        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}
