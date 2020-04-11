using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class ProveedoresCLS
    {
        [Display(Name = "Id Proveedor")]
        public int IdProveedor { get; set; }

        [Display(Name ="Tipo Proveedor")]
        [Required(ErrorMessage = "El campo Tipo Porveedore es requerido u obligatorio")]
        public string TipoProveedor { get; set; }

        [Display(Name = "Tipo Documento")]
        [Required(ErrorMessage = "El campo Tipo Documento es requerido u obligatorio")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El campo Cedula/RNC es requerido u obligatorio")]
        [Display(Name = "Cedula | RNC")]
        public string NumDocumento { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido u obligatorio")]
        [Display(Name = "Nombre Proveedor")]
        public string NombreProveedor { get; set; }

        [Display(Name = "Segundo Nombre")]
        [Required(ErrorMessage = "El campo Segundo Nombre es requerido u obligatorio")]
        public string SegundoNombre { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "El campo Celular es requerido u obligatorio")]
        public string Celular { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo Telefono es requerido u obligatorio")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un email valido ej. alguien@example.com")]
        public string Email { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El campo Direccion es requerido u obligatorio")]
        public string Direccion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "El campo Fecha es requerido u obligatorio")]
        public System.DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaActualizacion { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int Estado { get; set; }

        public string ProvErrors { get; set; }
        /*Campo combinado*/
        [Display(Name ="Nombre")]
        public string NombreCompleto { get { return NombreProveedor + " " + SegundoNombre; } }
   

    }
}