using System;
using PropertyChanged;
using Volo.Abp.Domain.Entities.Auditing;

namespace WbpTutorial.Domain.Books;

[AddINotifyPropertyChangedInterface]
public class Book : FullAuditedEntity<Guid>
{
    public string Name { get; set; }
    public DateTime PublishDate { get; set; }
    public decimal Price { get; set; }
}