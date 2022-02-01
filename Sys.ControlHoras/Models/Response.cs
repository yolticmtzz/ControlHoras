using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sys.Produccion.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public int Id { get; set; }




        #region Posicion
        public string Medida { get; set; }
        public string TipoMedida { get; set; }
        #endregion



    }
}