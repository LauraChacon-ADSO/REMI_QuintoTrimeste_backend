namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class ActualizarUsuariosDto

    {
        public string TipoDocumentoUsuario { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string ApellidoUsuario { get; set; } = null!;
        public string? CorreoUsuario { get; set; }
        public string? Password { get; set; } // opcional (solo si lo quiere cambiar)
        public int CodigoNivel { get; set; }

    }
}