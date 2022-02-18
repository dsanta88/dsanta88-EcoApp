using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EcoApp.Server.Data
{
    public class AuditoriaDA
    {
        LogEventoDA logDA = new LogEventoDA();
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Auditoria> table;
        string conexionMongo = "";
        string bdName = "";
        public AuditoriaDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            table = database.GetCollection<Auditoria>("Auditoria");
        }



        public List<Auditoria> ObtenerTodos()
        {
            List<Auditoria> list = new List<Auditoria>();
            try
            {
                list = table.Find(FilterDefinition<Auditoria>.Empty).ToList();
                list = list.OrderByDescending(x => x.FechaRegistro).ToList();
                list.ToList().ForEach(i => i.FechaRegistroStr = i.FechaRegistro.ToString("d MMMM yyyy h:mm tt", CultureInfo.CreateSpecificCulture("es-MX")));
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return list;
        }

        public Auditoria Obtener(string id)
        {
            Auditoria obj = new Auditoria();
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



        public bool Ingresar(Auditoria model)
        {
            model.Accion = model.Accion.ToUpper();
            model.FechaRegistro = DateTime.Now;
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

        public bool LogEventosIngresar(Exception ex)
        {
            logDA.LogEventoIngresar(ex);
            return true;
        }
    }
}
