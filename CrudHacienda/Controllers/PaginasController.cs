using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Models;
using CrudHacienda.Filtros;

namespace CrudHacienda.Controllers
{
    [Acceder]
    public class PaginasController : Controller
    {
        // GET: Paginas
        public ActionResult Index()
        {
            List<PaginasCLS> ListaPaginas = new List<PaginasCLS>();
            using (var db = new MyonexionEntities())
            {
                ListaPaginas = (from Pagina in db.Pagina
                                select new PaginasCLS
                                {
                                   IDPAGINA = Pagina.IDPAGINA,
                                   MENSAJE = Pagina.MENSAJE,
                                   ACCION = Pagina.ACCION,
                                   CONTROLADOR = Pagina.CONTROLADOR                                      

                                  }).ToList();
                
                return View(ListaPaginas);
            }
        }

        /*Accoin Agregar usuarios*/
        public string Agregarpaginas(PaginasCLS pcls, int Titulo)
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
                            Pagina pag = new Pagina();
                            pag.MENSAJE = pcls.MENSAJE;
                            pag.ACCION = pcls.ACCION;
                            pag.CONTROLADOR = pcls.CONTROLADOR;
                            db.Pagina.Add(pag);
                            respuesta = db.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";

                        }
                        if (Titulo >= 0)
                        {//if para la editar datos
                            Pagina pag = db.Pagina.Where(p => p.IDPAGINA == Titulo).First();
                            pag.MENSAJE = pcls.MENSAJE;
                            pag.ACCION = pcls.ACCION;
                            pag.CONTROLADOR = pcls.CONTROLADOR;
                            respuesta = db.SaveChanges().ToString();
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
        public JsonResult RecuperarPagina(int idpagina)
        {
            PaginasCLS pcls = new PaginasCLS();
            using (var db = new MyonexionEntities())
            {
                Pagina pag = db.Pagina.Where(p => p.IDPAGINA == idpagina).First();
                pcls.MENSAJE = pag.MENSAJE;
                pcls.ACCION = pag.ACCION;
                pcls.CONTROLADOR = pag.CONTROLADOR;
            }

            return Json(pcls, JsonRequestBehavior.AllowGet);
        }
    }
}