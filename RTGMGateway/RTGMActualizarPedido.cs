using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace RTGMGateway
{
    public class RTGMActualizarPedido
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;
        private BasicHttpBinding _BasicHttpBinding;
        private EndpointAddress _EndpointAddress;
        private double longitudRepuesta;
        private int tiempoEspera = 180;
        private bool guardarLog;
        private const int MAX_CAPACITY = 2147483647;
        private RTGMCore.Fuente _Fuente;
        private string _CadenaConexion;
        private byte _Corporativo;
        private byte _Sucursal;
        private byte _Modulo;

        #region PROPIEDADES

        public string URLServicio { get; set; }

        public RTGMCore.Fuente Fuente
        {
            get
            {
                return _Fuente;
            }
        }

        public byte Corporativo
        {
            get
            {
                return _Corporativo;
            }
        }

        #endregion

        public RTGMActualizarPedido(byte Modulo, string CadenaConexion)
        {
            // Inicializar logger
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Creando instancia de RTGMActualizarPedido...");

            try
            {
                _BasicHttpBinding = new BasicHttpBinding();
                _BasicHttpBinding.MaxReceivedMessageSize = MAX_CAPACITY;
                _BasicHttpBinding.MaxBufferSize = MAX_CAPACITY;
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);

                _Modulo = Modulo;
                _CadenaConexion = CadenaConexion;
                consultarParametros(_Modulo, _CadenaConexion);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
            log.Info("Instancia de RTGMActualizarPedido creada.");
        }

        #region METODOS DE CLASE
        
        /// <summary>
        /// Registra los parámetros en el archivo log
        /// </summary>
        /// <param name="ParNombreMetodo">Nombre del método que se va a ejecutar</param>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        private void registrarParametros(SolicitudActualizarPedido ParSolicitud)
        {
            log.Info("Inicia llamado a ActualizarPedido" +
                    ", ID Empresa: "                + _Corporativo + 
                    ", Source: "                    + _Fuente +
                    ", Pedidos: "                   + ParSolicitud.Pedidos.Count + 
                    ", Portatil: "                  + ParSolicitud.Portatil +
                    ", Tipo de actualización: "     + ParSolicitud.TipoActualizacion + 
                    ", Usuario: "                   + ParSolicitud.Usuario);
        }

        /// <summary>
        /// Consulta los parámetros FuenteCRM, Corporativo y Sucursal mediante una instancia de la 
        /// clase DAO
        /// </summary>
        /// <param name="modulo"></param>
        /// <param name="cadenaConexion"></param>
        private void consultarParametros(byte modulo, string cadenaConexion)
        {
            DataRow drParametros;
            DAO obDAO = new DAO(modulo, cadenaConexion);

            _Fuente = obDAO.consultarFuente(modulo, cadenaConexion);
            drParametros = obDAO.consultarCorporativoSucursal(modulo, cadenaConexion);

            if (drParametros != null)
            {
                _Corporativo = Convert.ToByte(drParametros["Corporativo"]);
                _Sucursal = Convert.ToByte(drParametros["Sucursal"]);
            }
            else
            {
                _Corporativo = 0;
                _Sucursal = 0;
            }
        }

        /// <summary>
        /// Ejecuta el método serviceClient.ActualizarPedido()
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        private List<RTGMCore.Pedido> actualizarPedidos(SolicitudActualizarPedido ParSolicitud)
        {
            return serviceClient.ActualizarPedido(  _Fuente,                            _Corporativo, 
                                                    ParSolicitud.TipoActualizacion,     ParSolicitud.Portatil, 
                                                    ParSolicitud.Pedidos,               ParSolicitud.Usuario);
        }

        #endregion

        public List<RTGMCore.Pedido> ActualizarPedido(SolicitudActualizarPedido Solicitud)
        {
            List<RTGMCore.Pedido> lstPedidosRespuesta = new List<RTGMCore.Pedido>();

            try
            {
                log.Info("===   Inicia ejecución de método ActualizarPedido   ===");

                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

                registrarParametros(Solicitud);

                lstPedidosRespuesta = actualizarPedidos(Solicitud);
                
                lstPedidosRespuesta.ForEach(x => log.Info(Utilerias.SerializarAString(x)));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                lstPedidosRespuesta.ForEach(x => x.Message += Environment.NewLine + ex.Message);
                throw ex;
            }
            finally
            {
                if (serviceClient.State == CommunicationState.Faulted)
                {
                    serviceClient.Abort();
                }
                else
                {
                    serviceClient.Close();
                }

                log.Info("===   Finaliza ejecución de método ActualizarPedidos   ===");
            }
            return lstPedidosRespuesta;
        }
    }
}
