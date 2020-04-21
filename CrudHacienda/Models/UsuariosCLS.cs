using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class UsuariosCLS
    {
        [Required]
        [Display(Name ="Id")]
        public int IdUsuario { get; set; }
        /*-----------*/
        [Required]
        [Display(Name = "Nombre Usuario")]
        public string NombreUsuario { get; set; }
        /*-----------*/
        [Required]
        [StringLength(100, ErrorMessage ="El numero maximo de caracteres aceptados es: 100")]
        [Display(Name = "Password")]
        public string Contrasena { get; set; }
        /*-----------*/
        [Display(Name = "Tipo Usuario")]
        public string TipoUsuario { get; set; }
        /*-----------*/
        [Required(ErrorMessage ="El codigo del empleado es necesario")]
        [Display(Name = "Codigo Empleado")]
        public int CodigoEmpleado { get; set; }
        /*-----------*/
        [Display(Name = "Empleado")]
        public string Empleado { get; set; }
    }
}