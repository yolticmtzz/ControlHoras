using Model;
using Repository;
using Sys.ControlTiempo.Helpers;
using Sys.ControlTiempo.Models.Select;
using Sys.Produccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Controllers
{
    public class IngresoHorasController : BaseApplicationController
    {
        ControlHoraEntities db = new ControlHoraEntities();
        // GET: IngresoHoras
        public ActionResult Index()
        {
            GenerarBitacora("Entra a IngresoHoras");
            return View();
        }

        public ActionResult Agregar(IngresoHora ingresoHora)
        {
            ingresoHora.Vigente = true;
            db.IngresoHora.Add(ingresoHora);
            try
            {
                if (db.SaveChanges() > 0)
                {
                    GenerarBitacora(string.Format("Ha ingresado {0} horas para el dia {1} referenciado a la tabla ProyectoEmpleado con id = {2}", ingresoHora.Hora , ingresoHora.Fecha.ToString("yyyy/MM/dd"), ingresoHora.IdProyectoEmpleado));

                    return Json(new Response { IsSuccess = true, Message = "Se ha asignado la hora correctamente" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new Response { IsSuccess = false, Message = "No se pudo agregar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(IngresoHora ingresoHora)
        {
            var objEdit = db.IngresoHora.Where(X => X.IdIngresoHora == ingresoHora.IdIngresoHora && X.Vigente == true).FirstOrDefault();
            if (objEdit != null)
            {
                objEdit.IdProyectoEmpleado = ingresoHora.IdProyectoEmpleado;
                objEdit.Hora = ingresoHora.Hora;
                objEdit.Fecha = ingresoHora.Fecha;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora(string.Format("Ha editado ingresoHora N° {0}, datos: Horas->{1} - Fecha->{2} - ProyectoEmpleado: {3}", objEdit.IdIngresoHora, ingresoHora.Hora, ingresoHora.Fecha.ToString("yyyy/MM/dd"), ingresoHora.IdProyectoEmpleado));
                        return Json(new Response { IsSuccess = true, Message = string.Format("La hora ha sido editada correctamente") }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No existen cambios, no se ha editado el registro" }, JsonRequestBehavior.AllowGet);

                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo actualizar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Eliminar(int Id)
        {

            var obj = db.IngresoHora.Where(X => X.Vigente == true && X.IdIngresoHora == Id).First();
            if (obj != null)
            {
                obj.Vigente = false;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora(string.Format("Ha eliminado IngresoHora N° {0}", Id));
                        return Json(new Response { IsSuccess = true, Message = "El registro se ha eliminado correctamente" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No existen cambios, no se ha eliminado el registro" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo eliminar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TraerTodo()
        {
            int idEmpleado = Int32.Parse(Session["Usuario_ID"].ToString());
            var list = db.IngresoHora.Where(X => X.Vigente == true && X.ProyectoEmpleado.IdEmpleado == idEmpleado).ToList();
            return View(list);
        }

        public ActionResult Traer(int Id)
        {
            var ingresohora = (from c in db.IngresoHora where c.IdIngresoHora == Id && c.Vigente == true select c).FirstOrDefault();




            if (ingresohora != null)
            {
                var obj = new IngresoHoraSelect
                {
                    IdIngresoHora = ingresohora.IdIngresoHora,
                    IdProyectoEmpleado = ingresohora.IdProyectoEmpleado,
                    Hora = Math.Round(ingresohora.Hora, 1).ToString().Replace(',','.'),
                    Fecha = ingresohora.Fecha.ToString("yyyy/MM/dd"),

                };
                return Json(new Response { IsSuccess = true, Result = obj }, JsonRequestBehavior.AllowGet);
            }

            return Json(new Response { IsSuccess = false, Message = "No se a podido traer el proyecto" }, JsonRequestBehavior.AllowGet);
        }

    }
}