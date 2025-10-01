using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class RegisterDto
    {
        [Required]
        [StringLength(20)]
        public string Documento { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string TipoDocumento { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string Password { get; set; } = string.Empty;

        // 1 = Admin, 2 = Vendedor
        public int CodigoNivel { get; set; }
    }
}
