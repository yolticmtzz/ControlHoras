using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sys.ControlTiempo.Models.Select
{
    public class ProyectoSelect
    {
        public int IdProyecto { get; set; }
        public int IdCliente { get; set; }
        public string Descripcion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public short HorasAsignada { get; set; }
    }
}