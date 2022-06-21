namespace BlazorFullStack.Client.Services.SuperHeroes
{
    public interface ISuperHeroesService
    {
        /* Aqui determinamos, como se llamaran los servicios  
        que utilizaremos en el front para llamar a los Controllers del back
        NOTA: Los Services, se llamaran igual que los controllers del back
            

        */
        List<SuperHero> Heroes { get; set; }

        List<Comic> Comics { get; set; }
        Task GetComics();

        Task GetSuperHeroes();
        
        Task<SuperHero> GetOneHero(int Id);

        Task CreateSuperHero(SuperHero hero);

        Task DeleteSuperHero(int Id);

        Task UpdateSuperHero(SuperHero hero);
    }
}
