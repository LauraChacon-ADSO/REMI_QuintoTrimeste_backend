using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_REMI_WebApi.Models;

[Index("nombreCliente", "apellidoCliente", Name = "IndexNombrecliente")]
[Index(nameof(correoCliente), IsUnique = true)]
[Index(nameof(telefonoCliente), IsUnique = true)]
[Table("cliente")]
public partial class cliente
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

    [InverseProperty("documentoClienteNavigation")]
    public virtual ICollection<pedido> pedidos { get; set; } = new List<pedido>();
}
