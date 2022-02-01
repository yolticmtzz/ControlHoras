using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sys.ControlTiempo.Models
{
    public partial class MenuSelect
    {
        public static ControlHoraEntities db = new ControlHoraEntities();
        public string Nombre { get; set; }
        public string Archivo { get; set; }
        public string Icono { get; set; }



        public static List<MenuSelect> GetMenu(int idMenu, int idPerfil)
        {
            return (from Privilegio in db.Privilegio
                    join PerfilPrivilegio in db.PerfilPrivilegio on Privilegio.IdPrivilegio equals PerfilPrivilegio.IdPrivilegio
                    where PerfilPrivilegio.IdPerfil == idPerfil && Privilegio.IdMenu == idMenu
                    select new MenuSelect{ Nombre = Privilegio.Descripcion, Archivo = Privilegio.Archivo, Icono = Privilegio.Icono }).ToList();
        }

    }
}