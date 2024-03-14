using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PaqueteController : Controller
    {
        // GET: Paquete
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Paquete paquete = new ML.Paquete();

            var result = BL.Paquete.GetAll();
            paquete.Paquetes = result.Item3;

            paquete.Repartidor = new ML.Repartidor();

            var resultRepartidor = BL.Repartidor.GetAll();
            paquete.Repartidor.Repartidores = resultRepartidor.Item3;

            return View(paquete);
        }

        [HttpGet]
        public ActionResult Form(int? id)
        {
            if(id == null)
            {
                ML.Paquete paquete = new ML.Paquete();
                return View(paquete);
            } else
            {
                var result = BL.Paquete.GetById(id.Value);
                ML.Paquete paqueteFind = new ML.Paquete();
                paqueteFind = result.Item3;

                return View(paqueteFind);
            }
        }

        
    }
}