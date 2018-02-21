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
        public string UrlServicio{
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

        /// <summary>
        /// Método que recupera las direcciones de entrega
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudDireccionEntrega</param>
        public List<RTGMCore.Pedido> buscarPedidos(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.DatosFiscales buscarDatoFiscal(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Georreferencia buscarGeorreferencia(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.CondicionesCredito buscarCondicionesCredito(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Empleado buscarEmpleado(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Precio buscarPrecio(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.ConfiguracionSuministro buscarConfiguracionSuministro(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Zona buscarZona(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Ruta buscarRuta(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.ZonaEconomica buscarZonaEconomica(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.ProgramacionSuministro buscarProgramacionSuministro(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.GiroCliente buscarGiroCliente(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.RamoCliente buscarRamoCliente(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.TipoCliente buscarTipoCliente(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.OrigenCliente buscarOrigenCliente(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.TarjetaCredito buscarTarjetaCredito(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.AgendaGestionCobranza buscarAgendaCobranza(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Precio buscarProducto(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.Descuento buscarDescuento(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

		/// 
		/// <param name="ParSolicitud"></param>
		public RTGMCore.TipoFacturacion buscarTipoFacturacion(SolicitudPedidoGateway ParSolicitud){

			return null;
		}

	}//end RTGMPedidoGateway
    
}//end namespace RTGMGateway