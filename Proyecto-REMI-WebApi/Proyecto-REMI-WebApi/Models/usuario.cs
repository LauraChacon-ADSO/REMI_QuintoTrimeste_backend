using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_REMI_WebApi.Models;

[Table("usuario")]
public partial class usuario
{
    internal string ResetToken;
    internal DateTime? ResetTokenExpiry;

    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string documentoUsuario { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string tipoDocumentoUsuario { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string nombreUsuario { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string apellidoUsuario { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string? correoUsuario { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string password { get; set; } = null!;

    public int codigoNivel { get; set; }

    [ForeignKey("codigoNivel")]
    [InverseProperty("usuarios")]
    public virtual niveles codigoNivelNavigation { get; set; } = null!;
   
}
