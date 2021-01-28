using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen2.Models
{
    public class Estudiante
    {
        public string Clave { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Grado{ get; set; }
        public char Grupo { get; set; }
        public Decimal Calificacion { get; set; }

    }
}