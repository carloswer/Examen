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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Grafica()
        {
            return View();
        }

        [HttpPost]
        public JsonResult obtenerEstudiantes()
        {
            try
            {
                var listaEstudiantes = new List<Estudiante>();

                var estudiante1 = new Estudiante
                {
                    Nombres = "Carlos Eduardo",
                    ApellidoPaterno = "Noriega",
                    ApellidoMaterno = "Cazarez",
                    Calificacion = 95,
                    Grupo = 'A',
                    FechaNacimiento = DateTime.Now,
                    Grado = 5
                    };

                var estudiante2 = new Estudiante
                {
                    Nombres = "Vianey Guadalupe",
                    ApellidoPaterno = "Navarro",
                    ApellidoMaterno = "Castillon",
                    Calificacion = 100,
                    Grupo = 'b',
                    FechaNacimiento = DateTime.Now,
                    Grado = 5
                };

                var estudiante3 = new Estudiante
                {
                    Nombres = "Roberto",
                    ApellidoPaterno = "Lopez",
                    ApellidoMaterno = "Castillon",
                    Calificacion = 78,
                    Grupo = 'B',
                    FechaNacimiento = DateTime.Now,
                    Grado = 5
                };

                var estudiante4 = new Estudiante
                {
                    Nombres = "Yamileth Reyna",
                    ApellidoPaterno = "Solis",
                    ApellidoMaterno = "Lopez",
                    Calificacion = 57,
                    Grupo = 'C',
                    FechaNacimiento = DateTime.Now,
                    Grado = 4
                };

                listaEstudiantes.Add(estudiante1);
                listaEstudiantes.Add(estudiante2);
                listaEstudiantes.Add(estudiante3);
                listaEstudiantes.Add(estudiante4);


                return Json(new
                {
                    listaEstudiantes = listaEstudiantes 
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JsonResult ImportarDatos()
        {
            try
            {
                HttpRequestBase request = Request;

                //string path = "";
                //foreach (string fileName in Request.Files)
                //{
                //    HttpPostedFileBase file = Request.Files[fileName];

                //    if (file != null && file.ContentLength > 0)
                //    {
                //    }
                //}

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                List<Estudiante> listaEstudiantes = new List<Estudiante>();

                // Creamos una instancia de paquete de Excel de OfficeOpenXml
                using (ExcelPackage paquete = new ExcelPackage())
                {
                    var filename = @"C:\Users\cnoriega\Desktop\Calificaciones.xlsx";

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

                        listaEstudiantes.Add(estudiante);
                        
                    }
                }

                ViewBag.Lista = listaEstudiantes;
                return Json(new {
                    listado = listaEstudiantes
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }

    }
}