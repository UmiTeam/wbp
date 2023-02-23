using System;
using System.Linq.Dynamic.Core;
using Umi.Wbp.Application;
using Volo.Abp.Domain.Repositories;
using WbpTutorial.Domain.Books;

namespace WbpTutorial.Books;

public class BookAppService : WbpApplicationService<Book, Guid, PagedResult>, IBookAppService
{
    public BookAppService(IRepository<Book, Guid> repository) : base(repository){
    }
}