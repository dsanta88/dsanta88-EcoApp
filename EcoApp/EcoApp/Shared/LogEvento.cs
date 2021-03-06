using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EcoApp.Shared
{
    public class LogEvento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public DateTime Fecha { get; set; }

        [BsonElement]
        public string Mensaje { get; set; }

        [BsonElement]
        public string Fuente { get; set; }

        [BsonElement]
        public string Seguimiento { get; set; }

        [BsonElement]
        public bool Estado { get; set; }
    }
}
