namespace Proyecto_REMI_WebApi.DTOs
{
    public class LoginRequest
    {
        public string DocumentoUsuario { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
