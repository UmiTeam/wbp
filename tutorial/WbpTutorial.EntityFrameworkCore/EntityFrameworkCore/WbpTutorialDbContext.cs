using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using WbpTutorial.Domain.Books;

namespace WbpTutorial.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class WbpTutorialDbContext :
    AbpDbContext<WbpTutorialDbContext>
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    #endregion

    public DbSet<Book> Books { get; set; }

    public WbpTutorialDbContext(DbContextOptions<WbpTutorialDbContext> options)
        : base(options){
    }

    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(WbpTutorialConsts.DbTablePrefix + "YourEntities", WbpTutorialConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Book>(b => { b.ConfigureByConvention(); });
    }
}