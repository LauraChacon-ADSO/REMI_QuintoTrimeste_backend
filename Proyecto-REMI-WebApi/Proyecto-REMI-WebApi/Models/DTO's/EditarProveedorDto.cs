using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class EditarProveedorDto
    {

        [Key]
        [StringLength(20)]
        [Unicode(false)]
        public string documentoProveedor { get; set; } = null!;

        [StringLength(45)]
        [Unicode(false)]
        public string tipoDocumentoProveedor { get; set; } = null!;

        [StringLength(45)]
        [Unicode(false)]
        public string nombreProveedor { get; set; } = null!;

        [StringLength(45)]
        [Unicode(false)]
        public string? correoProveedor { get; set; }

        [StringLength(45)]
        [Unicode(false)]
        public string? telefonoProveedor { get; set; }
    }
}
