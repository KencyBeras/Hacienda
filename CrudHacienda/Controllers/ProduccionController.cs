using CrudHacienda.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Filtros;

namespace CrudHacienda.Controllers
{
    [Acceder]
    public class ProduccionController : Controller
    {
        //Generar un reporte en pdf
        public FileResult GenerarPDF()
        {
            Document doc = new Document();//Instanciamos la clase Document
            byte[] buffer;//Se declara un array de byte

            //Se guarda el PDF en memoria
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();
                //Titulo del reporte
                Paragraph title = new Paragraph("Reporte de produccion diario");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                //Espacio en blanco
                Paragraph tespacio = new Paragraph(" ");
                doc.Add(tespacio);
                //Columnas(Tabla)
                PdfPTable table = new PdfPTable(10);//10 es el numero de columnas que tendra la tabla
                table.WidthPercentage = 100f;
                //Anchos de cada columna en px
                float[] values = new float[10] {20,65,65,75,50, 70, 70, 70, 45,40};
                //Se asignas esos anchos a la tabla
                table.SetWidths(values);

                //Se agregan la Celdas a la tabla
                //Celda1;
                PdfPCell celda1 = new PdfPCell(new Phrase("ID"));
                celda1.BackgroundColor = new BaseColor(130,130,130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda1);
                //Celda 2;
                PdfPCell celda2 = new PdfPCell(new Phrase("Fecha"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda2);
                //Celda 3;
                PdfPCell celda3 = new PdfPCell(new Phrase("Producto"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda3.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda3);
                //Celda 4;
                PdfPCell celda4 = new PdfPCell(new Phrase("Turno"));
                celda4.BackgroundColor = new BaseColor(130, 130, 130);
                celda4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda4.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda4);
                //Celda 5;
                PdfPCell celda5 = new PdfPCell(new Phrase("Estado"));
                celda5.BackgroundColor = new BaseColor(130, 130, 130);
                celda5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda5.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda5);
                //Celda 6;
                PdfPCell celda6 = new PdfPCell(new Phrase("Proveedor"));
                celda6.BackgroundColor = new BaseColor(130, 130, 130);
                celda6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda6.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda6);
                //Celda 5;
                PdfPCell celda7 = new PdfPCell(new Phrase("Empleado"));
                celda7.BackgroundColor = new BaseColor(130, 130, 130);
                celda7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda7.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda7);
                //Celda 8;
                PdfPCell celda8 = new PdfPCell(new Phrase("Unidades"));
                celda8.BackgroundColor = new BaseColor(130, 130, 130);
                celda8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda8.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda8);
                //Celda 9;
                PdfPCell celda9 = new PdfPCell(new Phrase("Precio"));
                celda9.BackgroundColor = new BaseColor(130, 130, 130);
                celda9.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda9.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda9);
                //celda1.AddElement;
                //Celda 10;
                PdfPCell celda10 = new PdfPCell(new Phrase("Total"));
                celda10.BackgroundColor = new BaseColor(130, 130, 130);
                celda10.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda10.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda10);

                List<ProduccionCLS> listaprodcls = (List<ProduccionCLS>)Session["ListaU"];
                int nreg = listaprodcls.Count;
                for (int i = 0; i < nreg; i++)
                {
                    table.AddCell(listaprodcls[i].IdProduccion.ToString());
                    table.AddCell(listaprodcls[i].Fecha.ToString());
                    table.AddCell(listaprodcls[i].Producto);
                    table.AddCell(listaprodcls[i].Turno);
                    table.AddCell(listaprodcls[i].EstadoFacturacion.ToString());
                    table.AddCell(listaprodcls[i].AProveedor);
                    table.AddCell(listaprodcls[i].ElEmpleado);
                    table.AddCell(listaprodcls[i].UndsDespachadas);
                    table.AddCell(listaprodcls[i].PrecioVenta.ToString());
                    table.AddCell(listaprodcls[i].TotalVenta.ToString());
                }

                doc.Add(table);
                doc.Close();
                buffer = ms.ToArray();
            }

            return File(buffer, "application/pdf");
        }

        // GET: Produccion
        public ActionResult Index()
        {
           // List<ProduccionCLS> ListaProduccion = new List<ProduccionCLS>();
            ListarEmpleados();
            ListarProductos();
            ListarProveedores();
            List<ProduccionCLS> ListaProduccion = new List<ProduccionCLS>();
            using (var db = new MyonexionEntities())
            {
               ListaProduccion = (from prod in db.Produccion
                                  join detprod in db.DetalleProduccion on prod.IdProduccion equals detprod.IdProduccion
                                  join emp in db.Empleados on prod.Despachado equals emp.IdEmpleado
                                  join prov in db.Proveedores on prod.Proveedor equals prov.IdProveedor
                                  join product in db.MisProductos on prod.IdProducto equals product.IdProducto 
                                  orderby detprod.FechaProduccion descending
                                  select new ProduccionCLS
                                  {
                                     IdProduccion = prod.IdProduccion,
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

                Session["ListaU"] = ListaProduccion;
            }
                return View(ListaProduccion);
        }

        /*Filtras la busqueda de productos*/
        public ActionResult FiltroProduccion(ProduccionCLS prodcls, string Busqueda, DateTime FechaInicio, DateTime FechaFin)
        {
            List<ProduccionCLS> ListaProduccion = new List<ProduccionCLS>();
            using (var db = new MyonexionEntities())
            {

                if (FechaFin == null)
                {
                    ListaProduccion = (from prod in db.Produccion
                                       join detprod in db.DetalleProduccion on prod.IdProduccion equals detprod.IdProduccion
                                       join emp in db.Empleados on prod.Despachado equals emp.IdEmpleado
                                       join prov in db.Proveedores on prod.Proveedor equals prov.IdProveedor
                                       join product in db.MisProductos on prod.IdProducto equals product.IdProducto
                                       orderby detprod.FechaProduccion descending
                                       select new ProduccionCLS
                                       {
                                            IdProduccion = prod.IdProduccion,
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

                    Session["ListaU"] = ListaProduccion;
                }
                else
                {
                    ListaProduccion = (from prod in db.Produccion
                                       join detprod in db.DetalleProduccion on prod.IdProduccion equals detprod.IdProduccion
                                       where detprod.FechaProduccion >= FechaInicio && detprod.FechaProduccion <= FechaFin
                                       join emp in db.Empleados on prod.Despachado equals emp.IdEmpleado
                                       join prov in db.Proveedores on prod.Proveedor equals prov.IdProveedor
                                       join product in db.MisProductos on prod.IdProducto equals product.IdProducto
                                       orderby detprod.FechaProduccion descending
                                       select new ProduccionCLS
                                       {
                                            IdProduccion = prod.IdProduccion,
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

                    Session["ListaU"] = ListaProduccion;
                    //Variable global que elmacena la lista
                }
            }

            return PartialView("_TablaProduccion", ListaProduccion);
        }

        /*Lista de productos*/
        public void ListarProveedores()
        {
            List<SelectListItem> ListaProveedores;
            using (var db = new MyonexionEntities())
            {
                ListaProveedores = (from item in db.Proveedores
                                    select new SelectListItem
                                    {
                                        Text = item.NombreProveedor + " " + item.SegundoNombre,
                                        Value = item.IdProveedor.ToString()
                                    }).ToList();

                ListaProveedores.Insert(0, new SelectListItem { Text = "Proveedor: ---Seleccionar---", Value = "" });

            }
                ViewBag.listaProveedor = ListaProveedores;
        }
        /*Lista de Prodctos*/
        public void ListarProductos()
        {
            List<SelectListItem> ListaProductos;
            using (var db = new MyonexionEntities())
            {
                ListaProductos = (from item in db.MisProductos
                                  select new SelectListItem
                                  {
                                      Text = item.Producto,
                                      Value = item.IdProducto.ToString()
                                  }).ToList();

                ListaProductos.Insert(0, new SelectListItem { Text = "Productos: ---Seleccionar---", Value = "" });
            }                     
                ViewBag.listaProducto = ListaProductos;
        }

        /*Lista de empleados*/
        public void ListarEmpleados()
        {
            List<SelectListItem> ListaEmpleados;
            using (var db = new MyonexionEntities())
            {
                ListaEmpleados = (from item in db.Empleados
                                  select new SelectListItem
                                  {
                                      Text = item.Nombre,
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
                    /*------------*/
                }
                else {
                
                    using (var db = new MyonexionEntities())
                    {
                        using (var transaccion = db.Database.BeginTransaction())
                        {
                            try
                            {
                            /*Se trta de realizar la transaccion, si todo sale bien se ejecutara un commit*/
                                if (Titulo == -1)
                                {
                                    /*Primera insercion*/
                                    Produccion prod = new Produccion();
                                    prod.IdProducto = PCLS.IdProducto;
                                    prod.Unidad = PCLS.Unidad;
                                    prod.Turno = PCLS.Turno;
                                    prod.EstadoFacturacion = PCLS.EstadoFacturacion;
                                    prod.Proveedor = PCLS.Proveedor;
                                    prod.Despachado = PCLS.Despachado;
                                    db.Produccion.Add(prod);
                                    int IdProd = prod.IdProduccion;
                                    /*Insercion de los datos de la segunda tabla*/
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
                                    /*Update primera tabla*/
                                    //MisProductos MP = db.MisProductos.Where(p => p.IdProducto == Titulo).First();
                                    Produccion prod = db.Produccion.Where(p => p.IdProduccion == Titulo).First();
                                    prod.IdProducto = PCLS.IdProducto;
                                    prod.Unidad = PCLS.Unidad;
                                    prod.Turno = PCLS.Turno;
                                    prod.EstadoFacturacion = PCLS.EstadoFacturacion;
                                    prod.Proveedor = PCLS.Proveedor;
                                    prod.Despachado = PCLS.Despachado;
                                    respuesta = db.SaveChanges().ToString();
                                    int IdProd = prod.IdProduccion;//Temporal
                                    /*Update segunda tabla*/
                                    DetalleProduccion detprod = db.DetalleProduccion.Where(p => p.IdDetalleProduccion == Titulo).First();
                                    detprod.IdProduccion = IdProd;
                                    detprod.Cantidad = detprodCLS.Cantidad;
                                    detprod.PrecioVenta = detprodCLS.PrecioVenta;
                                    detprod.FechaProduccion = detprodCLS.FechaProduccion;
                                    respuesta = db.SaveChanges().ToString();
                                    transaccion.Commit();
                                }
                            }
                            catch
                            {
                                transaccion.Rollback();
                                /*Si ocurre un error durante la transaccion
                                todo el proceso se anulara*/
                            }
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

        /*Metodo que recupera los datos exixtentes de acuerdo al registro seleccionado*/
        public JsonResult DatosProduccion(int produccion)
        {                                   //La variable Productos es enviada desde la clase Editar en el index
            ListarEmpleados();
            ListarProductos();
            ListarProveedores();
            ProduccionCLS pcls = new ProduccionCLS();
            DetalleProduccionCLS dpcls = new DetalleProduccionCLS();
            using (var db = new MyonexionEntities())
            {
                Produccion Mprod = db.Produccion.Where(p => p.IdProduccion == produccion).First();
                pcls.IdProducto = Mprod.IdProducto;
                pcls.Unidad = Mprod.Unidad;
                pcls.Turno = Mprod.Turno;
                pcls.EstadoFacturacion = Mprod.EstadoFacturacion;
                pcls.Proveedor = (int)Mprod.Proveedor;
                pcls.Despachado = (int)Mprod.Despachado;
                DetalleProduccion dprod = db.DetalleProduccion.Where(p => p.IdDetalleProduccion == produccion).First();
                dpcls.Cantidad = dprod.Cantidad;
                dpcls.PrecioVenta = dprod.PrecioVenta;
                dpcls.FechaProduccion = dprod.FechaProduccion;
            }

            return Json(pcls, JsonRequestBehavior.AllowGet);
        }
        /*------***============***------*/
    }
}