using GimnasioApp.DA.Context;
using GimnasioApp.DA.Interfaces;
using GimnasioApp.Model;
using Microsoft.EntityFrameworkCore;

namespace GimnasioApp.DA.Repositories
{
    public class MembresiaRepository : IMembresiaRepository
    {
        private readonly GimnasioDbContext _context;

        public MembresiaRepository(GimnasioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Membresia>> ObtenerTodasAsync(string? identificacion = null)
        {
            var query = _context.Membresias.AsQueryable();

            if (!string.IsNullOrWhiteSpace(identificacion))
                query = query.Where(m => m.Identificacion.Contains(identificacion));

            return await query.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<IEnumerable<Membresia>> ObtenerActivasAsync(string? identificacion = null)
        {
            var query = _context.Membresias.Where(m => m.Estado == 1);

            if (!string.IsNullOrWhiteSpace(identificacion))
                query = query.Where(m => m.Identificacion.Contains(identificacion));

            return await query.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<IEnumerable<Membresia>> ObtenerVencidasAsync(string? identificacion = null)
        {
            var query = _context.Membresias.Where(m => m.Estado == 2);

            if (!string.IsNullOrWhiteSpace(identificacion))
                query = query.Where(m => m.Identificacion.Contains(identificacion));

            return await query.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Membresia?> ObtenerPorIdAsync(int id)
        {
            return await _context.Membresias.FindAsync(id);
        }

        public async Task<Membresia> AgregarAsync(Membresia membresia)
        {
            _context.Membresias.Add(membresia);
            await _context.SaveChangesAsync();
            return membresia;
        }

        public async Task<Membresia> EditarAsync(Membresia membresia)
        {
            _context.Membresias.Update(membresia);
            await _context.SaveChangesAsync();
            return membresia;
        }
    }
}
