using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Models;
using System.Text;

namespace CrudHacienda.Controllers
{
    public class UsuariosController : Controller
    {
        /*Metodo que lista los empleados existentes en mi db*/
        public void ListarEmpleados()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from Emp in db.Empleados
                         select new SelectListItem
                         {
                             Text = Emp.Nombre + " " + Emp.Apellidos,
                             Value = Emp.IdEmpleado.ToString()

                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "Empleado --Seleccionar--", Value = "" });
                ViewBag.ListaEmpleados= Lista;

            }

        }

        // GET: Usuarios
        public ActionResult Index()
        {
            ListarEmpleados();
            List<UsuariosCLS> ListaUsuarios = new List<UsuariosCLS>();
            using (var db = new MyonexionEntities())
            {
                ListaUsuarios = (from RVU in db.Usuarios
                                             select new UsuariosCLS
                                             {
                                                 IdUsuario = RVU.Usuario,
                                                 Permisos = RVU.Permisos,
                                                 CodigoEmpleado = RVU.CodEmpleado
                                             }).ToList();

            }

                return View(ListaUsuarios);
        }

        public ActionResult FiltroUsuarios(string Usuario)
        {
            List<UsuariosCLS> ListaUsuarios = new List<UsuariosCLS>();
            using (var db = new MyonexionEntities())
            {
                if (Usuario == null)
                {
                    ListaUsuarios = (from RVU in db.Usuarios
                                     select new UsuariosCLS
                                     {
                                         IdUsuario = RVU.Usuario,
                                         Permisos = RVU.Permisos,
                                         CodigoEmpleado = RVU.CodEmpleado
                                     }).ToList();

                }
                else
                {
                    ListaUsuarios = (from RVU in db.Usuarios
                                     where RVU.Usuario.Contains(Usuario)
                                     select new UsuariosCLS
                                     {
                                         IdUsuario = RVU.Usuario,
                                         Permisos = RVU.Permisos,
                                         CodigoEmpleado = RVU.CodEmpleado
                                     }).ToList();

                }
                return PartialView("_TableP",ListaUsuarios); 
            }


        }

        public string AgregarUsuarios(UsuariosCLS ucls,int Titulo)
        {
            string respuesta = "";
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();
                respuesta += "<ul class='list-group'>";
                foreach (var item in query)
                {

                    respuesta += "<li class='list-group-item'>" +item+"</li>";
                }

                respuesta += "</ul>";
            }
            else
            {

           
                using (var db = new MyonexionEntities())
                {
                    if (Titulo.Equals(-1))
                    {
                        Usuarios user = new Usuarios();
                        user.Usuario = ucls.IdUsuario;
                        /*Cifrar clave
                        SHA256Managed sha = new SHA256Managed();
                        byte[] byclave = Encoding.Default.GetBytes(ucls.Pass);
                        byte[] byclavecifrado = sha.ComputeHash(byclave);
                        string fclavecifrado = BitConverter.ToString(byclavecifrado).Replace("-", "");
                        Clave cifrada*/
                        user.Clave = ucls.Pass;
                        user.Permisos = ucls.Permisos;
                        user.CodEmpleado = ucls.CodigoEmpleado;
                        db.Usuarios.Add(user);
                        respuesta = db.SaveChanges().ToString();

                    }
                }
            }

            return respuesta;
        }

    }
}