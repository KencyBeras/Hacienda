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
    public class RolPaginaController : Controller
    {
        // GET: RolPagina
        public ActionResult Index()
        {
            ListarPagina();
            ListarTipoUsuario();
            List<RolPaginaCLS> ListaRoles = new List<RolPaginaCLS>();
            using (var db = new MyonexionEntities())
            {
                ListaRoles = (from rolp in db.RolPagina
                              join tipousuario in db.TipoUsuario
                              on rolp.IDTIPOUSUARIO equals tipousuario.IdTipoUsuario
                              join pag in db.Pagina
                              on rolp.IDPAGINA equals pag.IDPAGINA
                              orderby tipousuario.Privilegios
                              select new RolPaginaCLS
                              {
                                 IIDROLPAGINA = rolp.IIDROLPAGINA,
                                 TipoUsuario = tipousuario.Privilegios,
                                 Pagina = pag.MENSAJE                                  
                              }).ToList();
            }
            return View(ListaRoles);
        }

        public ActionResult FiltrarRolPagina(int Busqueda)
        {
            List<RolPaginaCLS> ListaRolPagina = new List<RolPaginaCLS>();
            using (var db = new MyonexionEntities())
            {
                if (Busqueda == 0 )
                {

                    ListaRolPagina = (from rolp in db.RolPagina
                                      join tipousuario in db.TipoUsuario
                                      on rolp.IDTIPOUSUARIO equals tipousuario.IdTipoUsuario
                                      join pag in db.Pagina
                                      on rolp.IDPAGINA equals pag.IDPAGINA
                                      orderby tipousuario.Privilegios
                                      select new RolPaginaCLS
                                      {
                                          IIDROLPAGINA = rolp.IIDROLPAGINA,
                                          TipoUsuario = tipousuario.Privilegios,
                                          Pagina = pag.MENSAJE
                                      }).ToList();
                }
                else
                {
                    ListaRolPagina = (from rolp in db.RolPagina
                                      join tipousuario in db.TipoUsuario
                                      on rolp.IDTIPOUSUARIO equals tipousuario.IdTipoUsuario
                                      join pag in db.Pagina
                                      on rolp.IDPAGINA equals pag.IDPAGINA
                                      orderby tipousuario.Privilegios
                                      where rolp.IDTIPOUSUARIO == Busqueda
                                      select new RolPaginaCLS
                                      {
                                          IIDROLPAGINA = rolp.IIDROLPAGINA,
                                          TipoUsuario = tipousuario.Privilegios,
                                          Pagina = pag.MENSAJE
                                      }).ToList();
                }

            }

            return PartialView("_TablaRolPagina", ListaRolPagina);
        }

        public void ListarTipoUsuario()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from tipoUsuario in db.TipoUsuario
                         select new SelectListItem
                         {
                             Text = tipoUsuario.Privilegios,
                             Value = tipoUsuario.IdTipoUsuario.ToString()

                         }).ToList();

                Lista.Insert(0, new SelectListItem { Text = "Tipo Usuario --Seleccionar--", Value = "0" });
                ViewBag.ListaTipoUsuario = Lista;
            }
        }

        public void ListarPagina()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from pagina in db.Pagina
                         select new SelectListItem
                         {
                             Text = pagina.MENSAJE,
                             Value = pagina.IDPAGINA.ToString()

                         }).ToList();

                Lista.Insert(0, new SelectListItem { Text = "Pagina --Seleccionar--", Value = "" });
                ViewBag.ListaPagina = Lista;
            }
        }

        /*Accoin rol*/
        public string AgregarRolPagina(RolPaginaCLS rpcls, int Titulo)
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
                            RolPagina rolp = new RolPagina();
                            rolp.IDTIPOUSUARIO = rpcls.IDTIPOUSUARIO;
                            rolp.IDPAGINA = rpcls.IDPAGINA;
                            db.RolPagina.Add(rolp);
                            respuesta = db.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";

                        }
                        if (Titulo >= 0)
                        {//if para la editar datos
                            RolPagina rolp = new RolPagina();
                            rolp.IDTIPOUSUARIO = rpcls.IDTIPOUSUARIO;
                            rolp.IDPAGINA = rpcls.IDPAGINA;
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
        public JsonResult RecuperarRol(int idrolpagina)
        {
            RolPaginaCLS rpcls = new RolPaginaCLS();
            using (var db = new MyonexionEntities())
            {
                RolPagina rolp = db.RolPagina.Where(p => p.IIDROLPAGINA == idrolpagina).First();

                rpcls.IDTIPOUSUARIO = rolp.IDTIPOUSUARIO;
                rpcls.IDPAGINA = rolp.IDPAGINA;
            }

            return Json(rpcls, JsonRequestBehavior.AllowGet);
        }

    }
}