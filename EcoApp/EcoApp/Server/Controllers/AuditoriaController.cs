using EcoApp.Server.Data;
using EcoApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {

        AuditoriaDA datos = new AuditoriaDA();
        LogEventoDA logDA = new LogEventoDA();
        Mensajes mensajes = new Mensajes();


        public IActionResult Get()
        {
            Response response = new Response();
            try
            {
                List<Auditoria> list = datos.ObtenerTodos();
                response.IsSuccessful = true;
                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }


        [HttpGet("[action]/{id:length(24)}")]
        public ActionResult<Auditoria> GetById(string id)
        {
            Response response = new Response();
            try
            {
                Auditoria obj = datos.Obtener(id);
                response.IsSuccessful = true;
                response.Data = obj;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(Auditoria model)
        {
            Response response = new Response();
            try
            {
                if (datos.Ingresar(model))
                {
                    response.IsSuccessful = true;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = mensajes.msgErrorGuardar();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }

    }
}