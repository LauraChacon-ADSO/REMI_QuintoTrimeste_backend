using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class CrearSubcategoriaDTO
    {

        [StringLength(45)]
        [Unicode(false)]
        public string nombreSubCategorias { get; set; } = null!;

        public int codigoCategorias { get; set; }
    }
}
