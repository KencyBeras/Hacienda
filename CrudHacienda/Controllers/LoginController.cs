using CrudHacienda.Models;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CrudHacienda.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult CerrarSeccion()
        {
            return RedirectToAction("Index");
        }

        public string Login(UsuariosCLS userCLS)
        {
            string mensaje = "";
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();
                mensaje += "<ul class='list-group'>";
                foreach (var item in query)
                {
                    mensaje += "<li class='list-group-item alert alert-danger'>" + item + "</li>";
                }
                mensaje += "</ul>";
                //Si el modelo devuleve un error lo atrapamos en la variable mensaje y lo enviamos a la vista
            }
            else
            {
                string Nusuario = userCLS.NombreUsuario;
                string pass = userCLS.Contrasena;
                /*Cifrar clave*/
                SHA256Managed sha = new SHA256Managed();
                byte[] byclave = Encoding.Default.GetBytes(pass);
                byte[] byclavecifrado = sha.ComputeHash(byclave);
                string fclavecifrado = BitConverter.ToString(byclavecifrado).Replace("-", "");
                /*Clave cifrada*/
                using (var db = new MyonexionEntities())
                {
                    int numeroVeces = db.Usuario.Where(p => p.NombreUsuario == Nusuario
                    && p.Contrasena == fclavecifrado).Count();

                    mensaje = numeroVeces.ToString();
                    if (mensaje == "0") mensaje = "Usuario o contrasena incorrctos";
                    else
                    {
                        //Objeto usuario
                        Usuario ousuario = db.Usuario.Where(p => p.NombreUsuario == Nusuario
                        && p.Contrasena == fclavecifrado).First();

                        Session["usuario"] = ousuario;

                        List<MenuCLS> listamenu = (from Usu in db.Usuario
                                                   join tusuario in db.TipoUsuario
                                                   on Usu.TipoUsuario equals tusuario.IdTipoUsuario
                                                   join rolp in db.RolPagina
                                                   on tusuario.IdTipoUsuario equals rolp.IDTIPOUSUARIO
                                                   join pag in db.Pagina
                                                   on rolp.IDPAGINA equals pag.IDPAGINA
                                                   where tusuario.IdTipoUsuario == ousuario.TipoUsuario && rolp.IDTIPOUSUARIO == Usu.TipoUsuario
                                                   select new MenuCLS
                                                   {
                                                       NombreAccion = pag.ACCION,
                                                       NombreControlador = pag.CONTROLADOR,
                                                       Mensaje = pag.MENSAJE


                                                   }).ToList();

                                Session["Rol"] = listamenu;
                    }
                }
            }

            return mensaje;

        }
    }
}