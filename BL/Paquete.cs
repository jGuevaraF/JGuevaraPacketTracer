using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paquete
    {
        public static ( ML.ResultExcel, Exception) ValidarDatos(List<ML.Paquete> paquetes)
        {
            try
            {
                ML.ResultExcel resultExcel = new ML.ResultExcel(); //Solo es para regresar la lista de errores
                string Mensaje = "";
                int contador = 2;
                resultExcel.Errores = new List<ML.ResultExcel>();
                foreach (ML.Paquete paquete in paquetes)
                {
                    Mensaje = "";
                    Mensaje += (paquete.InstruccionEntrega == null) ? "No continene instrucciones," : "";
                    Mensaje += (paquete.Peso == null) ? "No continene peso," : "";

                    //TODAS LAS Propiedades

                    if (Mensaje != null) //SI hay errores
                    {
                        ML.ResultExcel errorFila = new ML.ResultExcel();
                        errorFila.IdRegistro = contador;
                        errorFila.Mensaje = Mensaje;

                        resultExcel.Errores.Add(errorFila);
                    }
                    contador++;
                }
                return (resultExcel, null);
            }
            catch (Exception ex)
            {
                return (null, ex);
            }
        }
        
        public static (bool, string, List<ML.Paquete>, Exception) CargaMasivaExcel(string connectionString)
        {
            try
            {
                using(OleDbConnection context = new OleDbConnection(connectionString))
                {
                    var query = "SELECT * FROM [Hoja1$]";
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.Connection.Open();

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    DataTable tablaPaquete = new DataTable();
                    da.Fill(tablaPaquete);

                    if (tablaPaquete.Rows.Count > 0)
                    {
                        List<ML.Paquete> paquetes = new List<ML.Paquete>();
                        foreach (DataRow row in tablaPaquete.Rows)
                        {
                            ML.Paquete paquete = new ML.Paquete();
                            paquete.InstruccionEntrega = row[0].ToString();
                            paquete.Peso = Decimal.Parse(row[1].ToString());
                            paquete.DireccionOrigen = row[2].ToString();
                            paquete.DireccionEntrega = row[3].ToString();
                            paquete.FechaEstimadaEntrega = DateTime.Parse(row[4].ToString());
                            paquete.NumeroGuia = row[0].ToString();

                            paquetes.Add(paquete);
                        }
                        return (true, "", paquetes, null);
                    }
                    else
                    { 
                        return(false,"",null,null);
                    }
                }
            }catch (Exception ex)
            {
                return(false,ex.Message,null,ex);
            }
        }
        public static (bool, string, List<ML.Paquete>, Exception) GetAll()
        {
            ML.Paquete paquete = new ML.Paquete();
            try
            {
                using (DL.PacketTracerEntities context = new DL.PacketTracerEntities())
                {
                    var query = context.PaqueteGetAll().ToList();
                    if (query != null)
                    {
                        paquete.Paquetes = new List<ML.Paquete>();
                        foreach (var valor in query)
                        {
                            ML.Paquete objPaquete = new ML.Paquete();
                            objPaquete.IdPaquete = valor.IdPaquete;
                            objPaquete.InstruccionEntrega = valor.InstruccionEntrega;
                            objPaquete.Peso = valor.Peso.Value;
                            objPaquete.DireccionOrigen = valor.DireccionOrigen;
                            objPaquete.DireccionEntrega = valor.DireccionEntrega;
                            objPaquete.FechaEstimadaEntrega = valor.FechaEstimadaEntrega.Value;
                            objPaquete.NumeroGuia = valor.NumeroGuia;
                            objPaquete.CodigoQR = valor.CodigoQR;

                            paquete.Paquetes.Add(objPaquete);
                        }

                        return (true, null, paquete.Paquetes, null);
                    }
                    else
                    {
                        return (false, "No hay paquetes", null, null);
                    }
                }

            }
            catch (Exception e)
            {
                return (false, e.Message, null, e);
            }
        }

        public static (bool, string, ML.Paquete, Exception) GetById(int idPaquete)
        {
            ML.Paquete paquete = new ML.Paquete();
            try
            {
                using (DL.PacketTracerEntities context = new DL.PacketTracerEntities())
                {
                    var query = context.PaqueteGetById(idPaquete).SingleOrDefault();
                    if (query != null)
                    {

                        ML.Paquete objPaquete = new ML.Paquete();
                        objPaquete.IdPaquete = query.IdPaquete;
                        objPaquete.InstruccionEntrega = query.InstruccionEntrega;
                        objPaquete.Peso = query.Peso.Value;
                        objPaquete.DireccionOrigen = query.DireccionOrigen;
                        objPaquete.DireccionEntrega = query.DireccionEntrega;
                        objPaquete.FechaEstimadaEntrega = query.FechaEstimadaEntrega.Value;
                        objPaquete.NumeroGuia = query.NumeroGuia;
                        objPaquete.CodigoQR = query.CodigoQR;


                        return (true, null, objPaquete, null);
                    }
                    else
                    {
                        return (false, "No hay paquetes", null, null);
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
