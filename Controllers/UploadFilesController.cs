using Examen_2_Aplicacion.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Examen_2_Aplicacion.Controllers
{
    public class UploadFilesController : ApiController
    {
       

        [HttpPost]
        public async Task<HttpResponseMessage> GrabarArchivo(HttpRequestMessage Request, string Datos, string Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.request = Request;
            UploadFiles.Datos = Datos;
            UploadFiles.Proceso = Proceso;
            return await UploadFiles.GrabarArchivo(false);
        }


        [HttpGet]
        public HttpResponseMessage Get(string NombreImagen)
        {
            clsUpload upload = new clsUpload();
            return upload.ConsultarArchivo(NombreImagen);
        }
        
        [HttpPut]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage Request, string Datos, string Proceso)
        {
            clsUpload UploadFiles = new clsUpload();
            UploadFiles.request = Request;
            UploadFiles.Datos = Datos;
            UploadFiles.Proceso = Proceso;
            return await UploadFiles.GrabarArchivo(true);
        }


        [HttpDelete]
        public HttpResponseMessage EliminarArchivo(string NombreArchivo)
        {
            clsUpload upload = new clsUpload();
            return upload.EliminarArchivo(NombreArchivo);
        }

    }
}