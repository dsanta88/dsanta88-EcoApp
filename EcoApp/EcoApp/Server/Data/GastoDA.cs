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
    public class GastoDA
    {
        LogEventoDA logDA = new LogEventoDA();
        LeerJson objJson = new LeerJson();
        private IMongoCollection<Gasto> tbl_gastos;
        private IMongoCollection<GastoTipo> tbl_gastos_tipos;
        private IMongoCollection<Proveedor> tbl_proveedores;
        private IMongoCollection<Usuario> tbl_usuarios;
        string conexionMongo = "";
        string bdName = "";
        public GastoDA()
        {
            conexionMongo = objJson.GetConexionMongo();
            bdName = objJson.GetBdNameMongo();
            var client = new MongoClient(conexionMongo);
            var database = client.GetDatabase(bdName);
            tbl_gastos = database.GetCollection<Gasto>("Gastos");
            tbl_gastos_tipos = database.GetCollection<GastoTipo>("GastosTipos");
            tbl_proveedores = database.GetCollection<Proveedor>("Proveedores");
            tbl_usuarios = database.GetCollection<Usuario>("Usuarios");
        }


        public List<Gasto> Obtener(string id)
        {
            List<Gasto> list = new List<Gasto>();
            try
            {
                var gastos = tbl_gastos.Find(FilterDefinition<Gasto>.Empty).ToList();
                var gastosTipos = tbl_gastos_tipos.Find(FilterDefinition<GastoTipo>.Empty).ToList();
                var proveedores = tbl_proveedores.Find(FilterDefinition<Proveedor>.Empty).ToList();
                var usuarios = tbl_usuarios.Find(FilterDefinition<Usuario>.Empty).ToList();

                var result = (from gast in gastos.AsQueryable()
                              join gastTip in gastosTipos.AsQueryable() on gast.GastoTipoId equals gastTip.Id
                              join usu in usuarios.AsQueryable() on gast.ResponsableUsuarioId equals usu.Id
                              join prov in proveedores.AsQueryable() on gast.ProveedorId equals prov.Id
                              where (gast.Id == id || id == "-1")
                              select new
                              {
                                  GastoId = gast.Id,
                                  gast.GastoTipoId,
                                  GastoTipoNombre = gastTip.Nombre,
                                  ResponsableNombre = usu.Nombre,
                                  ProveedorRazonSocial=prov.RazonSocial,
                                  gast.ProveedorId,
                                  gast.ResponsableUsuarioId,
                                  gast.Valor,
                                  gast.Observacion,
                                  gast.ArchivoRuta,
                                  gast.FechaPago,
                                  gast.FechaRegistro,
                                  gast.UsuarioRegistroId,
                              }).ToList();

                foreach (var item in result)
                {
                    Gasto obj = new Gasto();
                    obj.Id = item.GastoId;
                    obj.GastoTipoId = item.GastoTipoId;
                    obj.GastoTipoNombre = item.GastoTipoNombre;
                    obj.ResponsableUsuarioId = item.ResponsableNombre;
                    obj.ProveedorRazonSocial = item.ProveedorRazonSocial;
                    obj.ProveedorId = item.ProveedorId;
                    obj.ResponsableUsuarioId = item.ResponsableUsuarioId;
                    obj.Valor = item.Valor;
                    obj.ArchivoRuta = item.ArchivoRuta;
                    obj.FechaPago = item.FechaPago;
                    obj.Observacion = item.Observacion;
                    obj.FechaRegistro = item.FechaRegistro;
                    obj.GastoUsuarioRegistroNombre = usuarios.Where(x => x.Id == item.UsuarioRegistroId).FirstOrDefault().Nombre;
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

        public bool IngresarEditar(Gasto model)
        {
            model.FechaRegistro = DateTime.Now;
            try
            {
                var obj = tbl_gastos.Find(x => x.Id == model.Id).FirstOrDefault();
                if (obj == null)
                {
                    tbl_gastos.InsertOne(model);
                }
                else
                {
                    tbl_gastos.ReplaceOne(x => x.Id == model.Id, model);
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
                tbl_gastos.DeleteOne(u => u.Id == id);
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
