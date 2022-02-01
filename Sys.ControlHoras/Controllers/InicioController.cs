using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
namespace Sys.ControlTiempo.Controllers
{
    public class InicioController : Controller
    {
        ControlHoraEntities db = new ControlHoraEntities();
        // GET: Inicio
        public ActionResult Index()
        {
            
            return View();
        }
    }
}