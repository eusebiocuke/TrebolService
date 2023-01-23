using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrebolService.Models;

namespace TrebolService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicController : ControllerBase
    {
        // POST: api/Afinidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Ingresos>> PostAfinidade(Afinidade afinidade)
        //{
        //    _context.Afinidades.Add(afinidade);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAfinidade", new { id = afinidade.Id }, afinidade);
        //}
    }
}
