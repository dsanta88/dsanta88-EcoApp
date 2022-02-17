using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class UsuariosDA
    {
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Usuario> _usuarios;
        string conexionMongo = "";
        string bdName = "";
        public UsuariosDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            _usuarios = database.GetCollection<Usuario>("usuarios");
        }
    }
}
