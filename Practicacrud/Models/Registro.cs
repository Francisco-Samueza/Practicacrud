using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicacrud.Models
{
    public class Registro
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Estado { get; set; }
    }
}
