//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrudHacienda.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RolPagina
    {
        public int IIDROLPAGINA { get; set; }
        public Nullable<int> IDTIPOUSUARIO { get; set; }
        public Nullable<int> IDPAGINA { get; set; }
    
        public virtual Pagina Pagina { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}