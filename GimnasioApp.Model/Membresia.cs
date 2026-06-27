using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GimnasioApp.Model
{
    [Table("Membresias")]
    public class Membresia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Identificacion { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioBaseMensual { get; set; }

        [Required]
        public int TipoDePlan { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime FechaDeInicio { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime FechaDeVencimiento { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }

        [Required]
        public int Estado { get; set; }
    }
}
