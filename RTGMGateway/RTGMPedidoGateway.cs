using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RTGMGateway
{
	public class RTGMPedidoGateway
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;
		private double longitudRepuesta;
		private int tiempoEspera;
		private bool guardarLog;
        
		~RTGMPedidoGateway(){

		}

		public RTGMPedidoGateway(){

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
        #endregion

        #region METODOS DE CLASE
        /// <summary>
        /// Registra los par�metros en el archivo log
        /// </summary>
        /// <param name="ParNombreMetodo">Nombre del m�todo que se va a ejecutar</param>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        private void registrarParametros(string ParNombreMetodo, SolicitudPedidoGateway ParSolicitud)
        {
            log.Info("Inicia llamado a "            + ParNombreMetodo + 
                    ", ID Empresa: "                + ParSolicitud.IDEmpresa + 
                    ", Source: "                    + ParSolicitud.FuenteDatos +
                    ", Tipo consulta pedido: "      + ParSolicitud.TipoConsultaPedido + 
                    ", Portatil: "                  + ParSolicitud.Portatil +
                    ", ID Usuario: "                + ParSolicitud.IDUsuario + 
                    ", ID Direcci�n entrega: "      + ParSolicitud.IDDireccionEntrega +
                    ", ID Sucursal: "               + ParSolicitud.IDSucursal + 
                    ", Fecha compromiso inicio: "   + ParSolicitud.FechaCompromisoInicio +
                    ", Fecha compromiso fin: "      + ParSolicitud.FechaCompromisoFin + 
                    ", Fecha suministro inicio: "   + ParSolicitud.FechaSuministroInicio +
                    ", Fecha suministro fin: "      + ParSolicitud.FechaSuministroFin + 
                    ", ID Zona: "                   + ParSolicitud.IDZona +
                    ", ID Ruta origen: "            + ParSolicitud.IDRutaOrigen + 
                    ", ID Ruta bolet�n: "           + ParSolicitud.IDRutaBoletin + 
                    ", ID Ruta suministro: "        + ParSolicitud.IDRutaSuministro + 
                    ", ID Estatus pedido: "         + ParSolicitud.IDEstatusPedido +
                    ", Estatus pedido: "            + ParSolicitud.EstatusPedidoDescripcion +
                    ", ID Estatus bolet�n: "        + ParSolicitud.IDEstatusBoletin +
                    ", Estatus bolet�n: "           + ParSolicitud.EstatusBoletin +
                    ", ID Estatus m�vil: "          + ParSolicitud.IDEstatusMovil +
                    ", Estatus m�vil: "             + ParSolicitud.EstatusMovilDescripcion +
                    ", ID Autotanque: "             + ParSolicitud.IDAutotanque +
                    ", ID Autotanque m�vil: "       + ParSolicitud.IDAutotanqueMovil +
                    ", Serie remisi�n: "            + ParSolicitud.SerieRemision +
                    ", Folio remisi�n: "            + ParSolicitud.FolioRemision +
                    ", Serie factura: "             + ParSolicitud.SerieFactura +
                    ", Folio factura: "             + ParSolicitud.FolioFactura +
                    ", ID Zona lecturista: "        + ParSolicitud.IDZonaLecturista +
                    ", Tipo pedido: "               + ParSolicitud.TipoPedido +
                    ", Tipo servicio: "             + ParSolicitud.TipoServicio +
                    ", A�o pedido: "                + ParSolicitud.A�oPed +
                    ", ID Pedido: "                 + ParSolicitud.IDPedido +
                    ", Pedido referencia: "         + ParSolicitud.PedidoReferencia + ".");
        }

        /// <summary>
        /// Ejecuta el m�todo serviceClient.ConsultarPedidos()
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        private List<RTGMCore.Pedido> consultarPedidos(SolicitudPedidoGateway ParSolicitud)
        {
            return serviceClient.ConsultarPedidos(ParSolicitud.IDEmpresa,                     ParSolicitud.FuenteDatos,
                                                    ParSolicitud.TipoConsultaPedido,        ParSolicitud.Portatil,
                                                    ParSolicitud.IDUsuario,                 ParSolicitud.IDDireccionEntrega,
                                                    ParSolicitud.IDSucursal,                ParSolicitud.FechaCompromisoInicio,
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
                                                    ParSolicitud.A�oPed,                    ParSolicitud.IDPedido,
                                                    ParSolicitud.PedidoReferencia);
        }
        #endregion

        #region METODOS DE BUSQUEDA
        /// <summary>
        /// M�todo que recupera los pedidos
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudPedidoGateway</param>
        public List<RTGMCore.Pedido> buscarPedidos(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarPedidos");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarPedidos", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarPedidos");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return lstPedidos;
        }
        
		/// <param name="ParSolicitud"></param>
		public RTGMCore.DatosFiscales buscarDatoFiscal(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarPedidos");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarPedidos", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarPedidos");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recuperan los datos fiscales en m�todo buscarDatoFiscal");
            //return lstPedidos[0];
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Georreferencia buscarGeorreferencia(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarGeorreferencia");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarGeorreferencia", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarGeorreferencia");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera la georreferencia en m�todo buscarGeorreferencia");
            return lstPedidos[0].Georreferencia;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.CondicionesCredito buscarCondicionesCredito(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarCondicionesCredito");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarCondicionesCredito", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarCondicionesCredito");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recuperan las condiciones de cr�dito en m�todo buscarCondicionesCredito");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Empleado buscarEmpleado(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarEmpleado");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarEmpleado", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarEmpleado");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarEmpleado");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Precio buscarPrecio(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarPrecio");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarPrecio", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarPrecio");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarPrecio");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.ConfiguracionSuministro buscarConfiguracionSuministro(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarConfiguracionSuministro");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarConfiguracionSuministro", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarConfiguracionSuministro");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarConfiguracionSuministro");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Zona buscarZona(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarZona");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarZona", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarZona");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarZona");
            return lstPedidos[0].Zona;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Ruta buscarRuta(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarRuta");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarRuta", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarRuta");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarRuta");
            //return lstPedidos[0].Ruta;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.ZonaEconomica buscarZonaEconomica(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarZonaEconomica");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarZonaEconomica", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarZonaEconomica");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarZonaEconomica");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.ProgramacionSuministro buscarProgramacionSuministro(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarProgramacionSuministro");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarProgramacionSuministro", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarProgramacionSuministro");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarProgramacionSuministro");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.GiroCliente buscarGiroCliente(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarGiroCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarGiroCliente", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarGiroCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarGiroCliente");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.RamoCliente buscarRamoCliente(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarRamoCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarRamoCliente", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarRamoCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarRamoCliente");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.TipoCliente buscarTipoCliente(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarTipoCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarTipoCliente", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarTipoCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarTipoCliente");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.OrigenCliente buscarOrigenCliente(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarOrigenCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarOrigenCliente", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarOrigenCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarOrigenCliente");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.TarjetaCredito buscarTarjetaCredito(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarTarjetaCredito");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarTarjetaCredito", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarTarjetaCredito");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarTarjetaCredito");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.AgendaGestionCobranza buscarAgendaCobranza(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarAgendaCobranza");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarAgendaCobranza", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarAgendaCobranza");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarAgendaCobranza");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Precio buscarProducto(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarProducto");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarProducto", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarProducto");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarProducto");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Descuento buscarDescuento(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarDescuento");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarDescuento", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarDescuento");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarDescuento");
            //return lstPedidos[0].;
            return null;
        }

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.TipoFacturacion buscarTipoFacturacion(SolicitudPedidoGateway ParSolicitud)
        {
            List<RTGMCore.Pedido> lstPedidos = new List<RTGMCore.Pedido>();
            try
            {
                log.Info("Inicia ejecuci�n de m�todo buscarTipoFacturacion");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                registrarParametros("buscarTipoFacturacion", ParSolicitud);

                lstPedidos = consultarPedidos(ParSolicitud);

                log.Info("Finaliza ejecuci�n de m�todo buscarTipoFacturacion");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera el empleado en m�todo buscarTipoFacturacion");
            //return lstPedidos[0].;
            return null;
        }
        #endregion
        
    }//end RTGMPedidoGateway

}//end namespace RTGMGateway