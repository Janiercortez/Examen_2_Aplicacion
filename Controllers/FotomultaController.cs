using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Examen_2_Aplicacion.Models;

namespace Examen_2_Aplicacion.Controllers
{
    [RoutePrefix("api/Fotomulta")]
    public class FotomultaController : ApiController
    {
        private DBExamenEntities1 db = new DBExamenEntities1();

        [HttpPost]
        [Route("Grabar")]
        public IHttpActionResult GrabarFotomulta(FotomultaRequest request)
        {
            try
            {
                // Verificar si el vehículo existe
                var vehiculo = db.Vehiculoes.FirstOrDefault(v => v.Placa == request.Placa);
                if (vehiculo == null)
                {
                    // Si no existe, crearlo
                    vehiculo = new Vehiculo
                    {
                        Placa = request.Placa,
                        TipoVehiculo = request.TipoVehiculo,
                        Marca = request.Marca,
                        Color = request.Color
                    };
                    db.Vehiculoes.Add(vehiculo);
                    db.SaveChanges();
                }

                // Crear la fotomulta
                var fotomulta = new Infraccion
                {
                    PlacaVehiculo = request.Placa,
                    FechaInfraccion = request.FechaInfraccion,
                    TipoInfraccion = request.TipoInfraccion
                };
                db.Infraccions.Add(fotomulta);
                db.SaveChanges();

                // Asociar imágenes a la fotomulta
                foreach (var nombreImagen in request.Imagenes)
                {
                    db.FotoInfraccions.Add(new FotoInfraccion
                    {
                        idInfraccion = fotomulta.idFotoMulta,
                        NombreFoto = nombreImagen
                    });
                }
                db.SaveChanges();

                return Ok("Fotomulta registrada correctamente");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetByPlaca")]
        public IHttpActionResult GetFotomultasByPlaca(string placa)
        {
            try
            {
                var resultado = db.Infraccions
                    .Where(i => i.PlacaVehiculo == placa)
                    .Select(i => new
                    {
                        Vehiculo = db.Vehiculoes.Where(v => v.Placa == i.PlacaVehiculo).Select(v => new
                        {
                            v.Placa,
                            v.TipoVehiculo,
                            v.Marca,
                            v.Color
                        }).FirstOrDefault(),
                        Fotomulta = new
                        {
                            i.idFotoMulta,
                            i.FechaInfraccion,
                            i.TipoInfraccion,
                            Imagenes = db.FotoInfraccions
                                .Where(f => f.idInfraccion == i.idFotoMulta)
                                .Select(f => f.NombreFoto).ToList()
                        }
                    }).ToList();

                if (!resultado.Any())
                {
                    return NotFound();
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    public class FotomultaRequest
    {
        public string Placa { get; set; }
        public string TipoVehiculo { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public DateTime FechaInfraccion { get; set; }
        public string TipoInfraccion { get; set; }
        public List<string> Imagenes { get; set; }
    }
}
