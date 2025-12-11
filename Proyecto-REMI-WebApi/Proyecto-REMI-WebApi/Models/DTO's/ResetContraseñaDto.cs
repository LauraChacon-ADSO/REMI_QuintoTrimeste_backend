namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class ResetContraseñaDto
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
