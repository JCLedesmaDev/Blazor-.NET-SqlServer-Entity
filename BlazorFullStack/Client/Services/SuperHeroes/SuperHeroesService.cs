using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStack.Client.Services.SuperHeroes
{
    public class SuperHeroesService : ISuperHeroesService
    {

        ///Aqui dentro, ejecutaremos las peticiones HTTP que tiene el BACK


        /// Inyectamos e inicializamos en nuestra clase, el servicio http
        private readonly HttpClient http;
        private readonly NavigationManager _navigationManager;

        public SuperHeroesService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            _navigationManager = navigationManager;
        }



        // Clase implementacion
        public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
        public List<Comic> Comics { get; set; } = new List<Comic>();

        public async Task GetComics()
        {
            List<Comic>? result = await this.http.GetFromJsonAsync
               <List<Comic>>($"/api/superhero/comics");

            if (result != null)
            {
                Comics = result;
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

        public async Task GetSuperHeroes()
        {
            /// NOTA: Aputa al nombre del controller del back, no importa mayusculas o minusculas.
            /// NOTA 2: Tuvimos que importar using System.Net.Http.Json;
            List<SuperHero>? result = await this.http.GetFromJsonAsync
                <List<SuperHero>>("/api/superhero");


            if (result != null)
            {
                Heroes = result;
            }
        }

        public async Task CreateSuperHero(SuperHero hero)
        {
            // Obtenemos una respuesta Http.
            //HttpResponseMessage result = await this.http.PostAsJsonAsync("/api/superhero", hero);
            //await SetListHeroes(result);
            
            await this.http.PostAsJsonAsync("/api/superhero", hero);
            RedirectTo();
        }

        public async Task DeleteSuperHero(int Id)
        {
            // Obtenemos una respuesta Http.
            //HttpResponseMessage result = await this.http.DeleteAsync($"/api/superhero/{Id}");
            //await SetListHeroes(result);
            
            await this.http.DeleteAsync($"/api/superhero/{Id}");
            RedirectTo();
        }

        public async Task UpdateSuperHero(SuperHero hero)
        {
            // Obtenemos una respuesta Http.
            //HttpResponseMessage result = await this.http.PutAsJsonAsync($"/api/superhero/{hero.Id}", hero);
            //await SetListHeroes(result);
            
            await this.http.PutAsJsonAsync($"/api/superhero/{hero.Id}", hero);
            RedirectTo();
        }

        private void RedirectTo()
        {
            _navigationManager.NavigateTo("superHeroes");
        }
        /// Me daba error y no lo supe solucionar.
        //private async Task SetListHeroes(HttpResponseMessage result)
        //{
        //    // Obtenemos los datos json que nos da la respuesta http.
        //    var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
        //    Heroes = response;
        //    _navigationManager.NavigateTo("superHeroes");
        //}
    }
}
