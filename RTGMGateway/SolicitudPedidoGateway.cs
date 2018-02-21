using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RTGMGateway
{
	public struct SolicitudPedidoGateway
    {
        #region VARIABLES
        private int idEmpresa;

		private RTGMCore.Fuente fuenteDatos;

		private RTGMCore.TipoConsultaPedido tipoConsultaPedido;

		private bool portatil;

		private string idUsuario;

		private System.Nullable<int> idDireccionEntrega;

		private System.Nullable<int> idSucursal;

		private System.Nullable<System.DateTime> fechaCompromisoIncicio;

		private System.Nullable<System.DateTime> fechaCompromisoFin;

		private System.Nullable<System.DateTime> fechaSumistroInicio;

		private System.Nullable<System.DateTime> fechaSuministroFin;

		private System.Nullable<int> idZona;

		private System.Nullable<int> idRutaOrigen;

		private System.Nullable<int> idRutaBoletin;

		private System.Nullable<int> idRutaSuministro;

		private System.Nullable<int> idEstatusPedido;

		private string estatusPedidoDescripcion;

		private System.Nullable<int> idEstatusBoletin;

		private string estatusBoletin;

		private System.Nullable<int> idEstatusMovil;

		private string estatusMovilDescripcion;

		private System.Nullable<int> idAutotanque;

		private System.Nullable<int> idAutotanqueMovil;

		private string serieRemision;

		private System.Nullable<int> folioRemision;

		private string serieFactura;

		private System.Nullable<int> folioFactura;

		private System.Nullable<int> idZonaLecturista;

		private System.Nullable<int> tipoPedido;

		private System.Nullable<int> tipoServicio;

		private System.Nullable<int> añoPed;

		private System.Nullable<int> idPedido;

		private string pedidoReferencia;
        #endregion

        #region PROPIEDADES
        public int IDEmpresa{
			get{
				return idEmpresa;
			}
			set{
                idEmpresa = value;
			}
		}

		public RTGMCore.Fuente FuenteDatos{
			get{
				return fuenteDatos;
			}
			set{
				fuenteDatos = value;
			}
		}

		public RTGMCore.TipoConsultaPedido TipoConsultaPedido{
			get{
				return tipoConsultaPedido;
			}
			set{
				tipoConsultaPedido = value;
			}
		}

		public bool Portatil{
			get{
				return portatil;
			}
			set{
				portatil = value;
			}
		}

		public string IDUsuario{
			get{
				return idUsuario;
			}
			set{
                idUsuario = value;
			}
		}

		public System.Nullable<int> IDDireccionEntrega{
			get{
				return idDireccionEntrega;
			}
			set{
                idDireccionEntrega = value;
			}
		}

		public System.Nullable<int> IDSucursal{
			get{
				return idSucursal;
			}
			set{
                idSucursal = value;
			}
		}

		public System.Nullable<System.DateTime> FechaCompromisoIncicio{
			get{
				return fechaCompromisoIncicio;
			}
			set{
				fechaCompromisoIncicio = value;
			}
		}

		public System.Nullable<System.DateTime> FechaCompromisoFin{
			get{
				return fechaCompromisoFin;
			}
			set{
				fechaCompromisoFin = value;
			}
		}

		public System.Nullable<System.DateTime> FechaSumistroInicio{
			get{
				return fechaSumistroInicio;
			}
			set{
				fechaSumistroInicio = value;
			}
		}

		public System.Nullable<System.DateTime> FechaSuministroFin{
			get{
				return fechaSuministroFin;
			}
			set{
				fechaSuministroFin = value;
			}
		}

		public System.Nullable<int> IDZona{
			get{
				return idZona;
			}
			set{
                idZona = value;
			}
		}

		public System.Nullable<int> IDRutaOrigen{
			get{
				return idRutaOrigen;
			}
			set{
                idRutaOrigen = value;
			}
		}

		public System.Nullable<int> IDRutaBoletin{
			get{
				return idRutaBoletin;
			}
			set{
                idRutaBoletin = value;
			}
		}

		public System.Nullable<int> IDRutaSuministro{
			get{
				return idRutaSuministro;
			}
			set{
                idRutaSuministro = value;
			}
		}

		public System.Nullable<int> IDEstatusPedido{
			get{
				return idEstatusPedido;
			}
			set{
                idEstatusPedido = value;
			}
		}

		public string EstatusPedidoDescripcion{
			get{
				return estatusPedidoDescripcion;
			}
			set{
				estatusPedidoDescripcion = value;
			}
		}

		public System.Nullable<int> IDEstatusBoletin{
			get{
				return idEstatusBoletin;
			}
			set{
                idEstatusBoletin = value;
			}
		}

		public string EstatusBoletin{
			get{
				return estatusBoletin;
			}
			set{
				estatusBoletin = value;
			}
		}

		public System.Nullable<int> IDEstatusMovil{
			get{
				return idEstatusMovil;
			}
			set{
                idEstatusMovil = value;
			}
		}

		public string EstatusMovilDescripcion{
			get{
				return estatusMovilDescripcion;
			}
			set{
				estatusMovilDescripcion = value;
			}
		}

		public System.Nullable<int> IDAutotanque{
			get{
				return idAutotanque;
			}
			set{
                idAutotanque = value;
			}
		}

		public System.Nullable<int> IDAutotanqueMovil{
			get{
				return idAutotanqueMovil;
			}
			set{
                idAutotanqueMovil = value;
			}
		}

		public string SerieRemision{
			get{
				return serieRemision;
			}
			set{
				serieRemision = value;
			}
		}

		public System.Nullable<int> FolioRemision{
			get{
				return folioRemision;
			}
			set{
				folioRemision = value;
			}
		}

		public string SerieFactura{
			get{
				return serieFactura;
			}
			set{
				serieFactura = value;
			}
		}

		public System.Nullable<int> FolioFactura{
			get{
				return folioFactura;
			}
			set{
				folioFactura = value;
			}
		}

		public System.Nullable<int> IDZonaLecturista{
			get{
				return idZonaLecturista;
			}
			set{
                idZonaLecturista = value;
			}
		}

		public System.Nullable<int> TipoPedido{
			get{
				return tipoPedido;
			}
			set{
				tipoPedido = value;
			}
		}

		public System.Nullable<int> TipoServicio{
			get{
				return tipoServicio;
			}
			set{
				tipoServicio = value;
			}
		}

		public System.Nullable<int> AñoPed{
			get{
				return añoPed;
			}
			set{
				añoPed = value;
			}
		}

		public System.Nullable<int> IDPedido{
			get{
				return idPedido;
			}
			set{
                idPedido = value;
			}
		}

		public string PedidoReferencia{
			get{
				return pedidoReferencia;
			}
			set{
				pedidoReferencia = value;
			}
		}
        #endregion

    }//end SolicitudPedidoGateway

}//end namespace RTGMGateway