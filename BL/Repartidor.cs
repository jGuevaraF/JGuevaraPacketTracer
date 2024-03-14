using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Repartidor
    {
        public static (bool, string, List<ML.Repartidor>, Exception) GetAll()
        {
            ML.Repartidor repartidor = new ML.Repartidor();
            try
            {
                using (DL.PacketTracerEntities context = new DL.PacketTracerEntities())
                {
                    var query = context.RepartidorGetAll().ToList();

                    if (query != null)
                    {
                        repartidor.Repartidores = new List<ML.Repartidor>();

                        foreach (var objRepartidor in query)
                        {
                            ML.Repartidor rep = new ML.Repartidor();
                            rep.IdRepartidor = objRepartidor.IdRepartidor;
                            rep.Nombre = objRepartidor.Nombre;
                            rep.ApellidoPaterno = objRepartidor.ApellidoPaterno;
                            rep.ApellidoMaterno = objRepartidor.ApellidoMaterno;
                            repartidor.Repartidores.Add(rep);
                        }

                        return (true, null, repartidor.Repartidores, null);
                    }
                    else
                    {
                        return (false, "No hay repartidores", null, null);
                    }
                }

            }
            catch (Exception e)
            {
                return (false, e.Message, null, e);
            }
        }

        public static (bool, string, ML.Repartidor, Exception) GetById(int idRepartidor)
        {
            ML.Repartidor repartidor = new ML.Repartidor();
            try
            {
                using (DL.PacketTracerEntities context = new DL.PacketTracerEntities())
                {
                    var query = context.RepartidorGetById(idRepartidor).SingleOrDefault();

                    if (query != null)
                    {

                        ML.Repartidor rep = new ML.Repartidor();
                        rep.IdRepartidor = query.IdRepartidor;
                        rep.Nombre = query.Nombre;
                        rep.ApellidoPaterno = query.ApellidoPaterno;
                        rep.ApellidoMaterno = query.ApellidoMaterno;

                        return (true, null, rep, null);
                    }
                    else
                    {
                        return (false, "No se encontro el repartidor", null, null);
                    }
                }

            }
            catch (Exception e)
            {
                return (false, e.Message, null, e);
            }
        }


    }
}
