using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace CrudHacienda.Filtros
{
    public class Acceder: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = HttpContext.Current.Session["usuario"];
            if (usuario == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");

            }
            base.OnActionExecuting(filterContext);
        }
    }

}