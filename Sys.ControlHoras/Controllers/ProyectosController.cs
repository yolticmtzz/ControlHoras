using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.Web.Mvc;
using Sys.Produccion.Models;
using Sys.Produccion.Helpers;
using EFRepository;
using Repository;
using Newtonsoft.Json;
using Sys.ControlTiempo.Models.Select;
using Sys.ControlTiempo.Helpers;

namespace Sys.ControlTiempo.Controllers
{
    public class ProyectosController : BaseApplicationController
    {
        ControlHoraEntities db = new ControlHoraEntities();
        // GET: Proyectos
        public ActionResult Index()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            GenerarBitacora(string.Format("Ingresaa Proyectos"));
            return View();
        }
        public ActionResult Agregar(Proyecto proyecto)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            proyecto.Descripcion = proyecto.Descripcion.ToUpper();
            proyecto.Estado = 1;
            proyecto.Vigente = true;
            db.Proyecto.Add(proyecto);
            try
            {
                if (db.SaveChanges() > 0)
                {
                    GenerarBitacora("Crea proyecto " + proyecto.Descripcion );
                    return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido creado satisfactoriamente", proyecto.Descripcion) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new Response { IsSuccess = false, Message = string.Format(e.Message) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new Response { IsSuccess = false, Message = "No se pudo agregar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(Proyecto proyecto)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var objEdit = db.Proyecto.Where(X => X.IdProyecto == proyecto.IdProyecto && X.Vigente == true).First();
            if (objEdit != null)
            {
                objEdit.Descripcion = proyecto.Descripcion.ToUpper();
                objEdit.FechaFinal = proyecto.FechaFinal;
                objEdit.FechaInicio = proyecto.FechaInicio;
                objEdit.IdCliente = proyecto.IdCliente;
                objEdit.HorasAsignada = proyecto.HorasAsignada;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora("Edita proyecto N° " + proyecto.IdProyecto);
                        return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido editado satisfactoriamente", objEdit.Descripcion) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No existen cambios, no se ha editado el registro" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = string.Format(e.Message) }, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(new Response { IsSuccess = false, Message = "No se pudo editar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Activar(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var objEdit = db.Proyecto.Where(X => X.IdProyecto == Id).FirstOrDefault();
            if (objEdit != null)
            {
                objEdit.Estado = 1;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora("Activa proyecto N° " + objEdit.IdProyecto);
                        return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido activado", objEdit.Descripcion) }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo actualizar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Finalizar(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var objEdit = db.Proyecto.Where(X => X.IdProyecto == Id).FirstOrDefault();
            if (objEdit != null)
            {
                objEdit.Estado = 0;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora("Finbaliza proyecto N° " + objEdit.IdProyecto);

                        return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido finalizado", objEdit.Descripcion) }, JsonRequestBehavior.AllowGet);
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
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var objEdit = db.Proyecto.Where(X => X.IdProyecto == Id).FirstOrDefault();
            if (objEdit != null)
            {
                objEdit.Vigente = false;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora("Elimina proyecto N° " + objEdit.IdProyecto);

                        return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido eliminado", objEdit.Descripcion) }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo actualizar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TraerTodo()
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var list = db.Proyecto.Where(X => X.Vigente == true).ToList(); ;
            return View(list);
        }


        public ActionResult Traer(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            IRepository repository = new Model.Repository();
            var proyecto = (from c in db.Proyecto where c.IdProyecto == Id && c.Vigente == true select c).FirstOrDefault();




            if (proyecto != null)
            {
                var obj = new ProyectoSelect
                {
                    IdProyecto = proyecto.IdProyecto,
                    Descripcion = proyecto.Descripcion,
                    FechaFinal = proyecto.FechaFinal.Value.ToString("yyyy/MM/dd"),
                    FechaInicio = proyecto.FechaInicio.ToString("yyyy/MM/dd"),
                    HorasAsignada = proyecto.HorasAsignada.Value,
                    IdCliente = proyecto.IdCliente
                };
                return Json(new Response { IsSuccess = true, Result = obj }, JsonRequestBehavior.AllowGet);
            }

            return Json(new Response { IsSuccess = false, Message = "No se a podido traer el proyecto" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TraerDetalles(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var list = db.ProyectoEmpleado.Where(X => X.Vigente == true && X.IdProyecto == Id).ToList();
            return View(list);
        }

        public ActionResult AgregarDetalle(ProyectoEmpleado proyectoEmpleado)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            proyectoEmpleado.Vigente = true;

            db.ProyectoEmpleado.Add(proyectoEmpleado);
            try
            {
                if (db.SaveChanges() > 0)
                {
                    GenerarBitacora(string.Format("{0} ha sido agregado al proyecto {1}", proyectoEmpleado.IdEmpleado, proyectoEmpleado.IdProyecto));

                    return Json(new Response { IsSuccess = true, Message = string.Format("El empleado se ha asignado al proyecto correctamente"), Id = proyectoEmpleado.IdProyecto }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);

            }

            return Json(new Response { IsSuccess = false, Message = "No se pudo agregar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EliminarDetalle(int Id)
        {
            if (!ValidarSesion()) return RedirectToAction("Index", "Account");
            if (!ValidarPermiso(1)) return RedirectToAction("Index", "Home");
            var objEdit = db.ProyectoEmpleado.Where(X => X.IdProyectoEmpleado == Id).FirstOrDefault();
            if (objEdit != null)
            {
                objEdit.Vigente = false;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        GenerarBitacora(string.Format("{0} ha sido eliminado del proyecto {1}", objEdit.Empleado.Nombre, objEdit.Proyecto.Descripcion));
                        return Json(new Response { IsSuccess = true, Message = string.Format("{0} ha sido eliminado del proyecto", objEdit.Empleado.Nombre), Id = objEdit.IdProyecto }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new Response { IsSuccess = false, Message = "No se pudo actualizar la información, intentelo más tarde" }, JsonRequestBehavior.AllowGet);
        }





    }
}