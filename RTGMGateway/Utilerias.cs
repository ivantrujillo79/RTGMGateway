using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RTGMGateway
{
    public class Utilerias
    {
        public static void ExportarAXML(object objeto, string ruta, bool anexar = false)
        {
            StringWriter stringWriter = new StringWriter();
            var writer = new System.IO.StreamWriter(ruta, anexar);

            try
            {
                XmlSerializer serializer = new XmlSerializer(objeto.GetType());
                //XmlSerializer serializer = new XmlSerializer(typeof(RTGMCore.DireccionEntregaSIGAMETDatos));
                serializer.Serialize(writer, objeto);

                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stringWriter.Close();
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}
