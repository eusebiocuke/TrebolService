using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TrebolService.Data;
using TrebolService.Models;

namespace TrebolService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoesController : ControllerBase
    {
        private readonly TrebolAcademicDbContext _context;

        public IngresoesController(TrebolAcademicDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingresoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingreso>>> GetIngresos()
        {
            return await _context.Ingresos.ToListAsync();
        }

        // GET: api/Ingresoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingreso>> GetIngreso(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);

            if (ingreso == null)
            {
                return NotFound();
            }

            return ingreso;
        }

        // PUT: api/Ingresoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngreso(int id, Ingreso ingreso)
        {
            if (id != ingreso.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingreso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngresoExists(id))
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

        // POST: api/Ingresoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingreso>> PostIngreso(Ingreso ingreso)
        {
            ingreso.Status = "PENDIENTE";
            ingreso.Motivo = string.Empty;

            _context.Ingresos.Add(ingreso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngresoExists(ingreso.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIngreso", new { id = ingreso.Id }, ingreso);
        }

        // DELETE: api/Ingresoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }

            _context.Ingresos.Remove(ingreso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/{}procesar

        [HttpPost]
        [Route("procesar")]
        public async Task<ActionResult<Ingreso>> PostProcesarAsignacion()
        {
            var ingreso = _context.Ingresos.Where(a => a.Status == "PENDIENTE").ToList();

            List<bool> results = new List<bool>();

            ingreso.ForEach(a =>
            {
                var bresult = ProcesarIngreso(a);
                results.Add(bresult);
            });
            if (results.Any(a => a == false))
            {

                return Ok("Listado Procesado con errores");
            }
            else
            {
                return Ok("Listado Procesado correctamente");
            }
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.Id == id);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool ProcesarIngreso(Ingreso ingreso)
        {



            if (ingreso != null) //Exist
            {
                //IngresoDto ingresodto = new IngresoDto();
                //Helper.Map<Ingreso>(ingreso, ingresodto);
                //Check Value
                if (ModelState.IsValid)
                {
                    //check afinidad
                    if (!_context.Afinidades.Any(a => a.Id == ingreso.IdAfinidad))
                    {  //Rechazar
                        RechazarIngreso(ingreso, "La afinidad ingresada no existe");
                        return false;
                    }


                    //Check student exist
                    var studentExist = _context.Estudiantes.Any(a => a.Identificacion == ingreso.Identificacion
                    && a.Nombre == ingreso.Nombre
                    && a.Apellido == ingreso.Apellido
                    && a.Edad == ingreso.Edad
                    );
                    //Asignar Gremorio
                    if (!studentExist)
                    {
                        //No existe continuar el registro del estudiante
                        var newstudent = new Estudiante()
                        {
                            Nombre = ingreso.Nombre,
                            Apellido = ingreso.Apellido,
                            Edad = Convert.ToInt32(ingreso.Edad),
                            FechaOp = DateTime.Now,
                            Identificacion = ingreso.Identificacion,
                            IdAfinidad = Convert.ToInt32(ingreso.IdAfinidad)
                        };
                        try
                        {
                            _context.Database.BeginTransaction();

                            var grimorios = _context.Grimorios.ToList();
                            var indexMax = grimorios.Count - 1;
                            if (indexMax < 0)
                            {
                                _context.Database.RollbackTransaction();
                                //Rechazar
                                RechazarIngreso(ingreso, "No se encontraron grimorios actuales");
                                return false;
                            }
                            var grimorio = grimorios[RandomNumberGenerator.GetInt32(0, indexMax)].Id;


                            _context.Estudiantes.Add(newstudent);

                            ingreso.Status = "APROBADA";
                            ingreso.Motivo = string.Empty;
                            _context.Entry(ingreso).State = EntityState.Modified;

                            _context.SaveChanges();
                            //Asign Grimorio

                            GrimorioAsignado a = new GrimorioAsignado()
                            {
                                IdEstudiante = newstudent.Id,
                                IdGrimorio = grimorio,
                                FechaUpdate = DateTime.Now,
                            };
                            _context.GrimorioAsignados.Add(a);
                            _context.SaveChanges();

                            _context.Database.CommitTransaction();
                            //await _context.SaveChangesAsync();
                            return true;
                        }
                        catch (DbUpdateException)
                        {
                            _context.Database.RollbackTransaction();
                            if (IngresoExists(ingreso.Id))
                            {
                                return false;
                            }
                            else
                            {
                                throw;
                            }

                        }
                    }
                    else
                    {
                        //Rechazar
                        RechazarIngreso(ingreso, "El estudiante ya se encuentra registrado");
                        return false;
                    }
                }
                else
                {
                    //Rechazar
                    RechazarIngreso(ingreso, "Los parametros del estudiante son incompletos");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool RechazarIngreso(Ingreso ingreso, string motivo)
        {
            ingreso.Status = "RECHAZADO";
            ingreso.Motivo = motivo;
            ingreso.Fechaop = DateTime.Now;
            _context.Entry(ingreso).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                if (IngresoExists(ingreso.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

    }
}

