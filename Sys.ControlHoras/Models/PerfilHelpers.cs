using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Models
{
    public class PerfilHelpers
    {
        #region DataContext
        readonly private static ControlHoraEntities dc = new ControlHoraEntities();
        #endregion

        #region SelectList


        #endregion
        public static IEnumerable<SelectListItem> GetPerfiles()
        {
            IEnumerable<SelectListItem> selectList = from C in dc.Perfil
                                                     where C.Vigente == true
                                                     orderby C.Descripcion
                                                     select new SelectListItem
                                                     {
                                                         Text = C.Descripcion,
                                                         Value = C.IdPerfil.ToString()
                                                     };
            return selectList;
        }
    }
}