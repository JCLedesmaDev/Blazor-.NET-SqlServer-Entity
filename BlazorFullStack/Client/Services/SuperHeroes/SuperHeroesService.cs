using System.Net.Http.Json;

namespace BlazorFullStack.Client.Services.SuperHeroes
{
    public class SuperHeroesService : ISuperHeroesService
    {

        ///Aqui dentro, ejecutaremos las peticiones HTTP que tiene el BACK


        /// Inyectamos e inicializamos en nuestra clase, el servicio http
        private readonly HttpClient http;
        public SuperHeroesService(HttpClient http)
        {
            this.http = http;
        }



        // Clase implementacion
        public List<SuperHero> Heroes { get ; set ; } = new List<SuperHero>();
        public List<Comic> Comics { get; set; } = new List<Comic>();

        public async Task GetComics()
        {
            List<Comic> result = await this.http.GetFromJsonAsync
               <List<Comic>>($"/api/superhero/comics");

            if (result != null)
            {
                Comics =  result;
            }
        }

        public async Task<SuperHero> GetOneHero(int Id)
        {
            SuperHero result = await this.http.GetFromJsonAsync
                <SuperHero>($"/api/superhero/{Id}");

            if (result != null)
            {
                return result;
            }
            throw new Exception("Heroe no encontrado");
        }

        public  async Task GetSuperHeroes()
        {
            /// NOTA: Aputa al nombre del controller del back, no importa mayusculas o minusculas.
            /// NOTA 2: Tuvimos que importar using System.Net.Http.Json;
            List<SuperHero> result = await this.http.GetFromJsonAsync
                <List<SuperHero>>("/api/superhero");


            if (result != null)
            {
                Heroes = result;
            }
        }
    }
}
  