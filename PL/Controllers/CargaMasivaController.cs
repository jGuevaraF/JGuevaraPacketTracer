using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult Get()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Carga()
        {
            HttpPostedFileBase file = Request.Files["ArchivoExcel"];

            if (file != null)
            {
                if(file.ContentLength > 0)
                {
                    //OLEDB Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0 XML;Data Source= //RUTA EL EXCEL
                    string extension = Path.GetExtension(file.FileName).ToLower();

                    if(extension == ".xlsx")
                    {
                        string pathFolder = "~/CargaMasiva/";
                        //CargaMasivaHHMMSS

                        string pathArchivo = Server.MapPath(pathFolder) + Path.GetFileNameWithoutExtension(file.FileName) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(pathArchivo))
                        {
                            file.SaveAs(pathArchivo);
                            string context = "Provider = Microsoft.ACE.OLEDB.12.0; Extended Properties = Excel 12.0 XML; Data Source =" + pathArchivo;

                            //Leerlo Logica De negocio
                            var result = BL.Paquete.CargaMasivaExcel(context);
                            if (result.Item1)
                            {
                                var resultValidar = BL.Paquete.ValidarDatos(result.Item3);
                                if (resultValidar.Item1.Errores.Count > 0) //Si errores
                                {
                                    return View(resultValidar.Item1.Errores);
                                }
                                else
                                {
                                    return View();
                                }
                            }
                            else
                            {
                                //Mensaje de error 
                                return View();
                            }

                        }

                    }
                    else
                    {
                        //Menseje de error
                        return View();

                    }


                }
                else
                {
                    //Mensaje de error
                    return View();
                }
            }
            else
            {
                return View();
                //Mensaje de error 
            }
            return View();
            //Leer
            //Validar Si exite
            //Convertirlo
            //Insertar
            //Regresar
            //Ruta


        }
    }
}