using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class pagoReciboDto
    {
        public int codigoReciboVenta { get; set; }
        public int codigoFormaPago { get; set; }
        public string nombreFormaPago { get; set; } = null!;
        public decimal valorPago { get; set; }

    }
}
