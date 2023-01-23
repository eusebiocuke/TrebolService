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
    public class AfinidadesController : ControllerBase
    {
        private readonly TrebolAcademicDbContext _context;

        public AfinidadesController(TrebolAcademicDbContext context)
        {
            _context = context;
        }

        // GET: api/Afinidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Afinidade>>> GetAfinidades()
        {
            return await _context.Afinidades.ToListAsync();
        }

        // GET: api/Afinidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Afinidade>> GetAfinidade(int id)
        {
            var afinidade = await _context.Afinidades.FindAsync(id);

            if (afinidade == null)
            {
                return NotFound();
            }

            return afinidade;
        }

        // PUT: api/Afinidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAfinidade(int id, Afinidade afinidade)
        {
            if (id != afinidade.Id)
            {
                return BadRequest();
            }

            _context.Entry(afinidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AfinidadeExists(id))
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

        // POST: api/Afinidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Afinidade>> PostAfinidade(Afinidade afinidade)
        {
            _context.Afinidades.Add(afinidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAfinidade", new { id = afinidade.Id }, afinidade);
        }

        // DELETE: api/Afinidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAfinidade(int id)
        {
            var afinidade = await _context.Afinidades.FindAsync(id);
            if (afinidade == null)
            {
                return NotFound();
            }

            _context.Afinidades.Remove(afinidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AfinidadeExists(int id)
        {
            return _context.Afinidades.Any(e => e.Id == id);
        }
    }
}
