using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Client
{
    public class ServicioSingleton
    {
        public string UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }

        public string Email { get; set; }

        public string Rol { get; set; }

        public bool IsLogueado { get; set; }

        public string EmpresaLogo { get; set; }


        public ServicioSingleton()
        {
            UsuarioId = "";
            UsuarioNombre = "";
            Email = "";
            Rol = "";
        }
    }
}
