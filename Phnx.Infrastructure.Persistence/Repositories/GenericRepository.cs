using Microsoft.EntityFrameworkCore;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.DTOs.Users;
using Phnx.Infrastructure.Persistence.Database;
using Phoenix.Mediator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Phnx.Infrastructure.Persistence.Repositories
{

    public class GenericRepository<T>(PhnxDbContext dbcontext) : IGenericRepository<T> where T : BaseEntity
    {
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var dbSet = dbcontext.Set<T>();
            var entity = await dbSet.FindAsync([id], cancellationToken: cancellationToken);
            if (entity != null)
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
            throw new NotFoundException(typeof(T).Name);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().AnyAsync(predicate, cancellationToken);

        public async Task Create(T entity, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().AddAsync(entity, cancellationToken);
        public async Task<TResult?> LastAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken)
        {
            return await dbcontext.Set<T>()
                .Where(predicate)
                .OrderByDescending(x => x.CreatedDate)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<T?> GetById(Guid id, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().FindAsync([id], cancellationToken);

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken) =>
            predicate == null ? await dbcontext.Set<T>().ToListAsync(cancellationToken) : await dbcontext.Set<T>().Where(predicate).ToListAsync(cancellationToken);


        public async Task<T?> GetByQuery(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);

        public async Task<(int totalCount, List<T> result)> GetPaginated(int pageNum, int pageSize, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? sortPredicate, CancellationToken cancellationToken)
        {
            IQueryable<T> query = dbcontext.Set<T>().AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if (sortPredicate != null)
            {
                query = query.OrderByDescending(sortPredicate);
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedDate);
            }
            return (await query.CountAsync(cancellationToken), await query.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToListAsync(cancellationToken));
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().CountAsync(cancellationToken);


        public async Task<List<TResult>> GetAll<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().Where(predicate).AsNoTracking().Select(selector).ToListAsync(cancellationToken);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().CountAsync(predicate, cancellationToken);

        public async Task<List<TResult>> GetAll<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? sortPredicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken) =>
            sortPredicate == null
            ? await dbcontext.Set<T>().Where(predicate).AsNoTracking().Select(selector).ToListAsync(cancellationToken)
            : await dbcontext.Set<T>().Where(predicate).OrderByDescending(sortPredicate).AsNoTracking().Select(selector).ToListAsync(cancellationToken);

        public async Task<(int totalCount, List<T> result)> GetPaginatedWithTrack(int pageNum, int pageSize, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>> sortPredicate, bool isAscending, CancellationToken cancellationToken)
        {
            IQueryable<T> query = dbcontext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            if (isAscending)
            {
                query = query.OrderBy(sortPredicate);
            }
            else
            {
                query = query.OrderByDescending(sortPredicate);
            }
            return (await query.CountAsync(cancellationToken), await query.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToListAsync(cancellationToken));
        }
        public async Task<(int totalCount, List<T> result)> GetPaginatedWithTrack(int pageNum, int pageSize, Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, CancellationToken cancellationToken)
        {
            IQueryable<T> query = dbcontext.Set<T>();

            if (predicate != null)
                query = query.Where(predicate);

            query = orderBy(query);

            int totalCount = await query.CountAsync(cancellationToken);
            List<T> result = await query
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (totalCount, result);
        }

        public async Task<(int totalCount, List<TResult> result)> GetPaginated<TResult>(int pageNum, int pageSize, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? sortPredicate, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken)
        {
            IQueryable<T> query = dbcontext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            if (sortPredicate == null)
            {
                query = query.OrderByDescending(x => x.CreatedDate);
            }
            else
            {
                query = query.OrderByDescending(sortPredicate);
            }
            return (await query.CountAsync(cancellationToken), await query.Skip(pageSize * (pageNum - 1)).Take(pageSize).Select(selector).ToListAsync(cancellationToken));
        }

        public async Task AddRange(List<T> entities, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().AddRangeAsync(entities, cancellationToken);

        public void DeleteRange(List<T> entities)
        {
            dbcontext.Set<T>().AttachRange(entities);
            dbcontext.Set<T>().RemoveRange(entities);
        }

        public void Delete(T entity)
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Set<T>().Remove(entity);
        }

        public async Task<(int totalCount, List<TResult> result)> GetPaginated<TResult>(int pageNum, int pageSize, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>> sortPredicate, Expression<Func<T, TResult>> selector, bool isAscending, CancellationToken cancellationToken)
        {
            IQueryable<T> query = dbcontext.Set<T>().AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if (isAscending)
            {
                query = query.OrderBy(sortPredicate);
            }
            else
            {
                query = query.OrderByDescending(sortPredicate);
            }
            return (await query.CountAsync(cancellationToken), await query.Skip(pageSize * (pageNum - 1)).Take(pageSize).Select(selector).ToListAsync(cancellationToken));
        }


        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> predicate, CancellationToken cancellationToken)
        {
            return await dbcontext.Set<T>().SumAsync(predicate, cancellationToken);
        }

        public async Task<decimal> SumAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, decimal>> summedItem, CancellationToken cancellationToken)
        {
            return predicate == null
                ? await dbcontext.Set<T>().SumAsync(summedItem, cancellationToken)
                : await dbcontext.Set<T>().Where(predicate).AsNoTracking().SumAsync(summedItem, cancellationToken);
        }

        public async Task<(int totalCount, List<T> result)> GetPaginatedWithTrackMultiSort(int pageNum, int pageSize, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>> sortPredicate1, Expression<Func<T, object>> sortPredicate2, bool isAscending1, bool isAscending2, CancellationToken cancellationToken)
        {
            IQueryable<T> query = dbcontext.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            if (isAscending1)
            {
                query = query.OrderBy(sortPredicate1);
            }
            else
            {
                query = query.OrderByDescending(sortPredicate1);
            }
            if (isAscending2)
            {
                query = query.OrderBy(sortPredicate2);
            }
            else
            {
                query = query.OrderByDescending(sortPredicate2);
            }
            return (await query.CountAsync(cancellationToken), await query.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToListAsync(cancellationToken));
        }

        public async Task<List<TResult>> GetAll<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken) =>
            await dbcontext.Set<T>().OrderBy(x => x.CreatedDate).AsNoTracking().Select(selector).ToListAsync(cancellationToken);

        public async Task<(int totalCount, List<TAudit> result, List<AdminMiniResult> auditUsers)> GetPaginatedWithAudit<TAudit>(int pageNum, int pageSize, Expression<Func<TAudit, bool>>? predicate, Expression<Func<TAudit, object>>? sortPredicate, bool isAscending, CancellationToken cancellationToken) where TAudit : BaseAuditableEntity
        {
            IQueryable<TAudit> query = dbcontext.Set<TAudit>().AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            if (sortPredicate != null)
            {
                if (isAscending)
                    query = query.OrderBy(sortPredicate);
                else
                    query = query.OrderByDescending(sortPredicate);
            }
            else
            {
                if (isAscending)
                    query = query.OrderBy(x => x.UpdatedDate);
                else
                    query = query.OrderByDescending(x => x.UpdatedDate);
            }
            List<TAudit> result = await query.Skip(pageSize * (pageNum - 1)).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);
            List<AdminMiniResult> auditUsers = await GetAuditUsers(result, cancellationToken);
            return (await query.CountAsync(cancellationToken), result, auditUsers);
        }
        private async Task<List<AdminMiniResult>> GetAuditUsers<TAudit>(List<TAudit> entities, CancellationToken cancellationToken) where TAudit : BaseAuditableEntity
        {
            List<Guid> userIds = [.. entities
            .SelectMany(x => new Guid?[] { x.CreatedById, x.UpdatedById })
            .Where(id => id.HasValue && id != Guid.Empty)
            .Distinct()
            .Select(id => id!.Value)];

            return await dbcontext.Set<User>()
                .Where(x => userIds.Contains(x.Id) && x.Email != User.AdminEmail)
                .AsNoTracking()
                .Select(x => new AdminMiniResult(x.Id, x.Name))
                .ToListAsync(cancellationToken);
        }



    }
}
