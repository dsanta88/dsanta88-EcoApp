using EcoApp.Server.Helper;
using EcoApp.Shared;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Services
{
    public class UsuarioService
    {
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Usuario> _usuarios;
        string conexionMongo = "";
        string bdName = "";
        public UsuarioService(IConfiguration configuration)
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            _usuarios = database.GetCollection<Usuario>("usuarios");
        }


        public Usuario Autenticar(string email, string clave)
        {
            Usuario user = new Usuario();
            try
            {
                user = _usuarios.Find(x => x.Email == email && x.Clave == clave).FirstOrDefault();
            }
            catch
            {

            }
            return user;
        }

        public List<Usuario> GetAllUsuarios()
        {
            return _usuarios.Find(FilterDefinition<Usuario>.Empty).ToList();
        }

        public Usuario GetUsuario(string id) => _usuarios.Find(x => x.Id == id).FirstOrDefault();


        public bool SaveOrUpdate(Usuario model)
        {
            try
            {
                var obj = _usuarios.Find(x => x.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    _usuarios.InsertOne(model);
                }
                else
                {
                    _usuarios.ReplaceOne(x => x.Id == model.Id, model);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public Usuario Delete(string id)
        {
            var oldUser = _usuarios.Find(x => x.Id == id).FirstOrDefault();
            _usuarios.DeleteOne(u => u.Id == id);
            return oldUser;
        }

    }
}
