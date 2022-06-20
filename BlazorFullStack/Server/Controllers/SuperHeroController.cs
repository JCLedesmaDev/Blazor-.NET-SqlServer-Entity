using BlazorFullStack.Server.BaseDatos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFullStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        // Constructor
        private readonly BaseDatosContext _context;
        public SuperHeroController(BaseDatosContext BaseDatosContext)
        {
            this._context = BaseDatosContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {


            List<SuperHero> heroes = await this._context.SuperHeroesEntity.ToListAsync();

            // Retorna un status 200 con Herores
            return Ok(heroes);
       
            // Mientras que apra devovler un status 400, seria BadRequest()
            // Mientras que apra devovler un status 404, seria NotFound()
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {


            List<Comic> comics = await this._context.ComicsEntity.ToListAsync();

            return Ok(comics);
        }



        [HttpGet("{Id}")] //Es el attr que le vamos a pasar a la ruta GET
        public async Task<ActionResult<SuperHero>> GetOneHero( int Id)
        {

            // Nota no entiendo los metodos q brinda Entity.
            SuperHero Heroe = await this._context.SuperHeroesEntity
                .Include(heroe => heroe.Comic)
                .FirstOrDefaultAsync(heroe => heroe.Id == Id);


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
