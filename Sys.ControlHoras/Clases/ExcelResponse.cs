using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sys.ControlTiempo.Clases
{
    public class ExcelResponse
    {
        public string NombreArchivo { get; set; }
        public string Tipo { get; set; }

        public MemoryStream Ms { get; set; }
    }
}