using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class EmpleadosCLS
    {
        public int IdEmpleado { get; set; }
        /*-----------------*/
        [Required]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }
        /*-----------------*/
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        /*-----------------*/
        [Required]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }
        /*-----------------*/
        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
        /*-----------------*/
        [Display(Name = "Telefono")]
        [Required]
        public string Telefono { get; set; }
        /*-----------------*/
        [Display(Name = "Email/Correo")]
        public string Email { get; set; }
        /*-----------------*/
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        /*-----------------*/
        [Required]
        [Display(Name = "Puesto/Funcion")]
        public int Puesto { get; set; }
        /*-----------------*/
        [Display(Name = "Fecha Entrada")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaEntrada { get; set; }
        /*-----------------*/
        [Display(Name = "Ultima Actualizacion")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        /*-----------------*/
        [Required]
        [Display(Name = "Estado")]
        public int Estado { get; set;}

        /*Propiedades adicioales*/
        public string Nestado { get; set; }
        [Display(Name = "Nombre")]
        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }
        public string NPuesto { get; set; }
    }
}
