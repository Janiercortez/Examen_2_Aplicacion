using Examen_2_Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_2_Aplicacion.Clases
{
    public class clsFotoInfraccion
    {
        private DBExamenEntities1 dbExamen = new DBExamenEntities1();

        public string idInfraccion { get; set; }

        public List<string> Archivos { get; set; }

        public string GrabarImagenes()
        {
            try
            {
                if (Archivos.Count > 0)
                {
                    foreach (string Archivo in Archivos)
                    {
                        FotoInfraccion Imagen = new FotoInfraccion();
                        Imagen.idInfraccion = Convert.ToInt32(idInfraccion);
                        Imagen.NombreFoto = Archivo;

                        dbExamen.FotoInfraccions.Add(Imagen);
                        dbExamen.SaveChanges();
                    }
                    return "Imagenes guardadas correctamente";
                }
                else
                {
                    return "No se enviaron archivos para guardar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}