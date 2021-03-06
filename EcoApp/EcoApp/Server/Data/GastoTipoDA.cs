using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class GastoTipoDA
    {
        LogEventoDA logDA = new LogEventoDA();
        LeerJson objJson = new LeerJson();
        private IMongoCollection<GastoTipo> table;
        string conexionMongo = "";
        string bdName = "";
        public GastoTipoDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            table = database.GetCollection<GastoTipo>("GastosTipos");
        }



        public List<GastoTipo> ObtenerTodos()
        {
            List<GastoTipo> list = new List<GastoTipo>();
            try
            {
                list = table.Find(FilterDefinition<GastoTipo>.Empty).ToList();
                list.Where(x => x.Estado == true).ToList().ForEach(i => i.EstadoDescripcion = "ACTIVO");
                list.Where(x => x.Estado == false).ToList().ForEach(i => i.EstadoDescripcion = "INACTIVO");
                list = list.OrderBy(x=>x.Nombre).ToList();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return list;
        }

        public GastoTipo Obtener(string id)
        {
            GastoTipo obj = new GastoTipo();
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



        public bool IngresarEditar(GastoTipo model)
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
