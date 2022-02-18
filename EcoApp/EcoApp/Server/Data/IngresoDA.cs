using EcoApp.Server.Helper;
using EcoApp.Shared;
using MongoDB.Bson;
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
        private IMongoCollection<Ingreso> tbl_ingresos;
        private IMongoCollection<IngresoTipo> tbl_ingresos_tipos;
        private IMongoCollection<Usuario> tbl_usuarios;
        string conexionMongo = "";
        string bdName = "";
        public IngresoDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            tbl_ingresos = database.GetCollection<Ingreso>("Ingresos");
            tbl_ingresos_tipos = database.GetCollection<IngresoTipo>("IngresosTipos");
            tbl_usuarios = database.GetCollection<Usuario>("Usuarios");
        }


        public List<Ingreso> Obtener(string id)
        {
            List<Ingreso> list = new List<Ingreso>();
            try
            {
               var ingresos= tbl_ingresos.Find(FilterDefinition<Ingreso>.Empty).ToList();
               var ingresosTipos=tbl_ingresos_tipos.Find(FilterDefinition<IngresoTipo>.Empty).ToList();
               var usuarios = tbl_usuarios.Find(FilterDefinition<Usuario>.Empty).ToList();

                var result = (from ingre in ingresos.AsQueryable()
                              join ingreTip in ingresosTipos.AsQueryable() on ingre.IngresoTipoId equals ingreTip.Id
                              join usu in usuarios.AsQueryable() on ingre.UsuarioId equals usu.Id
                              where( ingre.Id==id  || id=="-1")
                              select new
                              {
                                  IngresoId = ingre.Id,
                                  IngresoTipoNombre = ingreTip.Nombre,
                                  IngresoUsuarioNombre = usu.Nombre,
                                  ingre.Valor,
                                  ingre.ArchivoRuta,
                                  ingre.FechaPago,
                                  ingre.FechaRegistro,
                                  ingre.UsuarioRegistroId,
                              }).ToList();

                foreach (var item in result)
                {
                    Ingreso obj = new Ingreso();
                    obj.Id = item.IngresoId;
                    obj.IngresoTipoNombre = item.IngresoTipoNombre;
                    obj.IngresoUsuarioNombre = item.IngresoUsuarioNombre;
                    obj.Valor = item.Valor;
                    obj.ArchivoRuta = item.ArchivoRuta;
                    obj.FechaPago = item.FechaPago;
                    obj.FechaRegistro = item.FechaRegistro;
                    obj.IngresoUsuarioRegistroNombre = usuarios.Where(x => x.Id == item.UsuarioRegistroId).FirstOrDefault().Nombre;
                    list.Add(obj);
                }

           
                list = list.OrderByDescending(x => x.FechaRegistro).ToList();
            }
            catch (Exception ex)
            {
                logDA.LogEventoIngresar(ex);
            }
            return list;
        }

        public bool IngresarEditar(Ingreso model)
        {
            model.FechaRegistro = DateTime.Now;
            try
            {
                var obj = tbl_ingresos.Find(x => x.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    tbl_ingresos.InsertOne(model);
                }
                else
                {
                    tbl_ingresos.ReplaceOne(x => x.Id == model.Id, model);
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
                tbl_ingresos.DeleteOne(u => u.Id == id);
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
