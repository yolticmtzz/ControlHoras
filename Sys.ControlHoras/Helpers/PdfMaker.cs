using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.ControlTiempo.Helpers
{
    public class PdfMaker
    {
        private static ControlHoraEntities db = new ControlHoraEntities();
        public static FileStreamResult GenerarReporteGeneral(List<Get_EmpleadosConHoras_Result> datos)
        {


            #region Declaración de documento y opciones generales

            Document doc = new Document();

            doc.SetPageSize(PageSize.NOTE);

            doc.SetMargins(9f, 9f, 9f, 9f);

            MemoryStream memStream = new MemoryStream();



            PdfWriter wri = PdfWriter.GetInstance(doc, memStream);


            doc.AddCreationDate();



            doc.AddTitle(DateTime.Today.ToString("d") + " Listado de horas general");

            doc.Open();

            #endregion

            #region Fuentes

            Font fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLACK);
            Font th = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.BLACK);
            Font td = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLACK);


            #endregion

            #region Cabecera


            Paragraph puchOrder = new Paragraph(" REPORTE GENERAL HORAS INGRESADAS SOINDUS ")
            {
                Alignment = Element.ALIGN_CENTER
            };

            doc.Add(puchOrder);

            #endregion

            #region Cuerpo

            doc.Add(new Chunk("\n"));

            var table = new PdfPTable(new float[] { 45f, 33f, 15f, 7f }) { WidthPercentage = 100 };

            // Esta es la primera fila

            table.AddCell(new PdfPCell(new Phrase("Empleado", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
            table.AddCell(new PdfPCell(new Phrase("Proyecto", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
            table.AddCell(new PdfPCell(new Phrase("Fecha", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
            table.AddCell(new PdfPCell(new Phrase("Hora", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });



            foreach (var item in datos)
            {

                table.AddCell(new PdfPCell(new Phrase(item.Nombre, td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                table.AddCell(new PdfPCell(new Phrase(item.Descripcion, td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                table.AddCell(new PdfPCell(new Phrase(item.Fecha.ToString("dd-MM-yyyy"), td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                table.AddCell(new PdfPCell(new Phrase(Math.Round(item.Hora, 1).ToString().Replace(",", "."), td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                doc.Add(table);
                table = new PdfPTable(new float[] { 45f, 33f, 15f, 7f }) { WidthPercentage = 100 };


            }

            // Agregamos la tabla al documento
            // Ceramos el documento

            #endregion





            doc.Close();

            byte[] byteStream = memStream.ToArray();

            memStream = new MemoryStream();

            memStream.Write(byteStream, 0, byteStream.Length);

            memStream.Position = 0;

            return new FileStreamResult(memStream, "application/pdf");


        }

        public static FileStreamResult GenerarReporteUsuario(List<Get_EmpleadosConHoras_Result> datos)
        {


            #region Declaración de documento y opciones generales

            Document doc = new Document();

            doc.SetPageSize(PageSize.NOTE);

            doc.SetMargins(9f, 9f, 9f, 9f);

            MemoryStream memStream = new MemoryStream();



            PdfWriter wri = PdfWriter.GetInstance(doc, memStream);


            doc.AddCreationDate();



            doc.AddTitle(DateTime.Today.ToString("d") + " Listado de horas general");

            doc.Open();

            #endregion

            #region Fuentes

            Font fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLACK);
            Font th = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.BLACK);
            Font td = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLACK);


            #endregion

            #region Cabecera


            Paragraph puchOrder = new Paragraph(" REPORTE USUARIO HORAS INGRESADAS SOINDUS ")
            {
                Alignment = Element.ALIGN_CENTER
            };

            doc.Add(puchOrder);

            #endregion

            #region Cuerpo

            doc.Add(new Chunk("\n"));

            var table = new PdfPTable(new float[] { 70f, 15f, 15f }) { WidthPercentage = 100 };

            // Esta es la primera fila

            table.AddCell(new PdfPCell(new Phrase("Proyecto", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
            table.AddCell(new PdfPCell(new Phrase("Fecha", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
            table.AddCell(new PdfPCell(new Phrase("Hora", th)) { BackgroundColor = new BaseColor(150, 150, 150), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });



            foreach (var item in datos)
            {

                table.AddCell(new PdfPCell(new Phrase(item.Descripcion, td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                table.AddCell(new PdfPCell(new Phrase(item.Fecha.ToString("dd-MM-yyyy"), td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                table.AddCell(new PdfPCell(new Phrase(Math.Round(item.Hora, 1).ToString().Replace(",", "."), td)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7f });
                doc.Add(table);
                table = new PdfPTable(new float[] { 70f, 15f, 15f }) { WidthPercentage = 100 };


            }

            // Agregamos la tabla al documento
            // Ceramos el documento

            #endregion





            doc.Close();

            byte[] byteStream = memStream.ToArray();

            memStream = new MemoryStream();

            memStream.Write(byteStream, 0, byteStream.Length);

            memStream.Position = 0;

            return new FileStreamResult(memStream, "application/pdf");
        }
    }
}