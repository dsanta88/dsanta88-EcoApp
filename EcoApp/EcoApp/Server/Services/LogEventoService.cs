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
    public class LogEventoService
    {
        LeerJson objJson = new LeerJson();
        private IMongoCollection<LogEvento> _logEventos;
        string conexionMongo = "";
        string bdName = "";
        public LogEventoService(IConfiguration configuration)
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            _logEventos = database.GetCollection<LogEvento>("LogEventos");
        }

        public bool Save(Exception ex)
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
                _logEventos.InsertOne(model);
                return true;
            }
            catch (Exception exe)
            {
                Save(exe);
                return false;
            }

        }
    }
}
