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
        [Required(ErrorMessage = "El campo Cedula es requerido u obligatorio")]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Phone Number Required!")]
        public string Telefono { get; set; }
        [Display(Name = "Email/Correo")]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        [Required]
        [Display(Name = "Puesto/Funcion")]
        public int Puesto { get; set; }
        [Required]
        [Display(Name = "Fecha Entrada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaEntrada { get; set; }
        [Display(Name = "Ultima Actualizacion")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        [Required]
        public int Estado { get; set;}

        public string Nestado { get; set; }
        [Display(Name = "Nombre")]
        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }
        public string NPuesto { get; set; }
    }
}
