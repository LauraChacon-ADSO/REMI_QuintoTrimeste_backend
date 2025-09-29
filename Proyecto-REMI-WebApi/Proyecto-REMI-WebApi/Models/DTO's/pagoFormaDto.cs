using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_REMI_WebApi.Models.DTO_s
{
    public class pagoFormaDto
    {
        public int codigoFormaPago { get; set; }

        public string nombreFormaPago { get; set; } = null!;

        public double valorPago { get; set; }

    }
}
