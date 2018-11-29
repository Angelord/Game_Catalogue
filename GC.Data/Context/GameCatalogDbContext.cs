using GameC.Data.Entities;
using System.Data.Entity;

namespace GameC.Data.Context
{
    public class GameCatalogDbContext : DbContext
    {
        public System.Data.Entity.DbSet<GameC.Data.Entities.Game> Games { get; set; }

        public System.Data.Entity.DbSet<GameC.Data.Entities.Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<GameC.Data.Entities.Rating> Ratings { get; set; }
    }
}
