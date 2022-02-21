using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class ComunicadoDA
    {
        LogEventoDA logDA = new LogEventoDA();
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Comunicado> table;
        string conexionMongo = "";
        string bdName = "";
        public ComunicadoDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            table = database.GetCollection<Comunicado>("Comunicados");
        }



        public List<Comunicado> ObtenerTodos()
        {
            List<Comunicado> list = new List<Comunicado>();
            try
            {
                list = table.Find(FilterDefinition<Comunicado>.Empty).ToList();
                list.Where(x => x.Estado == true).ToList().ForEach(i => i.EstadoDescripcion = "ACTIVO");
                list.Where(x => x.Estado == false).ToList().ForEach(i => i.EstadoDescripcion = "INACTIVO");
                list = list.OrderByDescending(x => x.FechaRegistro).ToList();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return list;
        }


        public Comunicado Obtener(string id)
        {
            Comunicado obj = new Comunicado();
            try
            {
                obj = table.Find(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return obj;
        }


        public bool IngresarEditar(Comunicado model)
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
