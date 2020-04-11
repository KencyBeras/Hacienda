using CrudHacienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudHacienda.Controllers
{
    public class ProductosController : Controller
    {
        public ActionResult Productos(ProductosCLS pcls)
        {
            List<ProductosCLS> ListaProductos = new List<ProductosCLS>();
            //string NombreProducto = pcls.Producto;
            using (var db = new MyonexionEntities())
            {
                ListaProductos = (from MisProductos in db.MisProductos
                                  where MisProductos.Estado == 1
                                  select new ProductosCLS
                                  {
                                      IdProducto = MisProductos.IdProducto,
                                      Producto = MisProductos.Producto,
                                      Descripcion = MisProductos.Descripcion,
                                      Estado = MisProductos.Estado,
                                      FechaCreacion = MisProductos.FechaCreacion,
                                      FechaActualizacion = MisProductos.FechaActualizacion
                                  }).ToList();

                return View(ListaProductos);
            }


        }

        /*Filtras la busqueda de productos*/
        public ActionResult Fproductos(ProductosCLS prodcls, string Producto)
        {
           // string prod = prodcls.Producto;
            List<ProductosCLS> ListaProductos = new List<ProductosCLS>();
            using (var db = new MyonexionEntities())
            {
                if (Producto == null)
                {

                    ListaProductos = (from MisProductos in db.MisProductos
                                      where MisProductos.Estado == 1
                                      select new ProductosCLS
                                      {
                                          IdProducto = MisProductos.IdProducto,
                                          Producto = MisProductos.Producto,
                                          Descripcion = MisProductos.Descripcion,
                                          Estado = MisProductos.Estado,
                                          FechaActualizacion = MisProductos.FechaActualizacion
                                      }).ToList();
                }
                else
                {
                    ListaProductos = (from MisProductos in db.MisProductos
                                      where( MisProductos.Estado == 1 && MisProductos.Producto.Contains(Producto)) ||
                                     (MisProductos.Estado == 1 && MisProductos.Descripcion.Contains(Producto))
                                      select new ProductosCLS
                                      {
                                          IdProducto = MisProductos.IdProducto,
                                          Producto = MisProductos.Producto,
                                          Descripcion = MisProductos.Descripcion,
                                          Estado = MisProductos.Estado,
                                          FechaActualizacion = MisProductos.FechaActualizacion
                                      }).ToList();

                }

            }

            return PartialView("_TablaProductos",ListaProductos);
        }


        /*AgregarProductos*/
        public ActionResult AgregarProductos()
        {
           
                return View();
        }

        [HttpPost]
        public int AgregarProductos(ProductosCLS PCLS,int Titulo)
        {
            int respuesta = 0;
            using (var db = new MyonexionEntities())
            {
                if (Titulo==1)
                {
                    MisProductos MP = new MisProductos();
                    MP.Producto = PCLS.Producto;
                    MP.Descripcion = PCLS.Descripcion;
                    MP.Estado = (int)PCLS.Estado;
                    MP.FechaCreacion = PCLS.FechaCreacion;
                    db.MisProductos.Add(MP);
                    respuesta = db.SaveChanges();
                }
            }

            return respuesta;
        }

        /*Eliminar productos*/
        public ActionResult ELiminarProductos(int id)
        {
            using (var db = new MyonexionEntities())
            {
                MisProductos pro = db.MisProductos.Where(p => p.IdProducto.Equals(id)).First();
                pro.Estado = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Productos");
        }


        /*Editar productos*/
        [HttpGet]
        public ActionResult EditarProductos(int id)
        {
            ProductosCLS prodcls = new ProductosCLS();
            using (var db = new MyonexionEntities())
            {
                MisProductos mprod = db.MisProductos.Where(p => p.IdProducto.Equals(id)).First();

                prodcls.IdProducto = mprod.IdProducto;
                prodcls.Producto = mprod.Producto;
                prodcls.Descripcion = mprod.Descripcion;
                prodcls.Estado = mprod.Estado;
                prodcls.FechaActualizacion = mprod.FechaActualizacion; 
                db.SaveChanges();
            }

            return View(prodcls);
        }

        [HttpPost]
        public ActionResult EditarProductos(ProductosCLS prodcls)
        {
            if (!ModelState.IsValid)
            {
                return View(prodcls);
            }

            int Idproducto = prodcls.IdProducto;
            using (var db = new MyonexionEntities())
            {
                
                MisProductos mprod = db.MisProductos.Where(p => p.IdProducto.Equals(Idproducto)).First();

                mprod.IdProducto = prodcls.IdProducto;
                mprod.Producto = prodcls.Producto;
                mprod.Descripcion = prodcls.Descripcion;
                mprod.Estado = (int)prodcls.Estado;
                mprod.FechaActualizacion = prodcls.FechaActualizacion;
                db.SaveChanges();
            }

            return RedirectToAction("Productos");
        }

    }
}