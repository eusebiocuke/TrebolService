using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrebolService.Data;
using TrebolService.Models;

namespace TrebolService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrimoriosController : ControllerBase
    {
        private readonly TrebolAcademicDbContext _context;

        public GrimoriosController(TrebolAcademicDbContext context)
        {
            _context = context;
        }

        // GET: api/Grimorios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grimorio>>> GetGrimorios()
        {
            return await _context.Grimorios.ToListAsync();
        }

        // GET: api/Grimorios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grimorio>> GetGrimorio(int id)
        {
            var grimorio = await _context.Grimorios.FindAsync(id);

            if (grimorio == null)
            {
                return NotFound();
            }

            return grimorio;
        }

        // PUT: api/Grimorios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrimorio(int id, Grimorio grimorio)
        {
            if (id != grimorio.Id)
            {
                return BadRequest();
            }

            _context.Entry(grimorio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrimorioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Grimorios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grimorio>> PostGrimorio(Grimorio grimorio)
        {
            _context.Grimorios.Add(grimorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrimorio", new { id = grimorio.Id }, grimorio);
        }

        // DELETE: api/Grimorios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrimorio(int id)
        {
            var grimorio = await _context.Grimorios.FindAsync(id);
            if (grimorio == null)
            {
                return NotFound();
            }

            _context.Grimorios.Remove(grimorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrimorioExists(int id)
        {
            return _context.Grimorios.Any(e => e.Id == id);
        }
    }
}
