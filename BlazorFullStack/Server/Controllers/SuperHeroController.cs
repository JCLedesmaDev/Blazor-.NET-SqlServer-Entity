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


            List<SuperHero> heroes = await this._context.SuperHeroesEntity
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


            List<Comic> comics = await this._context.ComicsEntity.ToListAsync();

            return Ok(comics);
        }



        [HttpGet("{Id}")] //Es el attr que le vamos a pasar a la ruta GET
        public async Task<ActionResult<SuperHero>> GetOneHero(int Id)
        {

            // Nota no entiendo los metodos q brinda Entity.
            SuperHero Heroe = await this._context.SuperHeroesEntity
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
            //heroe.Comic = null
            this._context.SuperHeroesEntity.Add(heroe);
            await this._context.SaveChangesAsync();


            /// Verificamos que se haya agregado el heroe en la BD
            return Ok(await GetDbHeores());
        }
        private async Task<ActionResult<List<SuperHero>>> GetDbHeores() {


            return await this._context.SuperHeroesEntity
                .Include(superheroe => superheroe.Comic)
                .ToListAsync();

        }


        [HttpPut("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int IdParam)
        {
            //heroe.Comic = null
            SuperHero DbHeroe = await this._context.SuperHeroesEntity
              .Include(heroe => heroe.Comic) // Incluye el comic al que pertenece pero no comprendo la relacion.
              .FirstOrDefaultAsync(heroe => heroe.Id == IdParam);


            if (DbHeroe == null) {
                return NotFound("No se pudo encontrar a este Heroe");
            }

            DbHeroe.FirstName = hero.FirstName;
            DbHeroe.LastName = hero.LastName;
            DbHeroe.HeroName = hero.HeroName;
            DbHeroe.ComicId = hero.ComicId;

            await this._context.SaveChangesAsync();

            return Ok(await GetDbHeores());
        }




        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero( int IdParam)
        {
            //heroe.Comic = null
            SuperHero DbHeroe = await this._context.SuperHeroesEntity
              .Include(heroe => heroe.Comic) // Incluye el comic al que pertenece pero no comprendo la relacion.
              .FirstOrDefaultAsync(heroe => heroe.Id == IdParam);


            if (DbHeroe == null)
            {
                return NotFound("No se pudo encontrar a este Heroe");
            }

           
            this._context.SuperHeroesEntity.Remove(DbHeroe);

            await this._context.SaveChangesAsync();

            return Ok(await GetDbHeores());
        }
    }


}