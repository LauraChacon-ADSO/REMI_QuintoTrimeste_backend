using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

public partial class subCategoria
{
    [Key]
    public int codigoSubCategorias { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string nombreSubCategorias { get; set; } = null!;

    public int codigoCategorias { get; set; }

    [ForeignKey("codigoCategorias")]
    [InverseProperty("subCategoria")]
    public virtual categoria codigoCategoriasNavigation { get; set; } = null!;

    [InverseProperty("codigoSubCategoriasNavigation")]
    public virtual ICollection<producto> productos { get; set; } = new List<producto>();
}
