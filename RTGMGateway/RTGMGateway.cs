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

    public struct SolicitudGateway
    {

        private RTGMCore.Fuente _Fuente;

        private System.Nullable<int> _IDCliente;

        private int _IDEmpresa;

        private System.Nullable<int> _Sucursal;

        private string _Telefono;

        private string _CalleNombre;

        private string _ColoniaNombre;

        private string _MunicipioNombre;

        private string _Nombre;

        private System.Nullable<int> _NumeroExterior;

        private string _NumeroInterior;

        private System.Nullable<int> _TipoServicio;

        private System.Nullable<int> _Zona;

        private System.Nullable<int> _ZonaEconomica;

        private int _ZonaLecturista;

        private bool _Portatil;

        private string _Usuario;

        private string _Referencia;

        private System.Nullable<int> _IDAutotanque;

        public int IDEmpresa
        {
            get
            {
                return _IDEmpresa;
            }
            set
            {
                _IDEmpresa = value;
            }
        }

        public System.Nullable<int> Sucursal
        {
            get
            {
                return _Sucursal;
            }
            set
            {
                _Sucursal = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _Telefono;
            }
            set
            {
                _Telefono = value;
            }
        }

        public string CalleNombre
        {
            get
            {
                return _CalleNombre;
            }
            set
            {
                _CalleNombre = value;
            }
        }

        public string ColoniaNombre
        {
            get
            {
                return _ColoniaNombre;
            }
            set
            {
                _ColoniaNombre = value;
            }
        }

        public string MunicipioNombre
        {
            get
            {
                return _MunicipioNombre;
            }
            set
            {
                _MunicipioNombre = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
            }
        }

        public System.Nullable<int> NumeroExterior
        {
            get
            {
                return _NumeroExterior;
            }
            set
            {
                _NumeroExterior = value;
            }
        }

        public string NumeroInterior
        {
            get
            {
                return _NumeroInterior;
            }
            set
            {
                _NumeroInterior = value;
            }
        }

        public System.Nullable<int> TipoServicio
        {
            get
            {
                return _TipoServicio;
            }
            set
            {
                _TipoServicio = value;
            }
        }

        public System.Nullable<int> Zona
        {
            get
            {
                return _Zona;
            }
            set
            {
                _Zona = value;
            }
        }

        public System.Nullable<int> ZonaEconomica
        {
            get
            {
                return _ZonaEconomica;
            }
            set
            {
                _ZonaEconomica = value;
            }
        }

        public int ZonaLecturista
        {
            get
            {
                return _ZonaLecturista;
            }
            set
            {
                _ZonaLecturista = value;
            }
        }

        public bool Portatil
        {
            get
            {
                return _Portatil;
            }
            set
            {
                _Portatil = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
            }
        }

        public string Referencia
        {
            get
            {
                return _Referencia;
            }
            set
            {
                _Referencia = value;
            }
        }

        public System.Nullable<int> IDAutotanque
        {
            get
            {
                return _IDAutotanque;
            }
            set
            {
                _IDAutotanque = value;
            }
        }

    }//end SolicitudGateway

}
