using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class ProductosCLS
    {

        [Required]
        [Display(Name = "ID Producto")]
        public int IdProducto { get; set; }
        /*---------------------------------------*/
        [Required]
        [Display(Name ="Nombre Producto")]
        public string Producto { get; set; }
        /*---------------------------------------*/
        [Required]
        [Display(Name = "Descripcion Producto")]
        public string Descripcion { get; set; }
        /*---------------------------------------*/
        [Required]
        [Display(Name = "Estado")]
        public Nullable<int> Estado { get; set; }
        /*---------------------------------------*/
        [Required]
        [Display(Name = "Fecha Creacion")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaCreacion { get; set; }
        /*---------------------------------------*/
        [Display(Name = "Ultima Actualizacion")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        /*Variable adicional*/
        public string mensaje { get; set; }
    }
}