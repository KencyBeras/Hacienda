using CrudHacienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace CrudHacienda.Controllers
{
    public class ProduccionController : Controller
    {
        // GET: Produccion
        public ActionResult Index()
        {
            //List<ProductosCLS> ListaProductos = new List<ProductosCLS>();
            ListaEmpleados();
            ListaPP();
            using (var db = new MyonexionEntities())
            {
               var DetalleProduccion = (from prod in db.Produccion
                                        join detprod in db.DetalleProduccion on prod.IdProduccion equals detprod.IdProduccion
                                        join emp in db.Empleados on prod.Despachado equals emp.IdEmpleado
                                        join prov in db.Proveedores on prod.Proveedor equals prov.IdProveedor
                                        join product in db.MisProductos on prod.IdProducto equals product.IdProducto 
                                          select new ProduccionCLS
                                          {
                                             Fecha = detprod.FechaProduccion,
                                             Producto = product.Producto,
                                             Turno = prod.Turno,
                                             EstadoFacturacion = prod.EstadoFacturacion,
                                             AProveedor = prov.NombreProveedor + " " + prov.SegundoNombre,
                                             ElEmpleado = emp.Nombre + " " + emp.Apellidos,
                                             UndsDespachadas = detprod.Cantidad + " " + prod.Unidad,//ojo con eso
                                             PrecioVenta = detprod.PrecioVenta,
                                             TotalVenta = detprod.TotalVenta

                                          }).ToList();

                return View(DetalleProduccion);
            }
        }

        /*Filtras la busqueda de productos*/
        public ActionResult FiltroProduccion(ProduccionCLS prodcls, string Busqueda, DateTime FechaInicio, DateTime FechaFin)
        {
            List<ProduccionCLS> ListaProducccion = new List<ProduccionCLS>();
            using (var db = new MyonexionEntities())
            {

                if (FechaFin == null)
                {
                    ListaProducccion = (from prod in db.Produccion
                                        join detprod in db.DetalleProduccion on prod.IdProduccion equals detprod.IdProduccion
                                        join emp in db.Empleados on prod.Despachado equals emp.IdEmpleado
                                        join prov in db.Proveedores on prod.Proveedor equals prov.IdProveedor
                                        join product in db.MisProductos on prod.IdProducto equals product.IdProducto
                                        select new ProduccionCLS
                                        {
                                            Fecha = detprod.FechaProduccion,
                                            Producto = product.Producto,
                                            Turno = prod.Turno,
                                            EstadoFacturacion = prod.EstadoFacturacion,
                                            AProveedor = prov.NombreProveedor + " " + prov.SegundoNombre,
                                            ElEmpleado = emp.Nombre + " " + emp.Apellidos,
                                            UndsDespachadas = detprod.Cantidad + " " + prod.Unidad,//ojo con eso
                                            PrecioVenta = detprod.PrecioVenta,
                                            TotalVenta = detprod.TotalVenta
                                        
                                        }).ToList();


                }
                else
                {
                    ListaProducccion = (from prod in db.Produccion
                                        join detprod in db.DetalleProduccion on prod.IdProduccion equals detprod.IdProduccion
                                        where detprod.FechaProduccion >= FechaInicio && detprod.FechaProduccion <= FechaFin
                                        join emp in db.Empleados on prod.Despachado equals emp.IdEmpleado
                                        join prov in db.Proveedores on prod.Proveedor equals prov.IdProveedor
                                        join product in db.MisProductos on prod.IdProducto equals product.IdProducto
                                        select new ProduccionCLS
                                        {
                                            Fecha = detprod.FechaProduccion,
                                            Producto = product.Producto,
                                            Turno = prod.Turno,
                                            EstadoFacturacion = prod.EstadoFacturacion,
                                            AProveedor = prov.NombreProveedor + " " + prov.SegundoNombre,
                                            ElEmpleado = emp.Nombre + " " + emp.Apellidos,
                                            UndsDespachadas = detprod.Cantidad + " " + prod.Unidad,//ojo con eso
                                            PrecioVenta = detprod.PrecioVenta,
                                            TotalVenta = detprod.TotalVenta
                                        
                                        }).ToList();
                }

            }

            return PartialView("_TablaProduccion", ListaProducccion);
        }

        /*Lista de productos*/
        public void ListaPP()
        {
            List<SelectListItem> ListaPP = new List<SelectListItem>();
            using (var db = new MyonexionEntities())
            {
                List<SelectListItem> listaProductos = (from item in db.MisProductos
                                                       where item.Estado == 1
                                                       select new SelectListItem
                                                       {
                                                           Text = item.Producto,
                                                           Value = item.IdProducto.ToString()
                                                       }).ToList();

                List<SelectListItem> listaProveedores = (from item in db.Proveedores
                                                         where item.Estado == 1
                                                         select new SelectListItem
                                                         {
                                                             Text = item.NombreProveedor + " " + item.SegundoNombre,
                                                             Value = item.IdProveedor.ToString()
                                                         }).ToList();

                ListaPP.AddRange(listaProveedores);
                listaProveedores = listaProveedores.OrderBy(p => p.Text).ToList();
                listaProveedores.Insert(0, new SelectListItem { Text = "Proveedor: ---Seleccionar---", Value = "" });
                ViewBag.listaProveedor = listaProveedores;
                //----------------------------------------------------------------------------------------------
                ListaPP.AddRange(listaProductos);
                listaProductos = listaProductos.OrderBy(p => p.Text).ToList();
                listaProductos.Insert(0, new SelectListItem { Text = "Producto: ---Seleccionar---", Value = "" });
                ViewBag.listaProducto = listaProductos;
            }
        }

        /*Lista de empleados*/
        public void ListaEmpleados()
        {
            List<SelectListItem> ListaEmpleados;
            using (var db = new MyonexionEntities())
            {
                ListaEmpleados = (from item in db.Empleados
                                  where item.Estado == 1
                                  select new SelectListItem
                                  {
                                      Text = item.Nombre + " " + item.Apellidos,
                                      Value = item.IdEmpleado.ToString()
                                  }).ToList();

                ListaEmpleados.Insert(0, new SelectListItem { Text = "Empleados: ---Seleccionar---", Value = "" });
            }

            ViewBag.FlistaEmpleado = ListaEmpleados;
        }

        [HttpPost]
        public string ProducciondelDia(ProduccionCLS PCLS, DetalleProduccionCLS detprodCLS, int Titulo)
        {
            string respuesta = "";
            try
            {
                using (var db = new MyonexionEntities())
                {
                    using (var transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if (Titulo == -1)
                            {
                                Produccion prod = new Produccion();
                                prod.IdProducto = PCLS.IdProducto;
                                prod.Unidad = PCLS.Unidad;
                                prod.Turno = PCLS.Turno;
                                prod.EstadoFacturacion = PCLS.EstadoFacturacion;
                                prod.Proveedor = PCLS.Proveedor;
                                prod.Despachado = PCLS.Despachado;
                                db.Produccion.Add(prod);
                                int IdProd = prod.IdProduccion;//Temporal

                                DetalleProduccion detprod = new DetalleProduccion();
                                detprod.IdProduccion = IdProd;
                                detprod.Cantidad = detprodCLS.Cantidad;
                                detprod.PrecioVenta = detprodCLS.PrecioVenta;
                                detprod.FechaProduccion = detprodCLS.FechaProduccion;
                                db.DetalleProduccion.Add(detprod);
                                respuesta = db.SaveChanges().ToString();
                                transaccion.Commit();
                            }
                            if (Titulo >=0)
                            {

                            }

                            //throw new Exception();

                        }
                        catch
                        {
                            transaccion.Rollback();

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
    }
}