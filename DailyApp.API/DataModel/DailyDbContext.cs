using Microsoft.EntityFrameworkCore;

namespace DailyApp.API.DataModel;

public class DailyDbContext : DbContext
{
    public DailyDbContext(DbContextOptions<DailyDbContext> options) : base(options)
    {
    }

    public virtual DbSet<AccountInfo> AccountInfo { get; set; }
}