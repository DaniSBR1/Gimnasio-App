using GimnasioApp.BL.Interfaces;
using GimnasioApp.DA.Interfaces;
using GimnasioApp.Model;

namespace GimnasioApp.BL.Services
{
    public class MembresiaService : IMembresiaService
    {
        private const int EstadoActiva = 1;
        private const int EstadoVencida = 2;

        private readonly IMembresiaRepository _membresiaRepository;

        public MembresiaService(IMembresiaRepository membresiaRepository)
        {
            _membresiaRepository = membresiaRepository;
        }

        public async Task<IEnumerable<Membresia>> ObtenerTodasAsync(string? identificacion = null)
        {
            var membresias = await _membresiaRepository.ObtenerTodasAsync(identificacion);
            return await ActualizarEstadosAsync(membresias);
        }

        public async Task<IEnumerable<Membresia>> ObtenerActivasAsync(string? identificacion = null)
        {
            // Se sincronizan primero los estados de todas las membresías del cliente
            // para que ninguna que ya venció siga apareciendo como Activa.
            await ActualizarEstadosAsync(await _membresiaRepository.ObtenerTodasAsync(identificacion));
            return await _membresiaRepository.ObtenerActivasAsync(identificacion);
        }

        public async Task<IEnumerable<Membresia>> ObtenerVencidasAsync(string? identificacion = null)
        {
            await ActualizarEstadosAsync(await _membresiaRepository.ObtenerTodasAsync(identificacion));
            return await _membresiaRepository.ObtenerVencidasAsync(identificacion);
        }

        public async Task<Membresia?> ObtenerPorIdAsync(int id)
        {
            var membresia = await _membresiaRepository.ObtenerPorIdAsync(id);
            if (membresia == null)
                return null;

            return (await ActualizarEstadosAsync(new[] { membresia })).Single();
        }

        public async Task<Membresia> AgregarAsync(Membresia membresia)
        {
            membresia.MontoTotal = CalcularMontoTotal(membresia.PrecioBaseMensual, membresia.TipoDePlan);
            membresia.FechaDeVencimiento = CalcularFechaDeVencimiento(membresia.FechaDeInicio, membresia.TipoDePlan);
            membresia.Estado = CalcularEstado(membresia.FechaDeVencimiento);
            return await _membresiaRepository.AgregarAsync(membresia);
        }

        public async Task<Membresia> EditarAsync(Membresia membresia)
        {
            membresia.MontoTotal = CalcularMontoTotal(membresia.PrecioBaseMensual, membresia.TipoDePlan);
            membresia.FechaDeVencimiento = CalcularFechaDeVencimiento(membresia.FechaDeInicio, membresia.TipoDePlan);
            membresia.Estado = CalcularEstado(membresia.FechaDeVencimiento);
            return await _membresiaRepository.EditarAsync(membresia);
        }

        public decimal CalcularMontoTotal(decimal precioBaseMensual, int tipoDePlan)
        {
            return tipoDePlan switch
            {
                1 => precioBaseMensual * 1,                        // Mensual: sin descuento
                2 => precioBaseMensual * 3 * 0.90m,                // Trimestral: 10% descuento
                3 => precioBaseMensual * 12 * 0.80m,               // Anual: 20% descuento
                _ => throw new ArgumentException("Tipo de plan no válido.")
            };
        }

        public DateTime CalcularFechaDeVencimiento(DateTime fechaDeInicio, int tipoDePlan)
        {
            return tipoDePlan switch
            {
                1 => fechaDeInicio.AddMonths(1),
                2 => fechaDeInicio.AddMonths(3),
                3 => fechaDeInicio.AddMonths(12),
                _ => throw new ArgumentException("Tipo de plan no válido.")
            };
        }

        public int CalcularEstado(DateTime fechaDeVencimiento)
        {
            // Una membresía está vencida cuando su fecha de vencimiento ya pasó
            // respecto al día de hoy (sin importar la hora).
            return fechaDeVencimiento.Date < DateTime.Now.Date ? EstadoVencida : EstadoActiva;
        }

        /// <summary>
        /// Recalcula el estado real de cada membresía según su fecha de vencimiento
        /// y persiste en base de datos únicamente aquellas cuyo estado cambió.
        /// Esto evita que una membresía agregada o editada con una fecha de inicio
        /// pasada (o que simplemente venció con el paso del tiempo) quede marcada
        /// incorrectamente como Activa.
        /// </summary>
        private async Task<IEnumerable<Membresia>> ActualizarEstadosAsync(IEnumerable<Membresia> membresias)
        {
            var lista = membresias.ToList();
            foreach (var membresia in lista)
            {
                var estadoCorrecto = CalcularEstado(membresia.FechaDeVencimiento);
                if (membresia.Estado != estadoCorrecto)
                {
                    membresia.Estado = estadoCorrecto;
                    await _membresiaRepository.EditarAsync(membresia);
                }
            }
            return lista;
        }
    }
}
