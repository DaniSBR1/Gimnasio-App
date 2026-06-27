using GimnasioApp.API.DTOs;
using GimnasioApp.BL.Interfaces;
using GimnasioApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembresiasController : ControllerBase
    {
        private readonly IMembresiaService _membresiaService;

        public MembresiasController(IMembresiaService membresiaService)
        {
            _membresiaService = membresiaService;
        }

        // GET api/membresias?identificacion=xxx
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas([FromQuery] string? identificacion = null)
        {
            var membresias = await _membresiaService.ObtenerTodasAsync(identificacion);
            var resultado = membresias.Select(m => new MembresiaListadoDto
            {
                Id = m.Id,
                Identificacion = m.Identificacion,
                NombreCompleto = $"{m.Nombre} {m.Apellidos}",
                TipoDePlan = ObtenerNombrePlan(m.TipoDePlan),
                FechaDeInicio = m.FechaDeInicio.ToString("dd/MM/yyyy"),
                FechaDeVencimiento = m.FechaDeVencimiento.ToString("dd/MM/yyyy"),
                Estado = ObtenerNombreEstado(m.Estado),
                MontoTotal = m.MontoTotal
            });
            return Ok(resultado);
        }

        // GET api/membresias/activas?identificacion=xxx
        [HttpGet("activas")]
        public async Task<IActionResult> ObtenerActivas([FromQuery] string? identificacion = null)
        {
            var membresias = await _membresiaService.ObtenerActivasAsync(identificacion);
            var resultado = membresias.Select(m => new MembresiaActivaDto
            {
                Id = m.Id,
                Identificacion = m.Identificacion,
                Nombre = m.Nombre,
                TipoDePlan = ObtenerNombrePlan(m.TipoDePlan),
                FechaDeVencimiento = m.FechaDeVencimiento.ToString("dd/MM/yyyy"),
                MontoTotal = m.MontoTotal
            });
            return Ok(resultado);
        }

        // GET api/membresias/vencidas?identificacion=xxx
        [HttpGet("vencidas")]
        public async Task<IActionResult> ObtenerVencidas([FromQuery] string? identificacion = null)
        {
            var membresias = await _membresiaService.ObtenerVencidasAsync(identificacion);
            var resultado = membresias.Select(m => new MembresiaVencidaDto
            {
                Id = m.Id,
                Identificacion = m.Identificacion,
                Nombre = m.Nombre,
                TipoDePlan = ObtenerNombrePlan(m.TipoDePlan),
                FechaDeInicio = m.FechaDeInicio.ToString("dd/MM/yyyy"),
                FechaDeVencimiento = m.FechaDeVencimiento.ToString("dd/MM/yyyy")
            });
            return Ok(resultado);
        }

        // GET api/membresias/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var membresia = await _membresiaService.ObtenerPorIdAsync(id);
            if (membresia == null)
                return NotFound(new { mensaje = "Membresía no encontrada." });

            var resultado = new MembresiaDetalleDto
            {
                Id = membresia.Id,
                Identificacion = membresia.Identificacion,
                Nombre = membresia.Nombre,
                Apellidos = membresia.Apellidos,
                Telefono = membresia.Telefono,
                TipoDePlan = ObtenerNombrePlan(membresia.TipoDePlan),
                TipoDePlanId = membresia.TipoDePlan,
                PrecioBaseMensual = membresia.PrecioBaseMensual,
                FechaDeInicio = membresia.FechaDeInicio.ToString("dd/MM/yyyy"),
                FechaDeVencimiento = membresia.FechaDeVencimiento.ToString("dd/MM/yyyy"),
                Estado = ObtenerNombreEstado(membresia.Estado),
                EstadoId = membresia.Estado,
                MontoTotal = membresia.MontoTotal
            };
            return Ok(resultado);
        }

        // POST api/membresias
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] AgregarMembresiaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var membresia = new Membresia
            {
                Identificacion = dto.Identificacion,
                Nombre = dto.Nombre,
                Apellidos = dto.Apellidos,
                Telefono = dto.Telefono,
                PrecioBaseMensual = dto.PrecioBaseMensual,
                FechaDeInicio = dto.FechaDeInicio,
                TipoDePlan = dto.TipoDePlan!.Value
            };

            var resultado = await _membresiaService.AgregarAsync(membresia);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.Id }, resultado);
        }

        // PUT api/membresias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EditarMembresiaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var membresiaExistente = await _membresiaService.ObtenerPorIdAsync(id);
            if (membresiaExistente == null)
                return NotFound(new { mensaje = "Membresía no encontrada." });

            membresiaExistente.Nombre = dto.Nombre;
            membresiaExistente.Apellidos = dto.Apellidos;
            membresiaExistente.Telefono = dto.Telefono;
            membresiaExistente.PrecioBaseMensual = dto.PrecioBaseMensual;
            membresiaExistente.TipoDePlan = dto.TipoDePlan!.Value;

            var resultado = await _membresiaService.EditarAsync(membresiaExistente);
            return Ok(resultado);
        }

        private static string ObtenerNombrePlan(int tipoDePlan) => tipoDePlan switch
        {
            1 => "Mensual",
            2 => "Trimestral",
            3 => "Anual",
            _ => "Desconocido"
        };

        private static string ObtenerNombreEstado(int estado) => estado switch
        {
            1 => "Activa",
            2 => "Vencida",
            _ => "Desconocido"
        };
    }
}
