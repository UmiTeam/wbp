using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Umi.Wbp.Application;

public abstract class WbpApplicationService<TEntity, TKey, TGetListInput> : ApplicationService, IWbpApplicationService<TEntity, TKey, TGetListInput> where TEntity : class, IEntity<TKey>
{
    private readonly IRepository<TEntity, TKey> repository;

    protected WbpApplicationService(IRepository<TEntity, TKey> repository){
        this.repository = repository;
    }

    public async Task<TEntity> GetAsync(TKey id){
        return await repository.GetAsync(id);
    }

    public async Task<PagedResultDto<TEntity>> GetListAsync(TGetListInput input){
        var query = await repository.GetQueryableAsync();
        var totalCount = await AsyncExecuter.CountAsync(query);

        var entities = new List<TEntity>();

        if (totalCount > 0){
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            entities = await AsyncExecuter.ToListAsync(query);
        }

        return new PagedResultDto<TEntity>(
            totalCount,
            entities
        );
    }

    public async Task<TEntity> CreateAsync(TEntity entity){
        await repository.InsertAsync(entity, autoSave: true);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TKey id, TEntity entity){
        await repository.UpdateAsync(entity, autoSave: true);
        return entity;
    }

    public async Task DeleteAsync(TKey id){
        await repository.DeleteAsync(id);
    }

    protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListInput input){
        //Try to sort query if available
        if (input is ISortedResultRequest sortInput){
            if (!string.IsNullOrWhiteSpace(sortInput.Sorting)){
                return query.OrderBy(sortInput.Sorting);
            }
        }

        //No sorting
        return query;
    }

    protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input){
        //Try to use paging if available
        if (input is IPagedResultRequest pagedInput){
            return query.PageBy(pagedInput);
        }

        //Try to limit query result if available
        if (input is ILimitedResultRequest limitedInput){
            return query.Take(limitedInput.MaxResultCount);
        }

        //No paging
        return query;
    }
}