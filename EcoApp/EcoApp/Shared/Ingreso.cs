using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoApp.Shared
{
    public class Ingreso
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string IngresoTipoId { get; set; }


        public string UsuarioId { get; set; }

        public string Observacion { get; set; }


        public string ArchivoRuta { get; set; }

        public decimal Valor { get; set; }
        public DateTime FechaPago { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistroId { get; set; }

        [BsonIgnore]
        public string IngresoTipoNombre { get; set; }
        [BsonIgnore]
        public string IngresoUsuarioNombre { get; set; }
        [BsonIgnore]
        public string IngresoUsuarioRegistroNombre { get; set; }


    }
}
