using Model;
using Sys.ControlTiempo.Models.Select;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Models
{
    public class Horas
    {
        #region DataContext
        readonly private static ControlHoraEntities dc = new ControlHoraEntities();
        #endregion

        #region SelectList


        #endregion
        public static IEnumerable<SelectListItem> GetProyectos()
        {
            int idEmpleado = Int32.Parse(HttpContext.Current.Session["Usuario_ID"].ToString());
            IEnumerable<SelectListItem> selectList = from C in dc.ProyectoEmpleado
                                                     where C.Vigente == true
                                                     && C.IdEmpleado == idEmpleado
                                                     && C.Proyecto.Vigente == true 
                                                     && C.Proyecto.Estado == 1
                                                     orderby C.Proyecto.Descripcion
                                                     select new SelectListItem
                                                     {
                                                         Text = C.Proyecto.Descripcion,
                                                         Value = C.IdProyectoEmpleado.ToString()
                                                     };
            return selectList;
        }

        public static IEnumerable<SelectListItem> GetProyectosGenerales()
        {
            IEnumerable<SelectListItem> selectList = from C in dc.Proyecto
                                                     where C.Vigente == true
                                                     && C.Vigente == true
                                                     orderby C.Descripcion
                                                     select new SelectListItem
                                                     {
                                                         Text = C.Descripcion,
                                                         Value = C.IdProyecto.ToString()
                                                     };
            return selectList;
        }

        public static IEnumerable<SelectListItem> GetProyectosGeneralesPorEmpleado()
        {
            int idEmpleado = Int32.Parse(HttpContext.Current.Session["Usuario_ID"].ToString());
            IEnumerable<SelectListItem> selectList = from C in dc.ProyectoEmpleado
                                                     where C.Vigente == true
                                                     && C.IdEmpleado == idEmpleado
                                                     && C.Proyecto.Vigente == true
                                                     && C.Vigente == true
                                                     orderby C.Proyecto.Descripcion
                                                     select new SelectListItem
                                                     {
                                                         Text = C.Proyecto.Descripcion,
                                                         Value = C.IdProyecto.ToString()
                                                     };
            return selectList;
        }


        public static MesesSelect GetMeses()
        {
            var mesMenor = dc.MesProceso.Where(X => X.Estado == 1).Min(X => X.AnioMes);
            var mesMayor = dc.MesProceso.Where(X => X.Estado == 1).Max(X => X.AnioMes);

            string mesMenorParse = string.Format("{0}-{1}-01", mesMenor.Substring(0, 4), mesMenor.Substring(4, 2));

            string mesMayorParse = string.Format("{0}/{1}/01", mesMayor.Substring(0, 4), mesMayor.Substring(4, 2));
            DateTime date = Convert.ToDateTime(mesMayorParse);
            DateTime newDate = date.AddMonths(1).AddDays(-1);

            return new MesesSelect
            {
                MesMenor = mesMenorParse,
                MesMayor = newDate.ToString("yyyy-MM-dd").Replace("/","-")
            };


        }

    }
}