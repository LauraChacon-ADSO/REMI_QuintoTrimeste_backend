using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class CrearCategoriaDTO
    {
        [StringLength(45)]
        [Unicode(false)]
        public string nombreCategorias { get; set; } = null!;
    }
}
