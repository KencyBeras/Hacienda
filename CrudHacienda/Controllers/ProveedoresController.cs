using CrudHacienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudHacienda.Controllers
{
    public class ProveedoresController : Controller
    {
        /*Accion Lista proveedores */
        
        public ActionResult ListaProveedores(ProveedoresCLS provCLS)
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
               
               
                    return View(ListaProv);
                } 
        }

        /*Filtro para busqueda de usuarios*/

        public ActionResult FiltroProveedores(ProveedoresCLS provcls, string NumDocumento)
        {

            List<ProveedoresCLS> ListaProv = new List<ProveedoresCLS>();
            using (var db = new MyonexionEntities())
            {
                if (NumDocumento == null)
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
                                 where (VRProv.Estado == 1 && VRProv.NumDocumento.Contains(NumDocumento)) ||
                                 (VRProv.Estado ==1 && VRProv.NombreProveedor.Contains(NumDocumento)) ||
                                 (VRProv.Estado == 1 && VRProv.SegundoNombre.Contains(NumDocumento))
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
        [HttpGet]
        public ActionResult AgregarProveedor()
        {
            return View();
        }

        [HttpPost]
        public int AgregarProveedores(ProveedoresCLS Mpro, int Titulo)
        {
           // ListarCombox();
            int respuesta = 1; //Numero de registtros afectados
            using (var bd = new MyonexionEntities())
            {
                if(Titulo ==1)
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
                    pro.FechaCreacion = Mpro.FechaCreacion;
                    pro.Estado = Mpro.Estado;
                    bd.Proveedores.Add(pro);
                    bd.SaveChanges();
                }
            }

            return respuesta;

        }

        /*accion Editar Proveedores*/
        [HttpGet]
        public ActionResult EditarProveedores(int id)
        {
            ProveedoresCLS pcls = new ProveedoresCLS();
            using (var db = new MyonexionEntities())
            {
                Proveedores prov = db.Proveedores.Where(p => p.IdProveedor.Equals(id)).First();
                pcls.IdProveedor = prov.IdProveedor;
                pcls.TipoDocumento = prov.TipoDocumento;
                pcls.NumDocumento = prov.NumDocumento;
                pcls.NombreProveedor = prov.NombreProveedor;
                pcls.SegundoNombre = prov.SegundoNombre;
                pcls.Celular = prov.Celular;
                pcls.Telefono = prov.Telefono;
                pcls.Email = prov.Email;
                pcls.Direccion = prov.Direccion;
                pcls.Estado = prov.Estado;
                pcls.FechaActualizacion = prov.FechaActualizacion;
                db.SaveChanges();
            }
            return View(pcls);
        }

        [HttpPost]
        public ActionResult EditarProveedores(ProveedoresCLS PROVCLS)
        {
            if (!ModelState.IsValid)
            {
                //ListarCombox();
                return View(PROVCLS);
            }

            int IdProveedor = PROVCLS.IdProveedor;
            using (var db = new MyonexionEntities())
            {
                //ListarCombox();
                //int nestado = 1;
                Proveedores prov = db.Proveedores.Where(p => p.IdProveedor.Equals(IdProveedor)).First();
                prov.IdProveedor = PROVCLS.IdProveedor;
                prov.TipoProveedor = PROVCLS.TipoProveedor;
                prov.NombreProveedor = PROVCLS.NombreProveedor;
                prov.SegundoNombre = PROVCLS.SegundoNombre;
                prov.TipoDocumento = PROVCLS.TipoDocumento;
                prov.NumDocumento = PROVCLS.NumDocumento;
                prov.Telefono = PROVCLS.Telefono;
                prov.Celular = PROVCLS.Celular;
                prov.Email = PROVCLS.Email;
                prov.Estado = PROVCLS.Estado;
                prov.Direccion = PROVCLS.Direccion;
                prov.FechaActualizacion = PROVCLS.FechaActualizacion;
                db.SaveChanges();
            }

            return RedirectToAction("ListaProveedores");
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
    }
}