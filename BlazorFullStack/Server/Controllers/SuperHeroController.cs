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
            _context = BaseDatosContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {


            List<SuperHero> heroes = await _context.SuperHeroesEntity
                .Include(sh => sh.Comic)
                .ToListAsync();

            // Retorna un status 200 con Herores
            return Ok(heroes);

            // Mientras que apra devovler un status 400, seria BadRequest()
            // Mientras que apra devovler un status 404, seria NotFound()
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {


            List<Comic> comics = await _context.ComicsEntity.ToListAsync();

            return Ok(comics);
        }



        [HttpGet("{Id}")] //Es el attr que le vamos a pasar a la ruta GET
        public async Task<ActionResult<SuperHero>> GetOneHero(int Id)
        {

            // Nota no entiendo los metodos q brinda Entity.
            SuperHero? Heroe = await _context.SuperHeroesEntity
                .Include(heroe => heroe.Comic) // Incluye el comic al que pertenece pero no comprendo la relacion.
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



        [HttpPost()]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero heroe)
        {
            heroe.Comic = null;
            _context.SuperHeroesEntity.Add(heroe);
            await _context.SaveChangesAsync();


            /// Verificamos que se haya agregado el heroe en la BD
            return Ok(await GetDbHeores());
        }
        private async Task<ActionResult<List<SuperHero>>> GetDbHeores() {


            return await _context.SuperHeroesEntity
                .Include(superheroe => superheroe.Comic)
                .ToListAsync();

        }


            /// Nota: El 1er parametro es el que pasamos comun
            /// Mientras que el 2do parametros es el que le pasamos por la URL
            /// el nombre del parametro debe coincidir con lo Definido en la linea 93
        [HttpPut("{IdParam}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int IdParam)
        {
           

            SuperHero? DbHeroe = await _context.SuperHeroesEntity
              .Include(heroe => heroe.Comic) // Incluye el comic al que pertenece pero no comprendo la relacion.
              .FirstOrDefaultAsync(heroe => heroe.Id == IdParam);


            if (DbHeroe == null) {
                return NotFound("No se pudo encontrar a este Heroe");
            }

            
            DbHeroe.FirstName = hero.FirstName;
            DbHeroe.LastName = hero.LastName;
            DbHeroe.HeroName = hero.HeroName;

            DbHeroe.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbHeores());
        }




        [HttpDelete("{IdParam}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero( int IdParam)
        {
            //heroe.Comic = null
            SuperHero? DbHeroe = await _context.SuperHeroesEntity
              .Include(heroe => heroe.Comic) // Incluye el comic al que pertenece pero no comprendo la relacion.
              .FirstOrDefaultAsync(heroe => heroe.Id == IdParam);


            if (DbHeroe == null)
            {
                return NotFound("No se pudo encontrar a este Heroe");
            }

           
            _context.SuperHeroesEntity.Remove(DbHeroe);

            await _context.SaveChangesAsync();

            return Ok(await GetDbHeores());
        }
    }


}