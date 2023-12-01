using AnimeTitlesApp.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeTitlesApp.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<AnimeType> AnimeTypes { get; set; }
    }
}
