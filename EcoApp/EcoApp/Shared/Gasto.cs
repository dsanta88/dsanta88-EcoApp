using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoApp.Shared
{
   public class Gasto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string GastoTipoId { get; set; }

        public string ProveedorId { get; set; }

        public string ResponsableUsuarioId { get; set; }



        public string Observacion { get; set; }

        public string ArchivoRuta { get; set; }

        public decimal Valor { get; set; }
        public DateTime FechaPago { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string UsuarioRegistroId { get; set; }


        [BsonIgnore]
        public string ProveedorNombre { get; set; }
        [BsonIgnore]
        public string GastoTipoNombre { get; set; }

        [BsonIgnore]
        public string ProveedorRazonSocial { get; set; }

        [BsonIgnore]
        public string ResponsableNombre { get; set; }

        [BsonIgnore]
        public string GastoUsuarioRegistroNombre { get; set; }
        


    }
}
