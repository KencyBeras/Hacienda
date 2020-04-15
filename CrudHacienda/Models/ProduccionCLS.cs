using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class ProduccionCLS
    {

        public int IdProduccion { get; set; }

        public int IdProducto { get; set; }

        public string Unidad { get; set; }

        public string Turno { get; set; }

        public int EstadoFacturacion { get; set; }

        public int Proveedor { get; set; }

        public int Despachado { get; set; }

        //Propiedades virtuales
        public string Producto { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> Fecha { get; set; }
        public string AProveedor { get; set; }
        public string ElEmpleado { get; set; }
        public string UndsDespachadas { get; set; }
        public double PrecioVenta { get; set; }
        public double TotalVenta { get; set; }
    }
}