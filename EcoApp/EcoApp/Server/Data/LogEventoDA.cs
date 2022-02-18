using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class LogEventoDA
    {
        LeerJson objJson = new LeerJson();
        private IMongoCollection<LogEvento> table;
        string conexionMongo = "";
        string bdName = "";
        public LogEventoDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            table = database.GetCollection<LogEvento>("LogEventos");
        }

        public bool LogEventoIngresar(Exception ex)
        {
            LogEvento model = new LogEvento();
            model.Fecha = DateTime.Now;
            model.Mensaje = ex.Message;
            model.Fuente = ex.Source;
            model.Seguimiento = ex.StackTrace;
            model.Estado = false;


            if (model.Fuente == null)
            {
                model.Fuente = "";
            }
            if (model.Seguimiento == null)
            {
                model.Seguimiento = "";
            }

            try
            {
                table.InsertOne(model);
                return true;
            }
            catch (Exception exe)
            {
                LogEventoIngresar(exe);
                return false;
            }

        }
    }
}
