using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

public partial class categoria
{
    [Key]
    public int codigoCategorias { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string nombreCategorias { get; set; } = null!;

    [InverseProperty("codigoCategoriasNavigation")]
    public virtual ICollection<subCategoria> subCategoria { get; set; } = new List<subCategoria>();
}
