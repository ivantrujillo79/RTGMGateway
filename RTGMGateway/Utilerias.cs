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
        BusquedaDireccionEntrega = 1,
        ConsultarPedidos = 2,
        ActualizarPedido = 3
    }
    
    public class Utilerias
    {
        public static void Exportar(object obSolicitud, object obRespuesta, RTGMCore.Fuente fuente, bool exitoso, EnumMetodoWS metodo)
        {
            string rutaConsultaDireccion = AppDomain.CurrentDomain.BaseDirectory
                    + "\\Log\\" + "BusquedaDireccionEntrega" + fuente.ToString().ToUpper() + (exitoso ? "_EXITOSO.xml" : "_FALLIDO.xml");

            string rutaConsultaPedido = AppDomain.CurrentDomain.BaseDirectory
                    + "\\Log\\" + "ConsultarPedidos" + fuente.ToString().ToUpper() + (exitoso ? "_EXITOSO.xml" : "_FALLIDO.xml");

            string rutaActualizarPedido = AppDomain.CurrentDomain.BaseDirectory
                    + "\\Log\\" + "ActualizarPedido" + fuente.ToString().ToUpper() + (exitoso ? "_EXITOSO.xml" : "_FALLIDO.xml");

            switch (metodo)
            {
                case EnumMetodoWS.BusquedaDireccionEntrega:
                    Utilerias.ExportarAXML(obSolicitud, rutaConsultaDireccion);
                    Utilerias.ExportarAXML(obRespuesta, rutaConsultaDireccion, true);
                    break;

                case EnumMetodoWS.ConsultarPedidos:
                    Utilerias.ExportarAXML(obSolicitud, rutaConsultaPedido);
                    Utilerias.ExportarAXML(obRespuesta, rutaConsultaPedido, true);
                    break;

                case EnumMetodoWS.ActualizarPedido:
                    Utilerias.ExportarAXML(obSolicitud, rutaActualizarPedido);
                    Utilerias.ExportarAXML(obRespuesta, rutaActualizarPedido, true);
                    break;
            }

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
