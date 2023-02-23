using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Umi.Wbp.Application;

public interface IWbpApplicationService<TEntityDto, in TKey, in TGetListInput> : IApplicationService
{
    Task<TEntityDto> GetAsync(TKey id);
    Task<PagedResultDto<TEntityDto>> GetListAsync(TGetListInput input);
    Task<TEntityDto> CreateAsync(TEntityDto input);
    Task<TEntityDto> UpdateAsync(TKey id, TEntityDto input);
    Task DeleteAsync(TKey id);
}