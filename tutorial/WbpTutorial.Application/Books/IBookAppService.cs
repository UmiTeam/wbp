using System;
using System.Linq.Dynamic.Core;
using Umi.Wbp.Application;
using WbpTutorial.Domain.Books;

namespace WbpTutorial.Books;

public interface IBookAppService : IWbpApplicationService<Book, Guid, PagedResult>
{
}