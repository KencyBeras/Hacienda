﻿using System;
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
        [Display(Name = "Password")]
        public string Contrasena { get; set; }
        /*-----------*/
        [Display(Name = "Tipo Usuario")]
        [Required]
        public string TipoUsuario { get; set; }
        /*-----------*/
        [Required(ErrorMessage ="El codigo del empleado es necesario")]
        [Display(Name = "Codigo Empleado")]
        public int CodigoEmpleado { get; set; }
    }
}