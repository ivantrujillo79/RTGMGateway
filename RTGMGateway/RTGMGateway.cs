using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RTGMGateway
{
    public class RTGMGateway
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string URLServicio { get; set; }
        RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;


        public RTGMGateway()
        {
            log.Info("Nueva instancia del Gateway ha sido creada.");
        }

        /// <summary>
        /// Método que recupera las direcciones de entrega
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudDireccionEntrega</param>
        public RTGMCore.DireccionEntrega BuscarDireccionEntrega(SolicitudDireccionEntrega ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método BusquedaDireccionEntrega");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);
                
                log.Info("Inicia llamado a BusquedaDireccionEntrega, Source: " + ParSolicitud.Fuente + " ID: " + ParSolicitud.IDDireccionEntrega + " Empresa: " + ParSolicitud.IDEmpresa + " Portatil: " + ParSolicitud.Portatil + " Autotanque: " + ParSolicitud.Autotanque);

                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDDireccionEntrega, ParSolicitud.IDEmpresa, null, null,
                   null, null, null, null, null,
                   null, null, null, null, null,
                   null, ParSolicitud.Portatil, null, null, ParSolicitud.Autotanque);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }

            return direcciones[0];
        }


        public RTGMCore.DatosFiscales BuscarDatosFiscales(SolicitudDireccionEntrega ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método BusquedaDatosFiscales");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDatosFiscales, Source: " + ParSolicitud.Fuente + " ID: " + ParSolicitud.IDDireccionEntrega + " Empresa: " + ParSolicitud.IDEmpresa + " Portatil: " + ParSolicitud.Portatil + " Autotanque: " + ParSolicitud.Autotanque);

                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDDireccionEntrega, ParSolicitud.IDEmpresa, null, null,
                   null, null, null, null, null,
                   null, null, null, null, null,
                   null, ParSolicitud.Portatil, null, null, ParSolicitud.Autotanque);

                log.Info("Finaliza ejecución de método BusquedaDatosFiscales");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recuperan los datos fiscales en método BusquedaDatosFiscales");
            return direcciones[0].DatosFiscales;
        }
    }

    public struct SolicitudDireccionEntrega
    {
        public RTGMCore.Fuente Fuente { get; set; }
        public int IDDireccionEntrega { get; set; }
        public int IDEmpresa { get; set; }
        public bool Portatil { get; set; }
        public int Autotanque { get; set; }
    }

}
