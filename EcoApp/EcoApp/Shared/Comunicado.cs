using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoApp.Shared
{
   public class Comunicado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public string Titulo { get; set; }

        public string Mensaje { get; set; }

        public string RolId { get; set; }

        public string ArchivoRuta { get; set; }

        public bool Estado { get; set; }

        public bool EnvioEmail { get; set; }


        public DateTime FechaVigencia { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistroId { get; set; }

        [BsonIgnore]
        public string EstadoDescripcion { get; set; }



    }
}
