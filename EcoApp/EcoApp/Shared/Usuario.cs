using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcoApp.Shared
{
   public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public string Celular { get; set; }

        public string Direccion { get; set; }
        public int NumeroCasa { get; set; }

        public string Rol { get; set; }
        public bool EstadoUsuario { get; set; }
        public bool EstadoPortero { get; set; }


        [BsonIgnore]
        public string EstadoUsuarioDescripcion { get; set; }
        [BsonIgnore]
        public string EstadoPorteroDescripcion { get; set; }
        [BsonIgnore]
        public string RolDescripcion { get; set; }

    }
}
