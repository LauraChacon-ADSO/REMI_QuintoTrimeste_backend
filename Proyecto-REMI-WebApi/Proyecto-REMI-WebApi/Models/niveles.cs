using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

public partial class niveles
{
    [Key]
    public int codigoNivel { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string nombreNivel { get; set; } = null!;

    [InverseProperty("codigoNivelNavigation")]
    public virtual ICollection<usuario> usuarios { get; set; } = new List<usuario>();
}
