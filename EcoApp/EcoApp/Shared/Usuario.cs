using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcoApp.Shared
{
   public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        [BsonElement]
        public string Nombre { get; set; }

        [BsonElement]
        public string Email { get; set; }

        [BsonElement]
        public string Clave { get; set; }

        [BsonElement]
        public string Celular { get; set; }


        [BsonElement]
        public int NumeroCasa { get; set; }

        [BsonElement]
        public string Rol { get; set; }

        [BsonElement]
        public bool Estado { get; set; }

        public string EstadoDescripcion { get; set; }

        public string RolDescripcion { get; set; }

    }
}
