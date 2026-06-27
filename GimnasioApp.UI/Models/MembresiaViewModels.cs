using System.ComponentModel.DataAnnotations;

namespace GimnasioApp.UI.Models
{
    public class MembresiaListadoViewModel
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

    public class MembresiaDetalleViewModel
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

    public class AgregarMembresiaViewModel
    {
        [Required(ErrorMessage = "La identificación es requerida.")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son requeridos.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio base mensual es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio base mensual debe ser mayor a cero.")]
        [Display(Name = "Precio Base Mensual")]
        public decimal PrecioBaseMensual { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaDeInicio { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El tipo de plan es requerido.")]
        [Range(1, 3, ErrorMessage = "El tipo de plan debe ser 1, 2 o 3.")]
        [Display(Name = "Tipo de Plan")]
        public int? TipoDePlan { get; set; }
    }

    public class EditarMembresiaViewModel
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son requeridos.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio base mensual es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio base mensual debe ser mayor a cero.")]
        [Display(Name = "Precio Base Mensual")]
        public decimal PrecioBaseMensual { get; set; }

        [Required(ErrorMessage = "El tipo de plan es requerido.")]
        [Range(1, 3, ErrorMessage = "El tipo de plan debe ser 1, 2 o 3.")]
        [Display(Name = "Tipo de Plan")]
        public int? TipoDePlan { get; set; }

        public DateTime FechaDeInicio { get; set; }
    }

    public class MembresiaActivaViewModel
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDePlan { get; set; } = string.Empty;
        public string FechaDeVencimiento { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
    }

    public class MembresiaVencidaViewModel
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDePlan { get; set; } = string.Empty;
        public string FechaDeInicio { get; set; } = string.Empty;
        public string FechaDeVencimiento { get; set; } = string.Empty;
    }
}
