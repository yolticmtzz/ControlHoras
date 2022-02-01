using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Models
{
    public class ProyectosHelper
    {
        #region DataContext
        readonly private static ControlHoraEntities dc = new ControlHoraEntities();
        #endregion

        #region SelectList


        #endregion
        public static List<SelectListItem> GetAnios()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            for (int I = DateTime.Now.Year - 1; I < (DateTime.Now.Year + ((DateTime.Now.Year - DateTime.Now.Year) + 3)); I++)
            {
                selectList.Add(new SelectListItem
                {
                    Text = I.ToString(),
                    Value = I.ToString()
                });
            }

            
            return selectList;
        }

        public static IEnumerable<SelectListItem> GetClientes()
        {
            IEnumerable<SelectListItem> selectList = from C in dc.Cliente
                                                     where C.Vigente == true
                                                     orderby C.Nombre
                                                     select new SelectListItem
                                                     {
                                                         Text = C.Nombre,
                                                         Value = C.IdCliente.ToString()
                                                     };
            return selectList;
        }


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