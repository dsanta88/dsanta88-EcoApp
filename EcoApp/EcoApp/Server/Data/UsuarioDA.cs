using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class UsuarioDA
    {
        LogEventoDA logDA = new LogEventoDA();
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Usuario> table;
        string conexionMongo = "";
        string bdName = "";
        public UsuarioDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            table = database.GetCollection<Usuario>("Usuarios");
        }

        public Usuario Autenticar(string email, string clave)
        {
            Usuario user = new Usuario();
            try
            {
                user = table.Find(x => x.Email == email && x.Clave == clave).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return user;
        }

        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> list = new List<Usuario>();
            try
            {
                list = table.Find(FilterDefinition<Usuario>.Empty).ToList();
                list.Where(x => x.EstadoUsuario == true).ToList().ForEach(i => i.EstadoUsuarioDescripcion = "ACTIVO");
                list.Where(x => x.EstadoUsuario == false).ToList().ForEach(i => i.EstadoUsuarioDescripcion = "INACTIVO");
                list.Where(x => x.EstadoPortero == true).ToList().ForEach(i => i.EstadoPorteroDescripcion = "ACTIVO");
                list.Where(x => x.EstadoPortero == false).ToList().ForEach(i => i.EstadoPorteroDescripcion = "INACTIVO");

                list.Where(x => x.Rol == "1").ToList().ForEach(i => i.RolDescripcion = "ADMINISTRADOR");
                list.Where(x => x.Rol == "2").ToList().ForEach(i => i.RolDescripcion = "PROPIERTARIO");
                list.Where(x => x.Rol == "3").ToList().ForEach(i => i.RolDescripcion = "ARRENDATARIO");
                list.Where(x => x.Rol == "4").ToList().ForEach(i => i.RolDescripcion = "TRABAJADOR");
                list.Where(x => x.Rol == "5").ToList().ForEach(i => i.RolDescripcion = "VECINO");
                list = list.OrderBy(x => x.Nombre).ToList();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return list;
        }

        public Usuario Obtener(string id)
        {
            Usuario obj = new Usuario();
            try
            {
                obj = table.Find(x=>x.Id==id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return obj;
        }



        public bool IngresarEditar(Usuario model)
        {
            try
            {
                var obj = table.Find(x => x.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    table.InsertOne(model);
                }
                else
                {
                    table.ReplaceOne(x => x.Id == model.Id, model);
                }
                return true;
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
                return false;
            }

        }


        public bool Eliminar(string id)
        {
            try
            {
                table.DeleteOne(u => u.Id == id);
                return true;
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
                return false;
            }
           
        }

        public bool LogEventosIngresar(Exception ex)
        {
            logDA.LogEventoIngresar(ex);
            return true;
        }
    }
}
