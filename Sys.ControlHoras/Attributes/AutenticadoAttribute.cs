using Model;
using Repository;
using Sys.Produccion.Helpers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sys.Produccion.Attributes
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ControlHoraEntities db = new ControlHoraEntities();
            if (!SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Index"
                }));
            }
            else
            {
                IRepository Repositorio = new Model.Repository();
                int idUser = SessionHelper.GetUser();
                var Usuario = db.Empleado.Where(X => X.IdEmpleado == idUser && X.Vigente == true).FirstOrDefault(); //Repositorio.FindEntity<Empleado>(c => c.IdEmpleado == idUser);
                if (Usuario != null)
                {
                    SessionHelper.ActualizarSession(Usuario);
                }
            }
        }
    }

    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                IRepository Repositorio = new Model.Repository();
                int idUser = SessionHelper.GetUser();
                var Usuario = Repositorio.FindEntity<Empleado>(c => c.IdEmpleado == idUser && c.Vigente==true);
                if (Usuario != null)
                {
                    SessionHelper.ActualizarSession(Usuario);
                    if (Usuario.IdPerfil == 1)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Admin",
                            action = "Index"
                        }));
                    }
                }
                                
            }
        }
    }

}