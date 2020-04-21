using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Models;
using System.Text;
using CrudHacienda.Filtros;

namespace CrudHacienda.Controllers
{
    [Acceder]

    public class UsuariosController : Controller
    {
        /*Metodo que lista los empleados existentes en mi db,Con este metodo se crea 
        el select en tablas relacionadas para la insercion de los datos*/
        public void ListarEmpleados()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from Emp in db.Empleados
                         select new SelectListItem
                         {
                             Text = Emp.Nombre + " " + Emp.Apellidos,//La propiedad Text es loq ue el usuario vera en el Texbox
                             Value = Emp.IdEmpleado.ToString()//Value es el valor interno que tendra el Texbox, siempre deberia ser el PrimaryKey

                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "Empleado --Seleccionar--", Value = "" });
                /*Esta opcion crea un primer registro sin valor el cual se mostrara por defecto en el Texbox*/
                ViewBag.ListaEmpleados = Lista;
            }
        }

        /*Metodo que lista los empleados existentes en mi db,Con este metodo se crea 
       el select en tablas relacionadas para la insercion de los datos*/
        public void ListarTipoUsuarios()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from tipuser in db.TipoUsuario
                         select new SelectListItem
                         {
                             Text = tipuser.Privilegios,//La propiedad Text es loq ue el usuario vera en el Texbox
                             Value = tipuser.IdTipoUsuario.ToString()//Value es el valor interno que tendra el Texbox, siempre deberia ser el PrimaryKey

                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "Tipo Usuario --Seleccionar--", Value = "" });
                /*Esta opcion crea un primer registro sin valor el cual se mostrara por defecto en el Texbox*/
                ViewBag.ListaTipoUsuarios = Lista;
            }
        }

        //(Index) al momenot de iniacira la App de esta forma aparecera usuarios
        public ActionResult Index()
        {
            ListarEmpleados();
            ListarTipoUsuarios();
            List<UsuariosCLS> ListaUsuarios = new List<UsuariosCLS>();
            using (var db = new MyonexionEntities())
            {
                List<UsuariosCLS> FListaUsuarios =(from RVU in db.Usuario
                                                   join tipuser in db.TipoUsuario
                                                   on RVU.TipoUsuario equals tipuser.IdTipoUsuario
                                                   join emp in db.Empleados
                                                   on RVU.CodEmpleado equals emp.IdEmpleado
                                                   select new UsuariosCLS
                                                   {
                                                       IdUsuario = RVU.IdUsuario,
                                                       NombreUsuario = RVU.NombreUsuario,
                                                       TipoUsuario = tipuser.Privilegios,
                                                       Empleado = emp.Nombre + " " + emp.Apellidos
                                                   }).ToList();

                ListaUsuarios.AddRange(FListaUsuarios);                           
            }

                return View(ListaUsuarios);
        }

        /*Accion que hace un filtro de los usuarios de acuerdo a los parametros preestablecidos*/
        public ActionResult FiltroUsuarios(UsuariosCLS ucls,string Usuario)
        {
            List<UsuariosCLS> ListaUsuarios = new List<UsuariosCLS>();
            using (var db = new MyonexionEntities())
            {
                if (Usuario == null)
                {
                    List<UsuariosCLS> FListaUsuarios = (from RVU in db.Usuario
                                                        join tipuser in db.TipoUsuario
                                                        on RVU.TipoUsuario equals tipuser.IdTipoUsuario
                                                        join emp in db.Empleados
                                                        on RVU.CodEmpleado equals emp.IdEmpleado
                                                        where RVU.NombreUsuario.Contains(Usuario)
                                                        select new UsuariosCLS
                                                        {
                                                            IdUsuario = RVU.IdUsuario,
                                                            NombreUsuario = RVU.NombreUsuario,
                                                            TipoUsuario = tipuser.Privilegios,
                                                            Empleado = emp.Nombre + " " + emp.Apellidos
                                                        }).ToList();

                    ListaUsuarios.AddRange(FListaUsuarios);
                }
                else
                {
                    List<UsuariosCLS> FListaUsuarios = (from RVU in db.Usuario
                                                        join tipuser in db.TipoUsuario
                                                        on RVU.TipoUsuario equals tipuser.IdTipoUsuario
                                                        join emp in db.Empleados
                                                        on RVU.CodEmpleado equals emp.IdEmpleado
                                                        where RVU.NombreUsuario.Contains(Usuario)
                                                        select new UsuariosCLS
                                                        {
                                                            IdUsuario = RVU.IdUsuario,
                                                            NombreUsuario = RVU.NombreUsuario,
                                                            TipoUsuario = tipuser.Privilegios,
                                                            Empleado = emp.Nombre + " " + emp.Apellidos
                                                        }).ToList();

                    ListaUsuarios.AddRange(FListaUsuarios);

                }
                return PartialView("_TableUsuarios",ListaUsuarios); 
            }


        }

        /*Accoin Agregar usuarios*/
        public string AgregarUsuarios(UsuariosCLS ucls,int Titulo)
        {
            string respuesta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    respuesta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {

                        respuesta += "<li class='list-group-item'>" + item + "</li>";
                    }

                    respuesta += "</ul>";
                }
                else
                {
                    /*Si el modelo es valido se ejecutaran las siguientes leineas*/
                    using (var db = new MyonexionEntities())
                    {
                        if (Titulo == -1)
                        {//If para la insercion de datos
                            Usuario user = new Usuario();
                            user.IdUsuario = ucls.IdUsuario;
                            user.NombreUsuario = ucls.NombreUsuario;
                            /*Cifrar clave*/
                            SHA256Managed sha = new SHA256Managed();
                            byte[] byclave = Encoding.Default.GetBytes(ucls.Contrasena);
                            byte[] byclavecifrado = sha.ComputeHash(byclave);
                            string fclavecifrado = BitConverter.ToString(byclavecifrado).Replace("-", "");
                            /*Clave cifrada*/
                            user.Contrasena = fclavecifrado;
                            user.TipoUsuario = int.Parse(ucls.TipoUsuario);
                            user.CodEmpleado = ucls.CodigoEmpleado;
                            db.Usuario.Add(user);
                            respuesta = db.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";
                            
                        }
                        if (Titulo >= 0)
                        {//if para la editar datos
                            Usuario user = db.Usuario.Where(p => p.IdUsuario == Titulo).First();
                            user.NombreUsuario = ucls.NombreUsuario;
                            /*Cifrar clave*/
                            SHA256Managed sha = new SHA256Managed();
                            byte[] byclave = Encoding.Default.GetBytes(ucls.Contrasena);
                            byte[] byclavecifrado = sha.ComputeHash(byclave);
                            string fclavecifrado = BitConverter.ToString(byclavecifrado).Replace("-", "");
                            /*Clave cifrada*/
                            user.Contrasena = fclavecifrado;
                            user.TipoUsuario = int.Parse(ucls.TipoUsuario);
                            user.CodEmpleado = ucls.CodigoEmpleado;
                            respuesta = db.SaveChanges().ToString();
                        }
                    }
                }

            }
            catch (Exception ex)

            {
                respuesta = "";

            }

            return respuesta;
        }

        /*Metodo que recupera los datos exixtentes de acuerdo al registro seleccionado*/
        public JsonResult RecuperarUsuarios(int idusuario)
        {
            UsuariosCLS ucls = new UsuariosCLS();
            using (var db = new MyonexionEntities())
            {
                Usuario user = db.Usuario.Where(p => p.IdUsuario == idusuario).First();
                ucls.NombreUsuario = user.NombreUsuario;
                ucls.Contrasena = user.Contrasena;
                ucls.TipoUsuario = user.TipoUsuario.ToString();
                ucls.CodigoEmpleado = user.CodEmpleado;
            }

            return Json(ucls,JsonRequestBehavior.AllowGet);
        }
    }
}