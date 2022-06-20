
namespace BlazorFullStack.Server.BaseDatos
{
    public class BaseDatosContext: DbContext
    {

        public BaseDatosContext(
            DbContextOptions<BaseDatosContext> options
        ) : base(options)
        {
        }


        // Cargamos a la BD, datos mockeados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Definimos a la Entidad que le vamos a cargar datos mockeados.
            modelBuilder.Entity<Comic>().HasData(
               new Comic { Id = 1, Name = "Marvel" },
               new Comic { Id = 2, Name = "DC" }
            );

            modelBuilder.Entity<SuperHero>().HasData(
                new SuperHero
                {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Parker",
                    HeroName = "Spider-Man",
                    ComicId = 1
                },
                new SuperHero
                {
                    Id = 2,
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    HeroName = "Batman",
                    ComicId = 2
                }

            );
        }

        public DbSet<Comic> ComicsEntity { get; set; }
        public DbSet<SuperHero> SuperHeroesEntity { get; set; }

    }
}
