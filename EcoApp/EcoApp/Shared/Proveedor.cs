using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoApp.Shared
{
   public class Proveedor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string RazonSocial { get; set; }

        public string Nit { get; set; }
        public string Tipo { get; set; }

        public string Email { get; set; }

        public string Celular { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Observacion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
  
        public string UsuarioRegistroId { get; set; }


        [BsonIgnore]
        public string EstadoDescripcion { get; set; }

    }
}
