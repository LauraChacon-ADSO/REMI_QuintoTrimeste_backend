using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class clientesInfoDto
    {
        [Key]
        [StringLength(20)]
        [Unicode(false)]
        public string documentoCliente { get; set; } = null!;

        [StringLength(45)]
        [Unicode(false)]
        public string tipoDocumentoCliente { get; set; } = null!;

        [StringLength(45)]
        [Unicode(false)]
        public string nombreCliente { get; set; } = null!;

        [StringLength(45)]
        [Unicode(false)]
        public string apellidoCliente { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El formato ingresado para el correo no es valido")]
        [StringLength(45)]
        [Unicode(false)]
        public string? correoCliente { get; set; }

        [RegularExpression(@"^\(\+57\)\s3\d{9}$", ErrorMessage = "El numero de telefono debe tener el formato(+57) 3xxxxxxxx")]
        [StringLength(45)]
        [Unicode(false)]
        public string? telefonoCliente { get; set; }
        public List<pedidoDetalleDto> detallesP { get; set; } = new();
    }
}
