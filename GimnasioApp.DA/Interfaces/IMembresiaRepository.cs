using GimnasioApp.Model;

namespace GimnasioApp.DA.Interfaces
{
    public interface IMembresiaRepository
    {
        Task<IEnumerable<Membresia>> ObtenerTodasAsync(string? identificacion = null);
        Task<IEnumerable<Membresia>> ObtenerActivasAsync(string? identificacion = null);
        Task<IEnumerable<Membresia>> ObtenerVencidasAsync(string? identificacion = null);
        Task<Membresia?> ObtenerPorIdAsync(int id);
        Task<Membresia> AgregarAsync(Membresia membresia);
        Task<Membresia> EditarAsync(Membresia membresia);
    }
}
