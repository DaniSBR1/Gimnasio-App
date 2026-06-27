using System.ComponentModel.DataAnnotations;

namespace GimnasioApp.API.DTOs
{
    public class MembresiaListadoDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string TipoDePlan { get; set; } = string.Empty;
        public string FechaDeInicio { get; set; } = string.Empty;
        public string FechaDeVencimiento { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
    }

    public class MembresiaDetalleDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string TipoDePlan { get; set; } = string.Empty;
        public int TipoDePlanId { get; set; }
        public decimal PrecioBaseMensual { get; set; }
        public string FechaDeInicio { get; set; } = string.Empty;
        public string FechaDeVencimiento { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int EstadoId { get; set; }
        public decimal MontoTotal { get; set; }
    }

    public class MembresiaActivaDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDePlan { get; set; } = string.Empty;
        public string FechaDeVencimiento { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
    }

    public class MembresiaVencidaDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDePlan { get; set; } = string.Empty;
        public string FechaDeInicio { get; set; } = string.Empty;
        public string FechaDeVencimiento { get; set; } = string.Empty;
    }

    public class AgregarMembresiaDto
    {
        [Required(ErrorMessage = "La identificación es requerida.")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son requeridos.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio base mensual es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio base mensual debe ser mayor a cero.")]
        public decimal PrecioBaseMensual { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        public DateTime FechaDeInicio { get; set; }

        [Required(ErrorMessage = "El tipo de plan es requerido.")]
        [Range(1, 3, ErrorMessage = "El tipo de plan debe ser 1, 2 o 3.")]
        public int? TipoDePlan { get; set; }
    }

    public class EditarMembresiaDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son requeridos.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio base mensual es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio base mensual debe ser mayor a cero.")]
        public decimal PrecioBaseMensual { get; set; }

        [Required(ErrorMessage = "El tipo de plan es requerido.")]
        [Range(1, 3, ErrorMessage = "El tipo de plan debe ser 1, 2 o 3.")]
        public int? TipoDePlan { get; set; }
    }
}
