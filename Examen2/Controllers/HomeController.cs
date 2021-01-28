using Examen2.Models;
using System;
using System.Collections.Generic;
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
    }
}