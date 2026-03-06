namespace Minigram.Core.Context
{
    using Microsoft.EntityFrameworkCore;

    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options)
            : base(options) {}
    }
}