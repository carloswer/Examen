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
        public string Nombre_Completo => Nombres + ' ' + ApellidoPaterno + ' ' + ApellidoMaterno;
        public DateTime FechaNacimiento { get; set; }
        public string FechaNacimiento_String => FechaNacimiento != DateTime.MinValue ? FechaNacimiento.ToString("dd/MM/yyyy") : "";
        public int Edad { get; set; }
        public int Grado{ get; set; }
        public char Grupo { get; set; }
        public Decimal Calificacion { get; set; }

    }
}