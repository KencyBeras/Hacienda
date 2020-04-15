using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class DetalleProduccionCLS
    {
        public int IdDetalleProduccion { get; set; }

        public Nullable<int> IdProduccion { get; set; }

        public double Cantidad { get; set; }

        public double PrecioVenta { get; set; }

        public Nullable<System.DateTime> FechaProduccion { get; set; }

    }
}