namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class ListarUsuariosDto
    {
        public string DocumentoUsuario { get; set; } = null!;
        public string TipoDocumentoUsuario { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string ApellidoUsuario { get; set; } = null!;
        public string? CorreoUsuario { get; set; }
        public int CodigoNivel { get; set; }
    }
}
