using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Helper
{
    public class LeerJson
    {
        public string GetConexionMongo()
        {
            //Leer archivo appsettings.json
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            //logFile = configuration["Logging:LogFile"] //Ejemplo para leer los nodos del archivo Json; 

            return configuration["conexionMongo"];

        }

        public string GetBdNameMongo()
        {
            //Leer archivo appsettings.json
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            //logFile = configuration["Logging:LogFile"] //Ejemplo para leer los nodos del archivo Json; 

            return configuration["bdName"];

        }
    }
}
