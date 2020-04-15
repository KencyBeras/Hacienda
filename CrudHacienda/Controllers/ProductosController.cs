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
        //(Index) al momenot de iniacira la App de esta forma aparecera usuarios
        public ActionResult Index()
        {
            List<ProductosCLS> ListaProductos = new List<ProductosCLS>();
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
        public ActionResult Fproductos(ProductosCLS prodcls, string Busqueda)
        {
            List<ProductosCLS> ListaProductos = new List<ProductosCLS>();
            using (var db = new MyonexionEntities())
            {
                if (Busqueda == null)
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
                                      where( MisProductos.Estado == 1 && MisProductos.Producto.Contains(Busqueda)) ||
                                     (MisProductos.Estado == 1 && MisProductos.Descripcion.Contains(Busqueda))
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


        ///*AgregarProductos*/
        //public ActionResult AgregarProductos()
        //{
           
        //        return View();
        //}

        [HttpPost]
        public string AgregarProductos(ProductosCLS PCLS,int Titulo)
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
                    /*------***=================***------*/
                }
                else
                {
                    using (var db = new MyonexionEntities())
                    {
                        if (Titulo == -1)
                        {
                            //If para la insercion de datos
                            MisProductos MP = new MisProductos();
                            MP.Producto = PCLS.Producto;
                            MP.Descripcion = PCLS.Descripcion;
                            MP.Estado = (int)PCLS.Estado;
                            MP.FechaCreacion = PCLS.FechaCreacion;
                            MP.FechaActualizacion = MP.FechaCreacion;
                            db.MisProductos.Add(MP);
                            respuesta = db.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";
                        }
                        if (Titulo >= 0)
                        {
                            //if para la editar datos
                            MisProductos MP = db.MisProductos.Where(p => p.IdProducto == Titulo).First();
                            MP.Producto = PCLS.Producto;
                            MP.Descripcion = PCLS.Descripcion;
                            MP.Estado = (int)PCLS.Estado;
                            MP.FechaActualizacion = PCLS.FechaCreacion; 
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
        public JsonResult RecuperarProductos(int IdProductos)
        {                                   //La variable Productos es enviada desde la clase Editar en el index
            ProductosCLS pcls = new ProductosCLS();
            using (var db = new MyonexionEntities())
            {
                MisProductos Mprod = db.MisProductos.Where(p => p.IdProducto == IdProductos).First();
                pcls.Producto = Mprod.Producto;
                pcls.Descripcion = Mprod.Descripcion;
                pcls.Estado = Mprod.Estado;
                pcls.FechaActualizacion = Mprod.FechaActualizacion;
            }
            return Json(pcls, JsonRequestBehavior.AllowGet);
        }
        /*------***============***------*/

        /*Eliminar productos*/
        public int EliminarRegistro(int idproducto)
        {
            int respuesta = 0;
            try
            {
                using (var db = new MyonexionEntities())
                {
                    MisProductos pro = db.MisProductos.Where(p => p.IdProducto == idproducto).First();
                    pro.Estado = 0;
                    respuesta = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
            }
            return respuesta;
        }
    }
}