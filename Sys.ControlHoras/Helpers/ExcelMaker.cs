using Model;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using Sys.ControlTiempo.Clases;

namespace Sys.ControlTiempo.Helpers
{
    public class ExcelMaker
    {
        public static ExcelResponse GenerarReporteGeneral(List<Get_EmpleadosConHoras_Result> datos)
        {
            #region Configuración general del documento

            MemoryStream ms = new MemoryStream();
            string nombre_archivo = "ReporteGeneral_" + DateTime.Today.ToString("d") + ".xlsx";

            SLDocument sl = new SLDocument();
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Por pagar");

            #endregion

            #region Configuración de celdas y cabeceras

            sl.SetCellValue("A1", "Empleado");
            sl.SetCellValue("B1", "Proyecto");
            sl.SetCellValue("C1", "Fecha");
            sl.SetCellValue("D1", "Hora");

            //Espacios columnas
            sl.SetColumnWidth("A1", 50);
            sl.SetColumnWidth("B1", 40);
            sl.SetColumnWidth("C1", 20);
            sl.SetColumnWidth("D1", 20);

            #endregion

            #region Ingreso de datos

            int posicionExcel = 2;

            foreach (var dato in datos)
            {
                sl.SetCellValue("A" + posicionExcel, dato.Nombre);
                sl.SetCellValue("B" + posicionExcel, dato.Descripcion);
                sl.SetCellValue("C" + posicionExcel, dato.Fecha.ToString("dd-MM-yyyy"));
                sl.SetCellValue("D" + posicionExcel, Math.Round(dato.Hora, 1).ToString().Replace(",", "."));

                posicionExcel++;
            }
            sl.SaveAs(ms);
            ms.Position = 0;

            #endregion

            return new ExcelResponse
            {
                Ms = ms,
                Tipo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                NombreArchivo = nombre_archivo
            };

        }

        public static ExcelResponse GenerarReporteUsuario(List<Get_EmpleadosConHoras_Result> datos)
        {
            #region Configuración general del documento

            MemoryStream ms = new MemoryStream();
            string nombre_archivo = "ReporteUsuario_" + DateTime.Today.ToString("d") + ".xlsx";

            SLDocument sl = new SLDocument();
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Por pagar");

            #endregion

            #region Configuración de celdas y cabeceras

            sl.SetCellValue("A1", "Proyecto");
            sl.SetCellValue("B1", "Fecha");
            sl.SetCellValue("C1", "Hora");

            //Espacios columnas
            sl.SetColumnWidth("B1", 40);
            sl.SetColumnWidth("C1", 20);
            sl.SetColumnWidth("D1", 20);

            #endregion

            #region Ingreso de datos

            int posicionExcel = 2;

            foreach (var dato in datos)
            {
                sl.SetCellValue("A" + posicionExcel, dato.Descripcion);
                sl.SetCellValue("B" + posicionExcel, dato.Fecha.ToString("dd-MM-yyyy"));
                sl.SetCellValue("C" + posicionExcel, Math.Round(dato.Hora, 1).ToString().Replace(",", "."));

                posicionExcel++;
            }
            sl.SaveAs(ms);
            ms.Position = 0;

            #endregion

            return new ExcelResponse
            {
                Ms = ms,
                Tipo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                NombreArchivo = nombre_archivo
            };
        }
    }
}