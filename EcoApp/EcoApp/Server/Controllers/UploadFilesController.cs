using EcoApp.Server.Data;
using EcoApp.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {

        LogEventoDA logDA = new LogEventoDA();
        private readonly IWebHostEnvironment enviroment;

        public UploadFilesController(IWebHostEnvironment enviroment)
        {
            this.enviroment = enviroment;
        }



        [HttpPost()]
        public IActionResult Post(Archivo model)
        {

            Response response = new Response();
            try
            {
                string archivo = model.Base64;


                //Valida
                if (model.Base64.IndexOf("data:image/jpeg;base64,") == 0)
                {
                    model.Base64 = model.Base64.Replace("data:image/jpeg;base64,", "");
                }

                //Convierte a Bites
                var bytesss = Convert.FromBase64String(model.Base64);
                string pathCarpete = enviroment.ContentRootPath.Replace("Server", "Client") + @"\wwwroot\";
                string path = pathCarpete + model.RutaFile;
                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytesss, 0, bytesss.Length);
                    imageFile.Flush();
                }
                response.IsSuccessful = true;
                model.RutaCarpeta = pathCarpete;
                response.Data = model;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccessful = false;
                logDA.LogEventoIngresar(ex);
            }

            return Ok(response);
        }

    }
}
