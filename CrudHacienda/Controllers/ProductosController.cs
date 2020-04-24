using CrudHacienda.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Filtros;

namespace CrudHacienda.Controllers
{
    [Acceder]
    public class ProductosController : Controller
    {
        //Generar un reporte en pdf
        public FileResult imprimirPDF()
        {
            Document doc = new Document();//Instanciamos la clase Document
            byte[] buffer;//Se declara un array de byte

            //Se guarda el PDF en memoria
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();
                //Titulo del reporte
                Paragraph title = new Paragraph("Reporte de productos");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                //Espacio en blanco
                Paragraph tespacio = new Paragraph(" ");
                doc.Add(tespacio);
                //Columnas(Tabla)
                PdfPTable table = new PdfPTable(5);//5 es el numero de columnas que tendra la tabla
                table.WidthPercentage = 100f;
                //Anchos de cada columna en px
                float[] values = new float[5] { 20, 60, 130, 30, 80 };
                //Se asignas esos anchos a la tabla
                table.SetWidths(values);

                //Se agregan la Celdas a la tabla
                //Celda1;
                PdfPCell celda1 = new PdfPCell(new Phrase("ID"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda1);
                //Celda 2;
                PdfPCell celda2 = new PdfPCell(new Phrase("Producto"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda2);
                //Celda 3;
                PdfPCell celda3 = new PdfPCell(new Phrase("Descripcion"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda3.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda3);
                //Celda 4;
                PdfPCell celda4 = new PdfPCell(new Phrase("Estado"));
                celda4.BackgroundColor = new BaseColor(130, 130, 130);
                celda4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda4.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda4);
                //Celda 5;
                PdfPCell celda5 = new PdfPCell(new Phrase("Fecha"));
                celda5.BackgroundColor = new BaseColor(130, 130, 130);
                celda5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                celda5.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda5);

                List<ProductosCLS> listaprodcls = (List<ProductosCLS>)Session["lista"];
                int nreg = listaprodcls.Count;
                for (int i = 0; i < nreg; i++)
                {
                    table.AddCell(listaprodcls[i].IdProducto.ToString());
                    table.AddCell(listaprodcls[i].Producto.ToString());
                    table.AddCell(listaprodcls[i].Descripcion);
                    table.AddCell(listaprodcls[i].Estado.ToString());
                    table.AddCell(listaprodcls[i].FechaActualizacion.ToString());
                }

                doc.Add(table);
                doc.Close();
                buffer = ms.ToArray();
            }

            return File(buffer, "application/pdf");
        }

        //Generar reporte Excel
        public FileResult generarEXCEL()
        {
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                //Todo el documento excel(Libro)
                ExcelPackage ep = new ExcelPackage();
                //Hoja de excel
                ep.Workbook.Worksheets.Add("Reporte");
                ExcelWorksheet ew = ep.Workbook.Worksheets[0];
                //Nombre de las columnas
                ew.Cells[1, 1].Value = "Id Producto";
                ew.Cells[1, 2].Value = "Producto";
                ew.Cells[1, 3].Value = "Descripcion";
                ew.Cells[1, 4].Value = "Estado";
                ew.Cells[1, 5].Value = "Fecha";
                ew.Column(1).Width = 20;
                ew.Column(2).Width = 50;
                ew.Column(3).Width = 90;
                ew.Column(4).Width = 50;
                ew.Column(5).Width = 50;
                using (var range = ew.Cells[1, 1, 1, 5])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);
                }

                List<ProductosCLS> lista = (List<ProductosCLS>)Session["lista"];
                int nregistros = lista.Count;
                for (int i = 0; i < nregistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].IdProducto;
                    ew.Cells[i + 2, 2].Value = lista[i].Producto;
                    ew.Cells[i + 2, 3].Value = lista[i].Descripcion;
                    ew.Cells[i + 2, 4].Value = lista[i].Estado;
                    ew.Cells[i + 2, 5].Value = lista[i].FechaActualizacion;
                }
                ep.SaveAs(ms);
                buffer = ms.ToArray();
            }
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        //Start(Index)
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

                Session["lista"] = ListaProductos;
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

                    Session["lista"] = ListaProductos;
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

                    Session["lista"] = ListaProductos;
                }
            }
            return PartialView("_TablaProductos",ListaProductos);
        }
        //Agregar productos
        [HttpPost]
        public string AgregarProductos(ProductosCLS PCLS,int Titulo)
        {
            string respuesta = "";//Se inicializa la variable respuesta como null o vacio
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
                    //Si mi modelo es valido abro una conexion a la DB
                    using (var db = new MyonexionEntities())
                    {
                        int NumRegExtt = 0;
                        if (Titulo.Equals(-1))
                        {
                            NumRegExtt = db.MisProductos.Where(p => p.Producto == PCLS.Producto).Count();
                            //Validando que no haya un registro igual al que se va a insertar
                            if (NumRegExtt >= 1)
                            {
                                respuesta = "-1";
                            }
                            else
                            {
                                //If para la insercion de datos
                                MisProductos MP = new MisProductos();
                                MP.Producto = PCLS.Producto;
                                MP.Descripcion = PCLS.Descripcion;
                                MP.Estado = (int)PCLS.Estado;
                                MP.FechaCreacion = DateTime.Now;
                                MP.FechaActualizacion = DateTime.Now;
                                db.MisProductos.Add(MP);
                                respuesta = db.SaveChanges().ToString();
                                if (respuesta == "0") respuesta = "";
                            }
                        }
                        if (Titulo >= 0)
                        {
                            NumRegExtt = db.MisProductos.Where(p => p.Producto == PCLS.Producto &&
                            p.IdProducto != Titulo).Count();
                            if (NumRegExtt >= 1)
                            {
                                respuesta = "-1";
                            }
                            else
                            {
                                //if para la editar datos
                                MisProductos MP = db.MisProductos.Where(p => p.IdProducto == Titulo).First();
                                MP.Producto = PCLS.Producto;
                                MP.Descripcion = PCLS.Descripcion;
                                MP.Estado = (int)PCLS.Estado;
                                MP.FechaActualizacion = DateTime.Now;
                                respuesta = db.SaveChanges().ToString();
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "";
                //En caso que ocurra un error en mi try lo atrapo en la variable respuesta
            }

            return respuesta;
        }

        /*Metodo que recupera los datos existentes de acuerdo al registro seleccionado*/
        public JsonResult RecuperarProductos(int IdProductos)//La variable IdProductos es enviada desde la clase Editar en el Index
        {                                   
            ProductosCLS pcls = new ProductosCLS();
            using (var db = new MyonexionEntities())
            {//Seabre una conexion a la base de datos y buscamos el registro que coincida con el id recibido
                MisProductos Mprod = db.MisProductos.Where(p => p.IdProducto == IdProductos).First();
                pcls.Producto = Mprod.Producto;
                pcls.Descripcion = Mprod.Descripcion;
                pcls.Estado = Mprod.Estado;
                pcls.FechaActualizacion = Mprod.FechaActualizacion;
            }
            return Json(pcls, JsonRequestBehavior.AllowGet);
            //Retornamos un json con los datos obtenidos
        }
        /*Eliminar productos*/
        public int EliminarRegistro(int idproducto)
        {//Recibimos el idproducto a eliminar
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