using CrudHacienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Filtros;

namespace CrudHacienda.Controllers
{
    [Acceder]
    public class ProveedoresController : Controller
    {
        /*Accion Lista proveedores */
        public ActionResult Index()
        {
            List<ProveedoresCLS> ListaProv = new List<ProveedoresCLS>();
            using (var db = new MyonexionEntities())
            {
                ListaProv = (from VRProv in db.Proveedores
                             where VRProv.Estado == 1
                             select new ProveedoresCLS
                             {
                                 IdProveedor = VRProv.IdProveedor,
                                 TipoProveedor = VRProv.TipoProveedor,
                                 NumDocumento = VRProv.NumDocumento,
                                 NombreProveedor = VRProv.NombreProveedor,
                                 SegundoNombre = VRProv.SegundoNombre,
                                 Celular = VRProv.Celular,
                                 Telefono = VRProv.Telefono,
                                 Email = VRProv.Email,
                                 Direccion = VRProv.Direccion,
                                 FechaActualizacion = VRProv.FechaActualizacion
                             }).ToList();

            }
                return View(ListaProv);
        }
        /*Filtro para busqueda de usuarios*/
        public ActionResult FiltroProveedores(ProveedoresCLS provcls, string NumDoc)
        {
            List<ProveedoresCLS> ListaProv = new List<ProveedoresCLS>();
            using (var db = new MyonexionEntities())
            {
                if (NumDoc == null)
                {
                    ListaProv = (from VRProv in db.Proveedores
                                 where VRProv.Estado == 1
                                 select new ProveedoresCLS
                                 {
                                     IdProveedor = VRProv.IdProveedor,
                                     TipoProveedor = VRProv.TipoProveedor,
                                     NumDocumento = VRProv.NumDocumento,
                                     NombreProveedor = VRProv.NombreProveedor,
                                     SegundoNombre = VRProv.SegundoNombre,
                                     Celular = VRProv.Celular,
                                     Telefono = VRProv.Telefono,
                                     Email = VRProv.Email,
                                     Direccion = VRProv.Direccion,
                                     FechaActualizacion = VRProv.FechaActualizacion
                                 }).ToList();
                }
                else
                {
                    ListaProv = (from VRProv in db.Proveedores
                                 where (VRProv.Estado == 1 && VRProv.NumDocumento.Contains(NumDoc)) ||
                                 (VRProv.Estado == 1 && VRProv.NombreProveedor.Contains(NumDoc)) ||
                                 (VRProv.Estado == 1 && VRProv.SegundoNombre.Contains(NumDoc))
                                 select new ProveedoresCLS
                                 {
                                     IdProveedor = VRProv.IdProveedor,
                                     TipoProveedor = VRProv.TipoProveedor,
                                     NumDocumento = VRProv.NumDocumento,
                                     NombreProveedor = VRProv.NombreProveedor,
                                     SegundoNombre = VRProv.SegundoNombre,
                                     Celular = VRProv.Celular,
                                     Telefono = VRProv.Telefono,
                                     Email = VRProv.Email,
                                     Direccion = VRProv.Direccion,
                                     FechaActualizacion = VRProv.FechaActualizacion
                                 }).ToList();

                }
            }

            return PartialView("_TablaProveedores", ListaProv);
        }
        /*Accion Agregar Proveedor*/

        [HttpPost]
        public string AgregarProveedores(ProveedoresCLS Mpro, int Titulo)
        {
            // ListarCombox();
            string respuesta = ""; //Numero de registtros afectados
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
                        respuesta += "<li class='list-group-item text-danger'>" + item + "</li>";
                    }
                    respuesta += "</ul>";
                    //Si el modelo devuleve un error lo atrapamos en la variable respuesta y lo enviamos a la vista
                }
                else
                {
                    using (var bd = new MyonexionEntities())
                    {
                        if (Titulo == -1)
                        {
                            Proveedores pro = new Proveedores();
                            pro.TipoProveedor = Mpro.TipoProveedor;
                            pro.TipoDocumento = Mpro.TipoDocumento;
                            pro.NumDocumento = Mpro.NumDocumento;
                            pro.NombreProveedor = Mpro.NombreProveedor;
                            pro.SegundoNombre = Mpro.SegundoNombre;
                            pro.Celular = Mpro.Celular;
                            pro.Telefono = Mpro.Telefono;
                            pro.Email = Mpro.Email;
                            pro.Direccion = Mpro.Direccion;
                            pro.FechaCreacion = DateTime.Now;
                            pro.FechaActualizacion = DateTime.Now;
                            pro.Estado = Mpro.Estado;
                            bd.Proveedores.Add(pro);
                            respuesta = bd.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";
                        }
                        if (Titulo >= 0)
                        {
                            Proveedores pro = bd.Proveedores.Where(p => p.IdProveedor == Titulo).First();
                            pro.TipoProveedor = Mpro.TipoProveedor;
                            pro.TipoDocumento = Mpro.TipoDocumento;
                            pro.NumDocumento = Mpro.NumDocumento;
                            pro.NombreProveedor = Mpro.NombreProveedor;
                            pro.SegundoNombre = Mpro.SegundoNombre;
                            pro.Celular = Mpro.Celular;
                            pro.Telefono = Mpro.Telefono;
                            pro.Email = Mpro.Email;
                            pro.Direccion = Mpro.Direccion;
                            pro.FechaActualizacion = DateTime.Now;
                            pro.Estado = Mpro.Estado;
                            respuesta = bd.SaveChanges().ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                respuesta = "";
            }

            return respuesta;
        }

        /*Accion Eliminar Proveedor*/
        public ActionResult EliminarProveedor(int id)
        {
            using (var db = new MyonexionEntities())
            {
                Proveedores pro = db.Proveedores.Where(p => p.IdProveedor.Equals(id)).First();
                pro.Estado = 0;
                db.SaveChanges();
            }

            return RedirectToAction("ListaProveedores");
        }

        /*Metodo que recupera los datos exixtentes de acuerdo al registro seleccionado*/
        public JsonResult RecuperarProveedores(int idproveedor)
        {
            ProveedoresCLS procls = new ProveedoresCLS();
            using (var db = new MyonexionEntities())
            {
                Proveedores prov = db.Proveedores.Where(p => p.IdProveedor == idproveedor).First();
                procls.IdProveedor = prov.IdProveedor;
                procls.NombreProveedor = prov.NombreProveedor;
                procls.SegundoNombre = prov.SegundoNombre;
                procls.TipoProveedor = prov.TipoProveedor;
                procls.TipoDocumento = prov.TipoDocumento;
                procls.NumDocumento = prov.NumDocumento;
                procls.Celular = prov.Celular;
                procls.Telefono = prov.Telefono;
                procls.Email = prov.Email;
                procls.Estado = prov.Estado;
                procls.Direccion = prov.Direccion;
                procls.FechaActualizacion = prov.FechaActualizacion;
            }

            return Json(procls, JsonRequestBehavior.AllowGet);
            /*------***============***------*/
        }

        /*Eliminar productos*/
        public int EliminarRegistro(int idproveedor)
        {//Recibimos el idproducto a eliminar
            int respuesta = 0;
            try
            {
                using (var db = new MyonexionEntities())
                {
                    Proveedores prov = db.Proveedores.Where(p =>p.IdProveedor == idproveedor).First();
                    prov.Estado = 0;
                    respuesta = db.SaveChanges();
                }
            }
            catch (Exception)
            {
                respuesta = 0;
            }
            return respuesta;
        }
    }
}