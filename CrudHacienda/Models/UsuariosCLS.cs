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
        [Display(Name ="Id Usuario")]
        public string IdUsuario { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Pass { get; set; }
        [Display(Name = "Tipo Usuario")]
        [Required]
        public string Permisos { get; set; }
        [Display(Name = "Codigo Empleado")]
        public Nullable<int> CodigoEmpleado { get; set; }
    }
}