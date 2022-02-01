using Model;
using Repository;
using Sys.ControlTiempo.Helpers;
using Sys.Produccion.Helpers;
using Sys.Produccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.Produccion.Controllers
{
    public class AccountController : BaseApplicationController
    {
        readonly private ControlHoraEntities db = new ControlHoraEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo))
            {
                TempData["Error"] = "Ingrese correo por favor";
                return View();
            }
            if (string.IsNullOrEmpty(clave))
            {
                TempData["Error"] = "Ingrese clave por favor";
                return View();
            }

            var empleado = db.Empleado.Where(X => X.Correo.ToUpper() == correo.ToUpper() && X.Vigente == true).FirstOrDefault();


            //string strMensaje = "El usuario y/o contraseña son incorrectos.";
            //recordar = recordar == null ? false : true;
            if (empleado != null)
            {
                if (CryproHelper.Confirm(clave, empleado.Clave, CryproHelper.Supported_HA.SHA512))
                {
                    //id = -1;
                    if (empleado.UltimoLogin != null)
                    {
                        empleado.UltimoLogin = DateTime.Now;
                        db.SaveChanges();
                        SessionHelper.AddUserToSession(empleado.IdEmpleado.ToString(), (bool)false);
                        SessionHelper.ActualizarSession(empleado);
                        GenerarBitacora("Inicia sesión");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["Usuario_ID"] = empleado.IdEmpleado;
                        return RedirectToAction("ReIngresar");
                    }

                }
            }
            TempData["Error"] = "El usuario y/o contraseña son incorrectos.";
            return View();
        }

        public ActionResult ReIngresar()
        {
            if (Session["Usuario_OK"] == null && Session["Usuario_ID"] != null)
            {
                return View();
            }
            else
            {
                if (Session["Usuario_OK"] != null)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public ActionResult ReIngresar(string antiguaClave, string nuevaClave, string repetirNuevaClave)
        {
            int idEEmpleado = Int32.Parse(Session["Usuario_ID"].ToString());
            var obj = db.Empleado.Where(X => X.Vigente == true && X.IdEmpleado == idEEmpleado).FirstOrDefault();
            if (!CryproHelper.Confirm(antiguaClave, obj.Clave, CryproHelper.Supported_HA.SHA512))
            {
                return Json(new Response { IsSuccess = false, Message = "Las clave antigua es incorrecta" }, JsonRequestBehavior.AllowGet);
            }
            if (nuevaClave != repetirNuevaClave)
            {
                return Json(new Response { IsSuccess = false, Message = "Las claves deben ser iguales" }, JsonRequestBehavior.AllowGet);
            }
            var esCorrecto = ToolsHelper.SendMail(obj.Correo, string.Format("Cambio de contraseña: {0}", obj.Nombre), string.Format("<h1 aling=center>{0}</h1><br> <h4>Contraseña: {1}</h4>", obj.Nombre, nuevaClave));
            if (esCorrecto)
            {
                obj.Clave = CryproHelper.ComputeHash(nuevaClave, CryproHelper.Supported_HA.SHA512, null);
                obj.UltimoLogin = DateTime.Now;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        Session["Usuario_ID"] = null;
                        return Json(new Response { IsSuccess = true, Message = "La contraseña ha sido cambiada satisfactoriamente, se ha enviado un aviso a su correo." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No se han registrado cambios, contacte con el Administrador enviando la información del problema." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new Response { IsSuccess = false, Message = "No se ha podido cambiar la contraseña, el envio de correo ha fallado." }, JsonRequestBehavior.AllowGet);



        }

        public ActionResult ResetearClave()
        {
            if (Session["Usuario_OK"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult ResetearClave(string antiguaClave, string nuevaClave, string repetirNuevaClave)
        {
            int idEEmpleado = Int32.Parse(Session["Usuario_ID"].ToString());
            var obj = db.Empleado.Where(X => X.Vigente == true && X.IdEmpleado == idEEmpleado).FirstOrDefault();
            if (!CryproHelper.Confirm(antiguaClave, obj.Clave, CryproHelper.Supported_HA.SHA512))
            {
                return Json(new Response { IsSuccess = false, Message = "Las clave antigua es incorrecta" }, JsonRequestBehavior.AllowGet);
            }
            if (nuevaClave != repetirNuevaClave)
            {
                return Json(new Response { IsSuccess = false, Message = "Las claves deben ser iguales" }, JsonRequestBehavior.AllowGet);
            }
            var esCorrecto = ToolsHelper.SendMail(obj.Correo, string.Format("Cambio de contraseña: {0}", obj.Nombre), string.Format("<h1 aling=center>{0}</h1><br> <h4>Contraseña: {1}</h4>", obj.Nombre, nuevaClave));
            if (esCorrecto)
            {
                obj.Clave = CryproHelper.ComputeHash(nuevaClave, CryproHelper.Supported_HA.SHA512, null);
                obj.UltimoLogin = DateTime.Now;
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        Session["Usuario_OK"] = null;
                        Session["Usuario_ID"] = null;
                        Session["Usuario_PERFIL"] = null;
                        Session["Usuario_NOMBRE"] = null;
                        Session["Usuario_PERFIL_NOMBRE"] = null;
                        Session["Usuario_PERFIL"] = null;
                        SessionHelper.DestroyUserSession();
                        return Json(new Response { IsSuccess = true, Message = "La contraseña ha sido cambiada satisfactoriamente, se ha enviado un aviso a su correo." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new Response { IsSuccess = false, Message = "No se han registrado cambios, contacte con el Administrador enviando la información del problema." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new Response { IsSuccess = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new Response { IsSuccess = false, Message = "No se ha podido cambiar la contraseña, el envio de correo ha fallado." }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CerrarSesion()
        {
            Session["Usuario_OK"] = null;
            Session["Usuario_ID"] = null;
            Session["Usuario_PERFIL"] = null;
            Session["Usuario_NOMBRE"] = null;
            Session["Usuario_PERFIL_NOMBRE"] = null;
            Session["Usuario_PERFIL"] = null;
            SessionHelper.DestroyUserSession();
            return RedirectToAction("Index");
        }

        //public ActionResult Registrarme()
        //{
        //    return View();
        //}

        //public ActionResult Activar(string Token)
        //{
        //    if(!string.IsNullOrEmpty(Token))
        //    {
        //        IRepository repository = new Model.Repository();
        //        var objUsu = repository.FindEntity<Usuarios>(c => c.Token == Token);
        //        if (objUsu != null)
        //        {
        //            ViewBag.Mensaje = "Cuenta Activada";
        //            objUsu.Token = "";
        //            objUsu.Activo = true;
        //            repository.Update(objUsu);
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "La cuenta no se pudo activar";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "La cuenta no se pudo activar";
        //    }

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Registrarme(string NombreEmpresa, string CorreoElectronico, string Password)
        //{
        //    IRepository repository = new Model.Repository();
        //    var objUsu = repository.FindEntity<Usuarios>(c => c.CorreoElectronico == CorreoElectronico);
        //    string strMensaje = "";
        //    int id = 0;
        //    if (objUsu != null)
        //    {
        //        strMensaje = "El usuario ya existe en nuestra base de datos, intente recuperar su cuenta para cambiar su contraseña.";
        //    }
        //    else
        //    {
        //        string strPass = CryproHelper.ComputeHash(Password, CryproHelper.Supported_HA.SHA512, null);
        //        var objEmpresa = repository.Create(new Empresas
        //        {
        //            CorreoElectronico = CorreoElectronico,
        //            Direccion = "",
        //            Logo = "",
        //            Moneda = "MX",
        //            NombreEmpresa = NombreEmpresa,
        //            Telefono = "",
        //            Tipo_Id = 2,
        //            ZonaHoraria_Id = null
        //        });
        //        if (objEmpresa != null)
        //        {
        //            string token = Guid.NewGuid().ToString();
        //            var objUsuNew = repository.Create(new Usuarios
        //            {
        //                Activo = false,
        //                CorreoElectronico = CorreoElectronico,
        //                EmpresaId = objEmpresa.Id,
        //                Fecha = DateTime.Now,
        //                Nombres = "",
        //                Password = strPass,
        //                Rol_Id = 1,
        //                Telefono = "",
        //                Token=token
        //            });
        //            if (objUsuNew != null)
        //            {
        //                var baseAddress = new Uri(ToolsHelper.UrlOriginal(Request));
        //                string Mensaje = "Gracias por inscribirte al sistema de Produccion, para entrar con el usuario " +
        //                    "y contraseña registrada debes revisar tu correo y activar la cuenta. <a href='" + baseAddress + "/Account/Activar?Token=" + token  + "'>Produccion</a>";
        //                ToolsHelper.SendMail(CorreoElectronico, "Gracias por registrarte a Produccion", Mensaje);
        //                strMensaje = "Te registraste correctamente, ya puedes entrar al sistema.";
        //                id = objUsuNew.Id;
        //            }
        //            else
        //            {
        //                strMensaje = "Disculpe las molestias, por el momento no podemos conectarnos con el servidor, intentelo nuevamente.";
        //            }
        //        }               
        //        else
        //        {
        //            strMensaje = "Disculpe las molestias, por el momento no podemos conectarnos con el servidor, intentelo nuevamente.";
        //        }
        //    }
        //    return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult RecuperarCuenta()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult RecuperarCuenta(string CorreoElectronico)
        //{
        //    IRepository repository = new Model.Repository();
        //    var objUsu = repository.FindEntity<Usuarios>(c => c.CorreoElectronico == CorreoElectronico);
        //    int id = 0;
        //    string strMensaje = "El correo no se encuentra registrado.";
        //    if (objUsu != null)
        //    {
        //        objUsu.Activo = true;
        //        string strToken = objUsu.Id.ToString() + objUsu.CorreoElectronico;
        //        string strTknAjax = CryproHelper.ComputeHash(strToken, CryproHelper.Supported_HA.SHA512, null);
        //        objUsu.Token = Server.UrlEncode(strTknAjax);
        //        repository.Update(objUsu);
        //        var baseAddress = ToolsHelper.UrlOriginal(Request) + "/Account/ResetPass/?tkn=" + objUsu.Token;
        //        string Mensaje = "Para restaurar tu cuenta de Produccion, entra a la siguiente liga y crea una nueva contraseña. <br/><br/> <a href='" + baseAddress + "'>Produccion recuperar cuenta</a>";
        //        ToolsHelper.SendMail(CorreoElectronico, "Recuperar cuenta de Produccion", Mensaje);
        //        strMensaje = "Se envío un correo con la información requerida para recuperar su cuenta.";
        //    }
        //    return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult ResetPass(string tkn)
        //{
        //    if (!string.IsNullOrEmpty(tkn))
        //    {
        //        IRepository repository = new Model.Repository();
        //        tkn = Server.UrlEncode(tkn);
        //        ViewBag.tkn = tkn;
        //        var objUsu = repository.FindEntity<Usuarios>(c => c.Token == tkn);
        //        if (objUsu != null)
        //        {
        //            return View();
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}



        //[HttpPost]
        //public ActionResult ResetPass(string Password, string tkn)
        //{
        //    IRepository repository = new Model.Repository();
        //    var objUsu = repository.FindEntity<Usuarios>(c => c.Token == tkn);
        //    string strMensaje = "";
        //    int id = 0;
        //    if (objUsu != null)
        //    {
        //        string strPass = CryproHelper.ComputeHash(Password, CryproHelper.Supported_HA.SHA512, null);
        //        objUsu.Password = strPass;
        //        objUsu.Token = "";
        //        repository.Update(objUsu);
        //        strMensaje = "Se actualizó la contraseña correctamente, ya puede entrar al sistema Produccion.";
        //    }
        //    else
        //    {
        //        strMensaje = "El token se encuentra vencido, necesita recuperar nuevamente su cuenta.";
        //    }
        //    return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        //}

    }
}