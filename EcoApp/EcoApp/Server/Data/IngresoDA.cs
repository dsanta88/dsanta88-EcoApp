using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class IngresoDA
    {
        LogEventoDA logDA = new LogEventoDA();
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Ingreso> table;
        string conexionMongo = "";
        string bdName = "";
        public IngresoDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            table = database.GetCollection<Ingreso>("Ingresos");
        }



        public List<Ingreso> ObtenerTodos()
        {
            List<Ingreso> list = new List<Ingreso>();
            try
            {
                list = table.Find(FilterDefinition<Ingreso>.Empty).ToList();
                list = list.OrderByDescending(x => x.FechaRegistro).ToList();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return list;
        }

        public Ingreso Obtener(string id)
        {
            Ingreso obj = new Ingreso();
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



        public bool IngresarEditar(Ingreso model)
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
