namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class CambiarContraseñaDto
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
