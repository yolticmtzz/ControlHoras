using Sys.ControlTiempo.Models.SelectList;
using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Models
{
    public class EmpleadoHelper
    {
        #region DataContext
        readonly private static ControlHoraEntities dc = new ControlHoraEntities();
        #endregion

        #region SelectList


        #endregion
        public static IEnumerable<SelectListItem> GetEmpleados()
        {
            IEnumerable<SelectListItem> selectList = from C in dc.Empleado
                                                     where C.Vigente == true
                                                     orderby C.Nombre
                                                     select new SelectListItem
                                                     {
                                                         Text = C.Nombre,
                                                         Value = C.IdEmpleado.ToString()
                                                     };
            return selectList;
        }
    }
}