using Examen2.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Grafica()
        {
            return View();
        }

        //public JsonResult obtenerEstudiantes()
        //{
        //    Estudiante est = new Estudiante();
        //    est.Nombres = "Carlos Eduardo";
        //    est.ApellidoPaterno = "Noriega";
        //    est.ApellidoMaterno = "Cazarez";
        //    est.FechaNacimiento = new DateTime(1995,10,13);

        //    var clave = crearClaveEstudiante(est,3);

        //    return Json(new
        //    {
        //        clave = clave
        //    });
        //}

        public JsonResult analizarCalificaciones(List<Estudiante> listaEstudiantes)
        {
            try
            {
                
                var listaOrdenada = listaEstudiantes.OrderBy(x=>x.Calificacion).ToList();

                var _estudianteMejor = listaOrdenada.Last();
                var _estudiantePeor = listaOrdenada.First();

                var cali = listaEstudiantes.Sum(x => x.Calificacion);

                var _promedioCalificacion = listaEstudiantes.Sum(x => x.Calificacion) / listaEstudiantes.Count();

                return Json(new
                {
                    estudianteMejor = _estudianteMejor,
                    estudiantePeor = _estudiantePeor,
                    promedio = _promedioCalificacion
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult ImportarDatos(int numVeces)
        {
            try
            {
                HttpRequestBase request = Request;

                string path = "";

                path = Server.MapPath("~/File");
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    if (file != null && file.ContentLength > 0)
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        path += file.FileName;
                        file.SaveAs(path);
                    }
                }

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                List<Estudiante> listaEstudiantes = new List<Estudiante>();

                // Creamos una instancia de paquete de Excel de OfficeOpenXml
                using (ExcelPackage paquete = new ExcelPackage())
                {
                    var filename = path;

                    using (FileStream flujo = System.IO.File.Open(filename, FileMode.OpenOrCreate))
                    {
                        paquete.Load(flujo);
                    }

                    // Obtenemos la primera hoja del documento
                    ExcelWorksheet hoja1 = paquete.Workbook.Worksheets.First();
                    
                    // Empezamos a leer a partir de la segunda fila
                    for (int numFila = 2; numFila < hoja1.Dimension.End.Row ; numFila++)
                    {
                        //var nombres = hoja1.Cells[numFila, 1].Text; // NOMBRES
                        //var apellidoPaterno = hoja1.Cells[numFila, 2].Text; // APELLIDO PATERNO
                        //var apellidoMaterno = hoja1.Cells[numFila, 3].Text; // APELLIDO MATERNO
                        //var fechaNacimiento = hoja1.Cells[numFila, 4].Text; // FECHA DE NACIMIENTO
                        //var grado = hoja1.Cells[numFila, 5].Text; // GRADO
                        //var grupo = hoja1.Cells[numFila, 6].Text; // GRUPO
                        //var calificacion = hoja1.Cells[numFila, 7].Text; // CALIFICACION


                        Estudiante estudiante = new Estudiante
                        {
                            Nombres = hoja1.Cells[numFila, 1].Text, // NOMBRES
                            ApellidoPaterno = hoja1.Cells[numFila, 2].Text, // APELLIDO PATERNO
                            ApellidoMaterno = hoja1.Cells[numFila, 3].Text, // APELLIDO MATERNO
                            FechaNacimiento = Convert.ToDateTime(hoja1.Cells[numFila, 4].Text), // FECHA DE NACIMIENTO
                            Grado = Convert.ToInt32(hoja1.Cells[numFila, 5].Text), // GRADO
                            Grupo = Convert.ToChar(hoja1.Cells[numFila, 6].Text), // GRUPO
                            Calificacion = Convert.ToDecimal(hoja1.Cells[numFila, 7].Text) // CALIFICACION                            
                        };
                        estudiante.Edad = (DateTime.Now.Date - estudiante.FechaNacimiento.Date).Days / 365;
                        estudiante.Clave = crearClaveEstudiante(estudiante, numVeces);

                        listaEstudiantes.Add(estudiante);
                        
                    }
                }

                return Json(listaEstudiantes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }


        protected string crearClaveEstudiante(Estudiante estudiante, int veces)
        {
            try
            {
                var abc = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToList();
                var abc1 = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToList();

                var clave = "";
                var nuevaClave = "";

                clave += estudiante.Nombres.ToUpper().Substring(0, 2);
                clave += estudiante.ApellidoMaterno.ToUpper().Substring(estudiante.ApellidoMaterno.Length - 2, 2);

                while(veces != 0)
                {
                    var letra = abc1.Last();
                    abc1.Remove(letra);
                    abc1.Insert(0, letra);

                    veces--;
                }

                for (int i = 0; i <= clave.ToCharArray().Length - 1; i++)
                {
                    nuevaClave += abc1[abc.IndexOf(clave[i])];
                }

                nuevaClave += estudiante.Edad;

                return nuevaClave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}