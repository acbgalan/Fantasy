using Fantasy.Backend.Data;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAsync()
        {
            var countries = await _context.Countries.ToListAsync();

            return Ok(countries);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Country>> GetASync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            _context.Add(country);
            await _context.SaveChangesAsync();

            return Ok(country);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            var currentCountry = await _context.Countries.FindAsync(id);

            if (currentCountry == null)
            {
                return NotFound();
            }

            currentCountry.Name = country.Name;
            _context.Update(currentCountry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}