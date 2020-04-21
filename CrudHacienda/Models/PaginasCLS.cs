using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudHacienda.Models
{
    public class PaginasCLS
    {
        public int IDPAGINA { get; set; }

        public string MENSAJE { get; set; }

        public string ACCION { get; set; }

        public string CONTROLADOR { get; set; }
    }
}