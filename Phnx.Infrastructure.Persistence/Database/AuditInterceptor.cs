using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Phnx.Contracts;
using Phnx.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Persistence.Database
{
    public sealed class AuditInterceptor(ICurrentUserService currentUser) : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not DbContext context)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var userId = currentUser.Id;
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.Entity is BaseAuditableEntity auditable)
                {

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditable.AuditCreate(userId);
                            break;

                        case EntityState.Modified:
                            auditable.AuditUpdate(userId);
                            break;
                    }
                }
                if (entry.Entity is BaseDeletableEntity deletable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            deletable.AuditDelete(userId);
                            break;

                        case EntityState.Modified:
                            var originalIsDeleted = (bool)entry.OriginalValues[nameof(BaseDeletableEntity.IsDeleted)]!;
                            var currentIsDeleted = deletable.IsDeleted;
                            if (originalIsDeleted && !currentIsDeleted)
                                deletable.AuditRestore(userId);
                            break;
                    }
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}