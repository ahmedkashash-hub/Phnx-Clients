using Phnx.Domain.Common;
using Phnx.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Contracts
{

    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);


        Task AddRange(List<T> entities, CancellationToken cancellationToken);
        Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task Create(T entity, CancellationToken cancellationToken);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        Task<List<T>> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken);
        Task<List<TResult>> GetAll<TResult>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, object>>? sortPredicate, System.Linq.Expressions.Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);
        Task<List<TResult>> GetAll<TResult>(System.Linq.Expressions.Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);
        Task<List<TResult>> GetAll<TResult>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);
        Task<T?> GetById(Guid id, CancellationToken cancellationToken);
        Task<T?> GetByQuery(System.Linq.Expressions.Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<(int totalCount, List<T> result)> GetPaginated(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate, System.Linq.Expressions.Expression<Func<T, object>>? sortPredicate, CancellationToken cancellationToken);
        Task<(int totalCount, List<TResult> result)> GetPaginated<TResult>(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate, System.Linq.Expressions.Expression<Func<T, object>> sortPredicate, System.Linq.Expressions.Expression<Func<T, TResult>> selector, bool isAscending, CancellationToken cancellationToken);
        Task<(int totalCount, List<TResult> result)> GetPaginated<TResult>(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate, System.Linq.Expressions.Expression<Func<T, object>>? sortPredicate, System.Linq.Expressions.Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);
        Task<(int totalCount, List<T> result)> GetPaginatedWithTrack(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate, System.Linq.Expressions.Expression<Func<T, object>> sortPredicate, bool isAscending, CancellationToken cancellationToken);
        Task<(int totalCount, List<T> result)> GetPaginatedWithTrackMultiSort(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate, System.Linq.Expressions.Expression<Func<T, object>> sortPredicate1, System.Linq.Expressions.Expression<Func<T, object>> sortPredicate2, bool isAscending1, bool isAscending2, CancellationToken cancellationToken);
        Task<decimal> SumAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate, System.Linq.Expressions.Expression<Func<T, decimal>> summedItem, CancellationToken cancellationToken);
        Task<decimal> SumAsync(System.Linq.Expressions.Expression<Func<T, decimal>> predicate, CancellationToken cancellationToken);
        Task<(int totalCount, List<T> result)> GetPaginatedWithTrack(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, CancellationToken cancellationToken);
        Task<(int totalCount, List<TAudit> result, List<AdminMiniResult> auditUsers)> GetPaginatedWithAudit<TAudit>(int pageNum, int pageSize, System.Linq.Expressions.Expression<Func<TAudit, bool>>? predicate, System.Linq.Expressions.Expression<Func<TAudit, object>>? sortPredicate, bool isAscending, CancellationToken cancellationToken) where TAudit : BaseAuditableEntity;
        Task<TResult?> LastAsync<TResult>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);
    }
}