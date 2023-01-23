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
    public class GrimorioAsignadoesController : ControllerBase
    {
        private readonly TrebolAcademicDbContext _context;

        public GrimorioAsignadoesController(TrebolAcademicDbContext context)
        {
            _context = context;
        }

        // GET: api/GrimorioAsignadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrimorioAsignado>>> GetGrimorioAsignados()
        {
            return await _context.GrimorioAsignados.ToListAsync();
        }

        //// GET: api/GrimorioAsignadoes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GrimorioAsignado>> GetGrimorioAsignado(int id)
        //{
        //    var grimorioAsignado = await _context.GrimorioAsignados.FindAsync(id);

        //    if (grimorioAsignado == null)
        //    {
        //        return NotFound();
        //    }

        //    return grimorioAsignado;
        //}

        //// PUT: api/GrimorioAsignadoes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGrimorioAsignado(int id, GrimorioAsignado grimorioAsignado)
        //{
        //    if (id != grimorioAsignado.IdEstudiante)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(grimorioAsignado).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GrimorioAsignadoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/GrimorioAsignadoes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<GrimorioAsignado>> PostGrimorioAsignado(GrimorioAsignado grimorioAsignado)
        //{
        //    _context.GrimorioAsignados.Add(grimorioAsignado);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (GrimorioAsignadoExists(grimorioAsignado.IdEstudiante))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetGrimorioAsignado", new { id = grimorioAsignado.IdEstudiante }, grimorioAsignado);
        //}

        //// DELETE: api/GrimorioAsignadoes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGrimorioAsignado(int id)
        //{
        //    var grimorioAsignado = await _context.GrimorioAsignados.FindAsync(id);
        //    if (grimorioAsignado == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.GrimorioAsignados.Remove(grimorioAsignado);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool GrimorioAsignadoExists(int id)
        //{
        //    return _context.GrimorioAsignados.Any(e => e.IdEstudiante == id);
        //}
    }
}
