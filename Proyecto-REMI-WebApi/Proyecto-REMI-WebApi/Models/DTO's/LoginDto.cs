using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El documento es requerido")]
        [StringLength(20, ErrorMessage = "El documento no puede exceder los 20 caracteres")]
        public string Documento { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(45, ErrorMessage = "La contraseña no puede exceder los 45 caracteres")]
        public string Password { get; set; } = string.Empty;

        // Opcional: recordar sesión
        public bool? RememberMe { get; set; } = false;
    }
}
