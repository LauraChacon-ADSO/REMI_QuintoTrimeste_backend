using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class EditarEntradaProductoDto
    {

        public DateOnly fechaEntrada { get; set; }

        [StringLength(45)]
        [Unicode(false)]
        public string numeroFacturaEntrada { get; set; } = null!;
    }
}
