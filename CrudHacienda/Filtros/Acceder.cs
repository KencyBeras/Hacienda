using CrudHacienda.Models;
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

            List<MenuCLS> roles = (List<MenuCLS>) HttpContext.Current.Session["Rol"];

            string nombreControldor = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string Accion = filterContext.ActionDescriptor.ActionName;

            int cantidad = roles.Where(p => p.NombreControlador == nombreControldor).Count();
            if (usuario == null || cantidad == 0)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}