using CrudHacienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudHacienda.Controllers
{
    public class EmpleadosController : Controller
    {
        public void ListarComboPuesto()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from puestos in db.Puestos
                         select new SelectListItem
                         {
                             Text = puestos.NomPuesto,
                             Value = puestos.IdPuesto.ToString()

                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "Puesto --Seleccionar--", Value = "" });
                ViewBag.ListaPuestos = Lista;

            }
        }

        public void ListarComboEstado()
        {
            List<SelectListItem> Lista;
            using (var db = new MyonexionEntities())
            {
                Lista = (from estado in db.EstadoEmpleado
                         select new SelectListItem
                         {
                             Text = estado.Estado,
                             Value = estado.IdStausEmpleado.ToString()

                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "Estado --Seleccionar--", Value = "" });
                ViewBag.ListaEstado = Lista;

            }
        }

        // GET: Empleados
        public ActionResult Index()
        {
            ListarComboEstado();
            ListarComboPuesto();
            List<EmpleadosCLS> ListaEmpleados = new List<EmpleadosCLS>();
            using (var db = new MyonexionEntities())
            {
                ListaEmpleados = (from Emp in db.Empleados
                                  join EE in db.EstadoEmpleado
                                  on Emp.Estado equals
                                  EE.IdStausEmpleado
                                  join cargo in db.Puestos
                                  on Emp.Puesto equals cargo.IdPuesto
                                  where Emp.Estado == 1

                                  select new EmpleadosCLS
                                  {
                                      IdEmpleado = Emp.IdEmpleado,
                                      Nombre = Emp.Nombre,
                                      Apellidos = Emp.Apellidos,
                                      Cedula = Emp.Cedula,
                                      Telefono = Emp.Telefono,
                                      Sexo = Emp.Sexo,
                                      Email = Emp.Email,
                                      Estado = Emp.Estado,
                                      Puesto = Emp.Puesto,
                                      NPuesto = cargo.NomPuesto,
                                      Nestado = EE.Estado
                                  }).ToList();


            }

            return View(ListaEmpleados);
        }


        public ActionResult FiltrarEmpleados(string Busqueda)
        {
            List<EmpleadosCLS> ListaEmpleados = new List<EmpleadosCLS>();
            using (var db = new MyonexionEntities())
            {
                if (Busqueda == null)
                {

                    ListaEmpleados = (from Emp in db.Empleados
                                      join EE in db.EstadoEmpleado
                                      on Emp.Estado equals
                                      EE.IdStausEmpleado
                                      join cargo in db.Puestos
                                      on Emp.Puesto equals cargo.IdPuesto
                                      where Emp.Estado == 1
                                      select new EmpleadosCLS
                                      {
                                          IdEmpleado = Emp.IdEmpleado,
                                          Nombre = Emp.Nombre,
                                          Apellidos = Emp.Apellidos,
                                          Cedula = Emp.Cedula,
                                          Telefono = Emp.Telefono,
                                          Sexo = Emp.Sexo,
                                          Email = Emp.Email,
                                          Estado = Emp.Estado,
                                          Puesto = Emp.Puesto,
                                          NPuesto = cargo.NomPuesto,
                                          Nestado = EE.Estado
                                      }).ToList();
                }
                else {
                    ListaEmpleados = (from Emp in db.Empleados
                                      join EE in db.EstadoEmpleado
                                      on Emp.Estado equals
                                      EE.IdStausEmpleado
                                      join cargo in db.Puestos
                                      on Emp.Puesto equals cargo.IdPuesto
                                      where (Emp.Estado == 1 && Emp.Nombre.Contains(Busqueda)) ||
                                      (Emp.Estado == 1 && Emp.Cedula.Contains(Busqueda)) ||
                                      (Emp.Estado == 1 && Emp.Apellidos.Contains(Busqueda)) ||
                                      (Emp.Estado == 1 && Emp.Telefono.Contains(Busqueda))
                                      select new EmpleadosCLS
                                      {
                                          IdEmpleado = Emp.IdEmpleado,
                                          Nombre = Emp.Nombre,
                                          Apellidos = Emp.Apellidos,
                                          Cedula = Emp.Cedula,
                                          Telefono = Emp.Telefono,
                                          Sexo = Emp.Sexo,
                                          Email = Emp.Email,
                                          Estado = Emp.Estado,
                                          Puesto = Emp.Puesto,
                                          NPuesto = cargo.NomPuesto,
                                          Nestado = EE.Estado
                                      }).ToList();
                }


            }

            return PartialView("_TablaEmpleados", ListaEmpleados);
        }

        public string AgregarEmpleados(EmpleadosCLS empcls, int Titulo)
        {
            string respuesta = "";
            try {

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
                    using (var db = new MyonexionEntities())
                    {
                        if (Titulo == -1)
                        {
                            Empleados emp = new Empleados();
                            emp.Nombre = empcls.Nombre;
                            emp.Apellidos = empcls.Apellidos;
                            emp.Cedula = empcls.Cedula;
                            emp.Telefono = empcls.Telefono;
                            emp.Email = empcls.Email;
                            emp.Sexo = empcls.Sexo;
                            emp.Direccion = empcls.Direccion;
                            emp.Puesto = empcls.Puesto;
                            emp.Estado = empcls.Estado;
                            emp.FechaEntrada = empcls.FechaEntrada;
                            emp.FechaActualizacion = emp.FechaEntrada;
                            db.Empleados.Add(emp);
                            respuesta = db.SaveChanges().ToString();
                            if (respuesta == "0") respuesta = "";
                        }
                        if (Titulo >= 0)
                        {
                            Empleados EMPDO = db.Empleados.Where(p => p.IdEmpleado == Titulo).First();
                            EMPDO.Nombre = empcls.Nombre;
                            EMPDO.Apellidos = empcls.Apellidos;
                            EMPDO.Cedula = empcls.Cedula;
                            EMPDO.Telefono = empcls.Telefono;
                            EMPDO.Email = empcls.Email;
                            EMPDO.Sexo = empcls.Sexo;
                            EMPDO.Direccion = empcls.Direccion;
                            EMPDO.Puesto = empcls.Puesto;
                            EMPDO.FechaActualizacion = EMPDO.FechaEntrada;
                            EMPDO.Estado = empcls.Estado;
                            respuesta = db.SaveChanges().ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                respuesta = "";
                /*Se iguala la respuesta a vacion para que esto
                se retorne como un error*/
            }
            return respuesta;
        }

        public JsonResult RecuperarEmpleados(int idempleado) {

            EmpleadosCLS EmpldoCLS = new EmpleadosCLS();
            using (var db = new MyonexionEntities())
            {
                Empleados Empldo = db.Empleados.Where(p => p.IdEmpleado == idempleado).First();
                EmpldoCLS.IdEmpleado = Empldo.IdEmpleado;
                EmpldoCLS.Cedula = Empldo.Cedula;
                EmpldoCLS.Nombre = Empldo.Nombre;
                EmpldoCLS.Apellidos = Empldo.Apellidos;
                EmpldoCLS.Sexo = Empldo.Sexo;
                EmpldoCLS.Telefono = Empldo.Telefono;
                EmpldoCLS.Email = Empldo.Email;
                EmpldoCLS.Direccion = Empldo.Direccion;
                EmpldoCLS.Puesto = Empldo.Puesto;
                EmpldoCLS.FechaEntrada = Empldo.FechaEntrada;
                EmpldoCLS.Estado = Empldo.Estado;
            }

            return Json(EmpldoCLS, JsonRequestBehavior.AllowGet);
        }

        /*Eliminar empleados*/
        public string EliminarEmpleado(EmpleadosCLS EMPCLS)
        {
            string respuesta = "";
            try
            {
                int idempleado = EMPCLS.IdEmpleado;
                using (var db = new MyonexionEntities())
                {
                    Empleados EMP = db.Empleados.Where(p => p.IdEmpleado == idempleado).First();
                    EMP.Estado = 2;
                    respuesta = db.SaveChanges().ToString();
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