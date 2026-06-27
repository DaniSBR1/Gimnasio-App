using GimnasioApp.Model;

namespace GimnasioApp.BL.Interfaces
{
    public interface IMembresiaService
    {
        Task<IEnumerable<Membresia>> ObtenerTodasAsync(string? identificacion = null);
        Task<IEnumerable<Membresia>> ObtenerActivasAsync(string? identificacion = null);
        Task<IEnumerable<Membresia>> ObtenerVencidasAsync(string? identificacion = null);
        Task<Membresia?> ObtenerPorIdAsync(int id);
        Task<Membresia> AgregarAsync(Membresia membresia);
        Task<Membresia> EditarAsync(Membresia membresia);
        decimal CalcularMontoTotal(decimal precioBaseMensual, int tipoDePlan);
        DateTime CalcularFechaDeVencimiento(DateTime fechaDeInicio, int tipoDePlan);
        int CalcularEstado(DateTime fechaDeVencimiento);
    }
}
