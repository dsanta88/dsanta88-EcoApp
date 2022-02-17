
using EcoApp.Server.Services;
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
    public class UsuariosController : ControllerBase
    {

        private UsuarioService usuarioService;

        public UsuariosController(UsuarioService _usuarioService)
        {
            usuarioService = _usuarioService;
        }


        [HttpGet("[action]/{email}/{clave}")]
        public IActionResult Autenticar(string email, string clave)
        {
            Response response = new Response();
            try
            {
                Usuario obj = usuarioService.Autenticar(email,clave);
                response.IsSuccessful = true;
                response.Data = obj;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                //logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }



        [HttpGet()]
        public IActionResult Get()
        {
            Response response = new Response();
            try
            {
                List<Usuario> list = usuarioService.GetAllUsuarios(); ;
                response.IsSuccessful = true;
                response.Data = list;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                //logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
          }

        
        [HttpGet("[action]/{id:length(24)}")]
        public ActionResult<Usuario> GetUserById(string id)
        {
            Response response = new Response();
            try
            {
                Usuario obj = usuarioService.GetUsuario(id);
                response.IsSuccessful = true;
                response.Data = obj;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                //logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(Usuario model)
        {
            Response response = new Response();
            try
            {
                if (usuarioService.SaveOrUpdate(model))
                {
                    response.IsSuccessful = true;
                }
                else
                {
                    response.IsSuccessful = false;
                    //response.Message = mensajes.msgErrorGuardar();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                //logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }
        [HttpDelete("{id:length(24)}")]
        public Usuario Delete(string id)
        {
            return usuarioService.Delete(id);
        }




        //LogEventosDA logDA = new LogEventosDA();
        //Mensajes mensajes = new Mensajes();



 
        //    return Ok(response);
        //}


        //[HttpGet("{id}")]
        //public IActionResult Get(string id)
        //{
        //    Response response = new Response();
        //    try
        //    {
        //        List<User> list = datos.GetAllUsuarios(id);
        //        response.IsSuccessful = true;
        //        response.Data = list;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        //logDA.LogEventoIngresar(ex);
        //    }

        //    return Ok(response);
        //}



        //[HttpPost]
        //public IActionResult Add(User model)
        //{
        //    Response response = new Response();
        //    try
        //    {
        //        if (datos.SaveOrUpdate(model))
        //        {
        //            response.IsSuccessful = true;
        //        }
        //        else
        //        {
        //            response.IsSuccessful = false;
        //            //response.Message = mensajes.msgErrorGuardar();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        //logDA.LogEventoIngresar(ex);
        //    }

        //    return Ok(response);
        //}

        //[HttpPost("[action]")]
        //public IActionResult Edit(Usuario model)
        //{
        //    Response response = new Response();
        //    try
        //    {
        //        if (datos.UsuariosEditar(model))
        //        {
        //            response.IsSuccessful = true;
        //        }
        //        else
        //        {
        //            response.IsSuccessful = false;
        //            //response.Message = mensajes.msgErrorEditar();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        //logDA.LogEventoIngresar(ex);
        //    }

        //    return Ok(response);
        //}

        //[HttpDelete("{Id}")]
        //public IActionResult Delete(int id)
        //{
        //    Response response = new Response();
        //    try
        //    {
        //        if (datos.UsuariosEliminar(id))
        //        {
        //            response.IsSuccessful = true;
        //        }
        //        else
        //        {
        //            response.IsSuccessful = false;
        //            //response.Message = mensajes.msgErrorEliminar();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = ex.Message;
        //        //logDA.LogEventoIngresar(ex);
        //    }

        //    return Ok(response);
        //}
    }
}
