using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using PropertyChanged;
using Umi.Wbp.Routers;
using WbpTutorial.Books;
using WbpTutorial.Domain.Books;

namespace WbpTutorial.Wpf.Views.Books;

[AddINotifyPropertyChangedInterface]
public class ListBookViewModel : INavigatedToAware
{
    private readonly IBookAppService bookAppService;

    public ListBookViewModel(IBookAppService bookAppService){
        this.bookAppService = bookAppService;
    }

    public IEnumerable<Book> Books { get; set; }

    public void OnNavigatedTo(NavigationContext navigationContext){
        // var listBookTask = bookAppService.GetListAsync(new PagedResult());
        // listBookTask.Wait();
        // Books = listBookTask.Result.Items;
    }
}