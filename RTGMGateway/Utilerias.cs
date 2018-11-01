using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RTGMGateway
{
    public enum EnumMetodoWS
    {
        ActualizarPedido = 1,
        ActualizarPedidoBoletin = 2,
        ActualizarPedidoSaldo = 3,
        ActualizarPedidoLiquidacion = 4,
        ActualizarPedidoCancelarLiquidacion = 5,
        BusquedaDireccionEntrega = 6,
        ConsultarPedidos = 7,
        ConsultarCondicionesCredito = 8,
    }
    
    public class Utilerias
    {
        public static void Exportar(object obSolicitud, object obRespuesta, RTGMCore.Fuente fuente, bool exitoso, EnumMetodoWS metodo)
        {
            string tipoConsulta = Enum.GetName(typeof(EnumMetodoWS), metodo);

            string ruta = AppDomain.CurrentDomain.BaseDirectory
                    + "\\Log\\" + tipoConsulta + fuente.ToString().ToUpper() + (exitoso ? "_EXITOSO.xml" : "_FALLIDO.xml");

            Utilerias.ExportarAXML(obSolicitud, ruta);
            Utilerias.ExportarAXML(obRespuesta, ruta, true);
        }

        private static void ExportarAXML(object objeto, string ruta, bool anexar = false)
        {
            var writer = new System.IO.StreamWriter(ruta, anexar);
            XmlTextWriter textWriter = new XmlTextWriter(writer);
            textWriter.Formatting = Formatting.Indented;
            textWriter.IndentChar = '\t';
            textWriter.Indentation = 1;

            try
            {
                XmlSerializer serializer = new XmlSerializer(objeto.GetType());
                serializer.Serialize(textWriter, objeto);
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                textWriter.Close();
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
        //Comentario
        public static string SerializarAString(object objeto)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(objeto.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, objeto);
                return textWriter.ToString();
            }
        }

    }
}
