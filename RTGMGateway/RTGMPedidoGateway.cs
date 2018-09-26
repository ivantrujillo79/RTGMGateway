using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
//using System.Threading.Tasks;

namespace RTGMGateway
{
	public class RTGMPedidoGateway
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;
		private double longitudRepuesta;
		private int tiempoEspera = 180;
		private bool guardarLog;
        private BasicHttpBinding _BasicHttpBinding;
        private EndpointAddress _EndpointAddress;
        private const int MAX_CAPACITY = 2147483647;
        private RTGMCore.Fuente _Fuente;
        private string _CadenaConexion;
        private byte _Corporativo;
        private byte _Sucursal;
        private byte _Modulo;


        ~RTGMPedidoGateway(){

		}

		public RTGMPedidoGateway(byte Modulo, string CadenaConexion)
        {
            // Inicializar logger
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Creando nueva instancia de RTGMPedidoGateway...");

            try
            {
                _BasicHttpBinding = new BasicHttpBinding();
                _BasicHttpBinding.MaxReceivedMessageSize = MAX_CAPACITY;
                _BasicHttpBinding.MaxBufferSize = MAX_CAPACITY;
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);

                _Modulo = Modulo;
                _CadenaConexion = CadenaConexion;
                consultarParametros(_Modulo, _CadenaConexion);

                log.Info("Instancia de RTGMPedidoGateway creada.");
            }
            catch (ArgumentOutOfRangeException aore)
            {
                throw new RTGMTimeOutException { Mensaje = "El periodo de espera de " + TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + 
                    " segundos en la consulta al RTGM se ha excedido" };
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public virtual void Dispose(){

		}

		//private void ~RTGMGateway() { }

        #region PROPIEDADES
        public string URLServicio{
			get;
			set;
		}

		public double LongitudRepuesta{
			get{
				return longitudRepuesta;
			}
			set{
				longitudRepuesta = value;
			}
		}

		public int TiempoEspera{
			get{
				return tiempoEspera;
			}
			set{
				tiempoEspera = value;
			}
        }

        public bool GuardarLog{
            get
            {
                return guardarLog;
            }
            set
            {
                guardarLog = value;
            }
        }

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

        #region METODOS DE CLASE

        /// <summary>
        /// Registra los parámetros en el archivo log
        /// </summary>
        /// <param name="ParNombreMetodo">Nombre del método que se va a ejecutar</param>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        private void registrarParametros(string ParNombreMetodo, SolicitudPedidoGateway ParSolicitud)
        {
            log.Info("Inicia llamado a "            + ParNombreMetodo + 
                    ", ID Empresa: "                + _Corporativo + 
                    ", Source: "                    + _Fuente +
                    ", Tipo consulta pedido: "      + ParSolicitud.TipoConsultaPedido + 
                    ", Portatil: "                  + ParSolicitud.Portatil +
                    ", ID Usuario: "                + ParSolicitud.IDUsuario + 
                    ", ID Dirección entrega: "      + ParSolicitud.IDDireccionEntrega +
                    ", ID Sucursal: "               + "" + 
                    ", Fecha compromiso inicio: "   + ParSolicitud.FechaCompromisoInicio +
                    ", Fecha compromiso fin: "      + ParSolicitud.FechaCompromisoFin + 
                    ", Fecha suministro inicio: "   + ParSolicitud.FechaSuministroInicio +
                    ", Fecha suministro fin: "      + ParSolicitud.FechaSuministroFin + 
                    ", ID Zona: "                   + ParSolicitud.IDZona +
                    ", ID Ruta origen: "            + ParSolicitud.IDRutaOrigen + 
                    ", ID Ruta boletín: "           + ParSolicitud.IDRutaBoletin + 
                    ", ID Ruta suministro: "        + ParSolicitud.IDRutaSuministro + 
                    ", ID Estatus pedido: "         + ParSolicitud.IDEstatusPedido +
                    ", Estatus pedido: "            + ParSolicitud.EstatusPedidoDescripcion +
                    ", ID Estatus boletín: "        + ParSolicitud.IDEstatusBoletin +
                    ", Estatus boletín: "           + ParSolicitud.EstatusBoletin +
                    ", ID Estatus móvil: "          + ParSolicitud.IDEstatusMovil +
                    ", Estatus móvil: "             + ParSolicitud.EstatusMovilDescripcion +
                    ", ID Autotanque: "             + ParSolicitud.IDAutotanque +
                    ", ID Autotanque móvil: "       + ParSolicitud.IDAutotanqueMovil +
                    ", Serie remisión: "            + ParSolicitud.SerieRemision +
                    ", Folio remisión: "            + ParSolicitud.FolioRemision +
                    ", Serie factura: "             + ParSolicitud.SerieFactura +
                    ", Folio factura: "             + ParSolicitud.FolioFactura +
                    ", ID Zona lecturista: "        + ParSolicitud.IDZonaLecturista +
                    ", Tipo pedido: "               + ParSolicitud.TipoPedido +
                    ", Tipo servicio: "             + ParSolicitud.TipoServicio +
                    ", Año pedido: "                + ParSolicitud.AñoPed +
                    ", ID Pedido: "                 + ParSolicitud.IDPedido +
                    ", Pedido referencia: "         + ParSolicitud.PedidoReferencia + ".");
        }

        /// <summary>
        /// Ejecuta el método serviceClient.ConsultarPedidos()
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        private List<RTGMCore.Pedido> consultarPedidos(SolicitudPedidoGateway ParSolicitud)
        {
            return serviceClient.ConsultarPedidos(  _Corporativo,                           _Fuente,
                                                    ParSolicitud.TipoConsultaPedido,        ParSolicitud.Portatil,
                                                    ParSolicitud.IDUsuario,                 ParSolicitud.IDDireccionEntrega,
                                                    null,                                   ParSolicitud.FechaCompromisoInicio,
                                                    ParSolicitud.FechaCompromisoFin,        ParSolicitud.FechaSuministroInicio,
                                                    ParSolicitud.FechaSuministroFin,        ParSolicitud.IDZona,
                                                    ParSolicitud.IDRutaOrigen,              ParSolicitud.IDRutaBoletin,
                                                    ParSolicitud.IDRutaSuministro,          ParSolicitud.IDEstatusPedido,
                                                    ParSolicitud.EstatusPedidoDescripcion,  ParSolicitud.IDEstatusBoletin,
                                                    ParSolicitud.EstatusBoletin,            ParSolicitud.IDEstatusMovil,
                                                    ParSolicitud.EstatusMovilDescripcion,   ParSolicitud.IDAutotanque,
                                                    ParSolicitud.IDAutotanqueMovil,         ParSolicitud.SerieRemision,
                                                    ParSolicitud.FolioRemision,             ParSolicitud.SerieFactura,
                                                    ParSolicitud.FolioFactura,              ParSolicitud.IDZonaLecturista,
                                                    ParSolicitud.TipoPedido,                ParSolicitud.TipoServicio,
                                                    ParSolicitud.AñoPed,                    ParSolicitud.IDPedido,
                                                    ParSolicitud.PedidoReferencia);
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

        #endregion

        #region METODOS DE BUSQUEDA

        /// <summary>
        /// Método que recupera los pedidos
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        public List<RTGMCore.Pedido> buscarPedidos(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("===   Inicia ejecución de método buscarPedidos   ===");

                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

                //RTGMCore.Fuente source;

                //source = RTGMCore.Fuente.Sigamet;

                registrarParametros("buscarPedidos", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                if (lstPedidos.Count > 0)
                {
                    if (lstPedidos[0].Message != null && lstPedidos[0].Message.Contains("La consulta no regreso datos"))
                    {
                        lstPedidos.Clear();
                    }
                }

                lstPedidos.ForEach(x => log.Info(Utilerias.SerializarAString(x)));

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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

                log.Info("===   Finaliza ejecución de método buscarPedidos   ===");
            }

            return lstPedidos;
        }
        
        #endregion
        
    }//end RTGMPedidoGateway

}//end namespace RTGMGateway