using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EnvioController : Controller
    {
        [HttpPost]
        public ActionResult AddEnvio(ML.Paquete paqueteAdd)
        {
            ML.Envio envio = new ML.Envio();
            envio.Paquete = new ML.Paquete();
            envio.Repartidor = new ML.Repartidor();
            envio.Envios = new List<ML.Envio>();

            bool existe = false;
            var resultPaquete = BL.Paquete.GetById(paqueteAdd.IdPaquete);
            var resultRepartidor = BL.Repartidor.GetById(paqueteAdd.Repartidor.IdRepartidor);

            envio.Paquete = resultPaquete.Item3;
            envio.Repartidor = resultRepartidor.Item3;

            if (Session["Envio"] == null)
            {
                envio.NumeroPaquetes = 1;
                envio.Envios.Add(envio);

                Session["Envio"] = envio.Envios;

            }
            else
            {
                GetEnvio(envio);
                foreach (ML.Envio paquete in envio.Envios)
                {
                    if (paqueteAdd.Repartidor.IdRepartidor == paquete.Repartidor.IdRepartidor)
                    {
                        paquete.NumeroPaquetes += 1;
                        existe = true;
                        break;
                    }
                    else
                    {
                        existe = false;
                    }
                }

                if (existe)
                {

                    Session["Envio"] = envio.Envios;
                }
                else
                {
                    envio.NumeroPaquetes = 1;
                    envio.Envios.Add(envio);
                    Session["Envio"] = envio.Envios;
                }
            }

            //return PartialView("~/Views/Paquete/Modal");
            return RedirectToAction("Envios");

        }

        [HttpGet]
        public ActionResult Envios()
        {
            ML.Envio envio = new ML.Envio();
            envio.Envios = new List<ML.Envio>();
            if (Session["Envio"] == null)
            {
                return View(envio);
            }
            else
            {
                GetEnvio(envio);
                return View(envio);
            }
        }

        public ML.Envio GetEnvio(ML.Envio envio)
        {
            List<ML.Envio> envios = (List<ML.Envio>)Session["Envio"]; //unboxing

            foreach (var obj in envios)
            {
                envio.Envios.Add(obj);
            }
            return envio;
        }

    }
}