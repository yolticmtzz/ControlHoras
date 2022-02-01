using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Helpers
{
    public class BaseApplicationController : Controller
    {
        private ControlHoraEntities db = new ControlHoraEntities();

        public bool ValidarPermiso(int idPerfil)
        {
            int sessionPerfil = Int32.Parse(Session["Usuario_PERFIL"].ToString());
            if (idPerfil != sessionPerfil)
            {
                return false;
            }
            else
            {
                return true;

            }

        }
        public string RemoteAddr()
        {
            return HttpContext.Request.ServerVariables["REMOTE_ADDR"];
        }

        public void GenerarBitacora(string texto)
        {
            try
            {
                string ipAddress = RemoteAddr();
                int idEmpleado = Int32.Parse(Session["Usuario_ID"].ToString());
                Bitacora bitacora = new Bitacora
                {
                    IdEmpleado = idEmpleado,
                    Fecha = DateTime.Now,
                    IP = ipAddress,
                    Bitacora1 = texto
                };

                db.Bitacora.Add(bitacora);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        public bool ValidarSesion()
        {
            if (Session["Usuario_OK"] == null)
            {
                return false;
            }
            return true;
        }

        public void RedirectToFirstMenu()
        {
            Response.Redirect("~/Account/Index");
        }

    }
}