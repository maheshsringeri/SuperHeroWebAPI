using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //private static List<SuperHero> heroes = new List<SuperHero> {
        //        new SuperHero{
        //            Id=1,
        //            Name="Raj Kumar",
        //            FIrstName="Raj F",
        //            LastName="Kumar L",
        //            Place="Bangalore"
        //        },
        //        new SuperHero{
        //            Id=2,
        //            Name="Shiva RajKumar",
        //            FIrstName="Shiva F",
        //            LastName="Shiva L",
        //            Place="Bangalore"
        //        },
        //        new SuperHero{
        //            Id=3,
        //            Name="Prem",
        //            FIrstName="Prem F",
        //            LastName="Prem L",
        //            Place="Mysore"
        //        },
        //        new SuperHero{
        //            Id=4,
        //            Name="Ravi RajKumar",
        //            FIrstName="Ravi F",
        //            LastName="Ravi L",
        //            Place="Bangalore"
        //        },
        //    };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound("Hero not found");
            }

            return Ok(hero);
        }



        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {

            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();


            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero superHero)
        {

            var hero = await _context.SuperHeroes.FindAsync(superHero.Id);
            if (hero == null)
            {
                return BadRequest("Hero is not found");
            }

            hero.Name = superHero.Name;
            hero.FIrstName = superHero.FIrstName;
            hero.LastName = superHero.LastName;
            hero.Place = superHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return NotFound("Hero is not found");

            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
