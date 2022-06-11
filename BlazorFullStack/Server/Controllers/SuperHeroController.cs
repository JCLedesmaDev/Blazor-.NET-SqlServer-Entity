using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        ///  DATOS MOCKEADOS
        public static List<Comic> Comics = new List<Comic> { 
        
            new Comic { Id = 1, Name = "Marvel"},
            new Comic { Id = 2, Name ="DC"}
        };

        public static List<SuperHero> Heroes = new List<SuperHero>
        {
            new SuperHero { 
                Id = 1, 
                FirstName="Peter", 
                LastName="Parker",
                HeroName="Spider-Man",
                Comic= Comics[0],
                ComicId=Comics[0].Id
            },
            new SuperHero {
                Id = 2,
                FirstName="Bruce",
                LastName="Wayne",
                HeroName="Batman",
                Comic= Comics[1],
                ComicId=Comics[1].Id
            },
        };
        ///  DATOS MOCKEADOS


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {

            // Retorna un status 200 con Herores
            return Ok(Heroes);
       
            // Mientras que apra devovler un status 400, seria BadRequest()
            // Mientras que apra devovler un status 404, seria NotFound()
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            return Ok(Comics);
        }



        [HttpGet("{Id}")] //Es el attr que le vamos a pasar a la ruta GET
        public async Task<ActionResult<SuperHero>> GetOneHero( int Id)
        {

            SuperHero Heroe = Heroes.FirstOrDefault(heroe => heroe.Id == Id);

            if (Heroe == null)
            {
                return NotFound("No se ha encontrado ningun Heroe");
            } 

            // Retorna un status 200 con Herores
            return Ok(Heroe);
       
            // Retorna un status 200 con Herores
            // Mientras que apra devovler un status 400, seria BadRequest()
            // Mientras que apra devovler un status 404, seria NotFound()
        }

    }
}
