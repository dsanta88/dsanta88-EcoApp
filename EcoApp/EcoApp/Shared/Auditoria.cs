using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoApp.Shared
{
   public class Auditoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Accion { get; set; }
        public string Mensaje { get; set; }
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }
        public string Campo4 { get; set; }
        public string Campo5 { get; set; }
        public string Tabla { get; set; }
        public string Usuario { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioRegistroId { get; set; }
        public DateTime FechaRegistro { get; set; }


        [BsonIgnore]
        public string FechaRegistroStr { get; set; }
    }
}
