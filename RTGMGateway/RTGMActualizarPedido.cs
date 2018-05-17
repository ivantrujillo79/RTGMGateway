using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace RTGMGateway
{
    class RTGMActualizarPedido
    {
        public string URLServicio { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;
        private double longitudRepuesta;
        private int tiempoEspera;
        private bool guardarLog;

        public List<RTGMCore.Pedido> ActualizarPedido(SolicitudActualizarPedido Solicitud)
        {
            List<RTGMCore.Pedido> lstPedidosRepuesta = new List<RTGMCore.Pedido>();
            

            log.Info("Inicia ejecución de método ActualizarPedido");
            System.ServiceModel.BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            System.ServiceModel.EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
            serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

            lstPedidosRepuesta = serviceClient.ActualizarPedido(Solicitud.Fuente, Solicitud.IDEmpresa, Solicitud.TipoActualizacion, Solicitud.Portatil, Solicitud.Pedidos, Solicitud.Usuario);

            lstPedidosRepuesta.ForEach(x => x.Message = "NO HAY ERROR");

            return lstPedidosRepuesta;
        }
    }
}
