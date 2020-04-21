using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class RolPaginaCLS
    {
        public int IIDROLPAGINA { get; set; }
        public Nullable<int> IDTIPOUSUARIO { get; set; }
        public Nullable<int> IDPAGINA { get; set; }

        public string Pagina { get; set; }
        public string TipoUsuario { get; set; }
    }
}