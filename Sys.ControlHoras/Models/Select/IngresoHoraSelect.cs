using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sys.ControlTiempo.Models.Select
{
    public class IngresoHoraSelect
    {
        public int IdIngresoHora { get; set; }
        public int IdProyectoEmpleado { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

    }
}