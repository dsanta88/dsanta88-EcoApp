using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoApp.Shared
{
    public class Archivo
    {
        public int EmpresaId { get; set; }
        public string Base64 { get; set; }
        public string Formato { get; set; }
        public string Nombre { get; set; }

        public string RutaCarpeta { get; set; }
        public string RutaFile { get; set; }
    }
}
