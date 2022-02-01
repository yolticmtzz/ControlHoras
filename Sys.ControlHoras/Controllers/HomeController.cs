using Model;
using Repository;
using Sys.Produccion.Attributes;
using Sys.ControlTiempo.Models.Select;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Sys.Produccion.Models;
using Sys.ControlTiempo.Helpers;

namespace Sys.Produccion.Controllers
{
    [Autenticado]
    public class HomeController : BaseApplicationController
    {
        ControlHoraEntities db = new ControlHoraEntities();

        // GET: Home
        public ActionResult Index()
        {
            //if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            //if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            ViewBag.Page = "Index";
            IRepository repository = new Model.Repository();
            var objResult = repository.FindEntity<Empleado>(c => true);
            return View(objResult);
        }

        public ActionResult TraerCalendario()
        {
            //if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            //if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            try
            {
                int idEmpleado = Int32.Parse(Session["Usuario_ID"].ToString());
                List<IngresoHora> list = db.IngresoHora.Where(X => X.Vigente == true && X.ProyectoEmpleado.IdEmpleado == idEmpleado).ToList();

                List<CalendarSelect> listCalendar = list.Select(X => new CalendarSelect
                {
                    id = X.IdIngresoHora,
                    title = string.Format("{0}: {1} horas", X.ProyectoEmpleado.Proyecto.Descripcion, Math.Round(X.Hora,1).ToString().Replace(",",".")),
                    start = X.Fecha.ToString("yyyy-MM-dd"),
                }).ToList();
                return Json(new Response { IsSuccess = true, Result = listCalendar}, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult ReporteGeneral(FormCollection formCollection)
        {
            var esHistorico = (formCollection["Historico"] ?? "").Equals("on", StringComparison.CurrentCultureIgnoreCase);

            var esExcel = (formCollection["Excel"] ?? "").Equals("on", StringComparison.CurrentCultureIgnoreCase);
            if (esHistorico)
            {
                List<Get_EmpleadosConHoras_Result> datos = db.Get_EmpleadosConHoras().ToList();
                if (esExcel)
                {
                    var objExcelResponse = ExcelMaker.GenerarReporteGeneral(datos);
                    return File(objExcelResponse.Ms, objExcelResponse.Tipo, objExcelResponse.NombreArchivo);
                }
                return PdfMaker.GenerarReporteGeneral(datos);
            }
            else
            {
                List<Get_EmpleadosConHoras_Result> datos = db.Get_EmpleadosConHoras().ToList();

                if (!string.IsNullOrEmpty(formCollection["IdEmpleados"]))
                {
                    string[] idsEmpleados = formCollection["IdEmpleados"].Split(new char[] { ',' }).ToArray<string>();

                    datos = datos.Where(X => idsEmpleados.Contains(X.IdEmpleado.ToString())).ToList();

                }

                if (!string.IsNullOrEmpty(formCollection["IdProyectos"]))
                {
                    string[] idsProyectos = formCollection["IdProyectos"].Split(new char[] { ',' }).ToArray<string>();

                    datos = datos.Where(X => idsProyectos.Contains(X.IdProyecto.ToString())).ToList();

                }

                if (!string.IsNullOrEmpty(formCollection["FechaInicial"]) && string.IsNullOrEmpty(formCollection["FechaFinal"]))
                {
                    DateTime fechaInicial = DateTime.Parse(formCollection["FechaInicial"]);

                    datos = datos.Where(X => X.Fecha >= fechaInicial).OrderBy(X => X.Fecha).ToList();


                }

                if (string.IsNullOrEmpty(formCollection["FechaInicial"]) && !string.IsNullOrEmpty(formCollection["FechaFinal"]))
                {
                    DateTime fechaFinal = DateTime.Parse(formCollection["FechaFinal"]);

                    datos = datos.Where(X => X.Fecha <= fechaFinal).OrderBy(X => X.Fecha).ToList();


                }


                if (!string.IsNullOrEmpty(formCollection["FechaInicial"]) && !string.IsNullOrEmpty(formCollection["FechaFinal"]))
                {
                    DateTime fechaInicial = DateTime.Parse(formCollection["FechaInicial"]);
                    DateTime fechaFinal = DateTime.Parse(formCollection["FechaFinal"]);

                    datos = datos.Where(X => X.Fecha >= fechaInicial && X.Fecha <= fechaFinal).OrderBy(X => X.Fecha).ToList();


                }
                if (esExcel)
                {
                    var objExcelResponse = ExcelMaker.GenerarReporteGeneral(datos);
                    return File(objExcelResponse.Ms, objExcelResponse.Tipo, objExcelResponse.NombreArchivo);
                }
                return PdfMaker.GenerarReporteGeneral(datos);
            }
        }

        public ActionResult ReporteUsuario(FormCollection formCollection)
        {
            var esHistorico = (formCollection["Historico"] ?? "").Equals("on", StringComparison.CurrentCultureIgnoreCase);

            var esExcel = (formCollection["Excel"] ?? "").Equals("on", StringComparison.CurrentCultureIgnoreCase);
            if (esHistorico)
            {
                List<Get_EmpleadosConHoras_Result> datos = db.Get_EmpleadosConHoras().Where(X => X.IdEmpleado == Int32.Parse(Session["Usuario_ID"].ToString())).ToList();
                if (esExcel)
                {
                    var objExcelResponse = ExcelMaker.GenerarReporteGeneral(datos);
                    return File(objExcelResponse.Ms, objExcelResponse.Tipo, objExcelResponse.NombreArchivo);
                }
                return PdfMaker.GenerarReporteUsuario(datos);
            }
            else
            {
                List<Get_EmpleadosConHoras_Result> datos = db.Get_EmpleadosConHoras().Where(X => X.IdEmpleado == Int32.Parse(Session["Usuario_ID"].ToString())).ToList();


                if (!string.IsNullOrEmpty(formCollection["IdProyectos"]))
                {
                    string[] idsProyectos = formCollection["IdProyectos"].Split(new char[] { ',' }).ToArray<string>();

                    datos = datos.Where(X => idsProyectos.Contains(X.IdProyecto.ToString())).ToList();
                    
                }


                if (!string.IsNullOrEmpty(formCollection["FechaInicial"]) && string.IsNullOrEmpty(formCollection["FechaFinal"]))
                {
                    DateTime fechaInicial = DateTime.Parse(formCollection["FechaInicial"]);

                    datos = datos.Where(X => X.Fecha >= fechaInicial).OrderBy(X => X.Fecha).ToList();


                }

                if (string.IsNullOrEmpty(formCollection["FechaInicial"]) && !string.IsNullOrEmpty(formCollection["FechaFinal"]))
                {
                    DateTime fechaFinal = DateTime.Parse(formCollection["FechaFinal"]);

                    datos = datos.Where(X => X.Fecha <= fechaFinal).OrderBy(X => X.Fecha).ToList();


                }


                if (!string.IsNullOrEmpty(formCollection["FechaInicial"]) && !string.IsNullOrEmpty(formCollection["FechaFinal"]))
                {
                    DateTime fechaInicial = DateTime.Parse(formCollection["FechaInicial"]);
                    DateTime fechaFinal = DateTime.Parse(formCollection["FechaFinal"]);

                    datos = datos.Where(X => X.Fecha >= fechaInicial && X.Fecha <= fechaFinal).ToList();


                }

                if (esExcel)
                {
                    var objExcelResponse = ExcelMaker.GenerarReporteGeneral(datos);
                    return File(objExcelResponse.Ms, objExcelResponse.Tipo, objExcelResponse.NombreArchivo);
                }

                return PdfMaker.GenerarReporteUsuario(datos);
            }

        }

    }
}