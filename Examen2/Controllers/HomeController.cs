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

        public JsonResult obtenerEstudiantes()
        {
            Estudiante est = new Estudiante();
            est.Nombres = "Carlos Eduardo";
            est.ApellidoPaterno = "Noriega";
            est.ApellidoMaterno = "Cazarez";
            est.FechaNacimiento = new DateTime(1995,10,13);

            var clave = crearClaveEstudiante(est,3);

            return Json(new
            {
                clave = clave
            });
        }

        [HttpPost]
        public JsonResult ImportarDatos()
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

                        estudiante.Clave = crearClaveEstudiante(estudiante, 3);

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
                var abc = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
                var abc2 = "";

                var clave = "";
                estudiante.Edad = (DateTime.Now.Date - estudiante.FechaNacimiento.Date).Days / 365;
                //CARLOS EDUARDO NORIEGA CAZAREZ
                clave += estudiante.Nombres.ToUpper().Substring(0, 2);
                clave += estudiante.ApellidoMaterno.ToUpper().Substring(estudiante.ApellidoMaterno.Length - 2, 2);

                //CA EZ

                var one = clave.ToCharArray()[0];
                var two = clave.ToCharArray()[1];
                var three = clave.ToCharArray()[2];
                var four = clave.ToCharArray()[3];

                char i = abc[0];
                var check = false;
                while (abc2.Length < abc.Length - 1)
                {
                    if (!check)
                    {
                        var value = i - veces;
                        if (value < 'A')
                        {
                            var dif = value - 'A';
                            i = (char)('Z' - Math.Abs(dif));
                        }
                        else
                            check = true;
                    }
                    else
                    {
                        if (i >= 'Z')
                        {
                            i = 'A';
                            check = true;
                        }
                        else
                            i++;
                    }

                    abc2 += i + "";
                    if (!check)
                        i++;
                }

                clave = clave.Replace(one, (char)abc2[abc.IndexOf(one)]);
                clave = clave.Replace(two, (char)abc2[abc.IndexOf(two)]);
                clave = clave.Replace(three, (char)abc2[abc.IndexOf(three)]);
                //clave = clave.Replace(four, (char)abc2[abc.IndexOf(four)]);

                clave += estudiante.Edad;

                return clave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}