using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Data;



namespace RTGMGateway
{
    public class RTGMGateway
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string URLServicio { get; set; }
        RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;
        private double longitudRepuesta;
        private int tiempoEspera;
        private bool guardarLog;
        private string _CadenaConexion;
        private byte _Modulo;
        private RTGMCore.Fuente _Fuente;
        private byte _Corporativo;
        private byte _Sucursal;

        public RTGMGateway(byte Modulo, string CadenaConexion)
        {
            // Inicializar logger
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Creando nueva instancia de RTGMGateway...");

            try
            {
                DataRow drParametros;
                _Modulo = Modulo;
                _CadenaConexion = CadenaConexion;
                DAO objDatos = new DAO(Modulo, CadenaConexion);
                _Fuente = objDatos.consultarFuente(Modulo, CadenaConexion);
                drParametros = objDatos.consultarCorporativoSucursal(Modulo, CadenaConexion);
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
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
            log.Info("Instancia de RTGMGateway creada.");
        }

        public double LongitudRepuesta
        {
            get
            {
                return longitudRepuesta;
            }
            set
            {
                longitudRepuesta = value;
            }
        }

        public int TiempoEspera
        {
            get
            {
                return tiempoEspera;
            }
            set
            {
                tiempoEspera = value;
            }
        }

        public bool GuardarLog
        {
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

        #region METODOS

        /// <summary>
        /// Método que recupera las direcciones de entrega
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudGateway</param>
        public RTGMCore.DireccionEntrega buscarDireccionEntrega(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarDireccionEntrega");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                //RTGMCore.Fuente.Sigamet;                

                log.Info("Inicia llamado a buscarDireccionEntrega" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo                      + ", Sucursal: "        + _Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
                
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, null,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                foreach (RTGMCore.DireccionEntrega dir in direcciones)
                {
                    log.Info(Utilerias.SerializarAString(dir));
                }

                log.Info("Finaliza ejecución de método buscarDireccionEntrega");
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n"+ex.Message);
                }
            }

            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0];
            else
                return null;
        }
        
        public RTGMCore.DatosFiscales buscarDatoFiscal(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarDatoFiscal");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarDatoFiscal" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo                      + ", Sucursal: "        + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarDatoFiscal");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recuperan los datos fiscales en método buscarDatoFiscal");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].DatosFiscales;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Georreferencia buscarGeorreferencia(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarGeorreferencia");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarGeorreferencia" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarGeorreferencia");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera georreferencia en método buscarGeorreferencia");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].Georreferencia;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.CondicionesCredito buscarCondicionesCredito(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarCondicionesCredito");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarCondicionesCredito" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarCondicionesCredito");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera condición de credito en método buscarCondicionesCredito");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].CondicionesCredito;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Empleado buscarEmpleado(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarEmpleado");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarEmpleado" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarEmpleado");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera empleado en método buscarEmpleado");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].SupervisorComercial;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Precio buscarPrecio(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarPrecio");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarPrecio" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarPrecio");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera precio en método buscarPrecio");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].PrecioPorDefecto;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.ConfiguracionSuministro buscarConfiguracionSuministro(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarConfiguracionSuministro");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarConfiguracionSuministro" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarConfiguracionSuministro");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera configuración suministro en método buscarConfiguracionSuministro");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].ConfiguracionSuministro;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Zona buscarZona(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarZona");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarZona" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarZona");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera zona en método buscarZona");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].ZonaSuministro;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Ruta buscarRuta(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarRuta");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarRuta" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarRuta");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera ruta en método buscarRuta");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].Ruta;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.ZonaEconomica buscarZonaEconomica(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarZonaEconomica");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarZonaEconomica" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarZonaEconomica");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera zona económica en método buscarZonaEconomica");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].ZonaEconomica;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.ProgramacionSuministro buscarProgramacionSuministro(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarProgramacionSuministro");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarProgramacionSuministro" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarProgramacionSuministro");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera programación suministro en método buscarProgramacionSuministro");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].ProgramacionSuministro;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.GiroCliente buscarGiroCliente(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarGiroCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarGiroCliente" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarGiroCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera giro cliente en método buscarGiroCliente");
            //return direcciones[0].GiroCliente;
            return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.RamoCliente buscarRamoCliente(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarRamoCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarRamoCliente" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarRamoCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera ramo cliente en método buscarRamoCliente");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].Ramo;
            else
                return null;

        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.TipoCliente buscarTipoCliente(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarTipoCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                    //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarTipoCliente" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarTipoCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera tipo cliente en método buscarTipoCliente");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].TipoCliente;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.OrigenCliente buscarOrigenCliente(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarOrigenCliente");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;                    

                log.Info("Inicia llamado a buscarOrigenCliente" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarOrigenCliente");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera origen cliente en método buscarOrigenCliente");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].OrigenCliente;
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.TarjetaCredito buscarTarjetaCredito(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarTarjetaCredito");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarTarjetaCredito" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarTarjetaCredito");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera tarjeta de crédito en método buscarTarjetaCredito");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].TarjetasCredito[0];
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.AgendaGestionCobranza buscarAgendaCobranza(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarAgendaCobranza");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarAgendaCobranza" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "     + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarAgendaCobranza");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera agenda cobranza en método buscarAgendaCobranza");
            //return direcciones[0].
            return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Producto buscarProducto(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarProducto");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarProducto" +
                    ", Source: "            + source + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "        + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarProducto");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera producto en método buscarProducto");
            //return direcciones[0].
            return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.Descuento buscarDescuento(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarDescuento");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarDescuento" +
                    ", Source: "            + source + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "        + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarDescuento");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera descuento en método buscarDescuento");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].Descuentos[0];
            else
                return null;
        }

        /// 
        /// <param name="ParSolicitud"></param>
        public RTGMCore.TipoFacturacion buscarTipoFacturacion(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarTipoFacturacion");
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                EndpointAddress endpointAddress = new EndpointAddress(this.URLServicio);
                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(basicHttpBinding, endpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarTipoFacturacion" +
                    ", Source: "            + source + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo + ", Sucursal: "        + _Sucursal +     
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(source, ParSolicitud.IDCliente,
                                                                    _Corporativo, _Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método buscarTipoFacturacion");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
            }
            log.Info("Se recupera tipo facturación en método buscarTipoFacturacion");
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0].TipoFacturacion;
            else
                return null;
        }
        #endregion
    }

    //public class rtgmInvalidResponseException : Exception
    //{
    //    public rtgmInvalidResponseException(string Mensaje)
    //    {
    //        new Exception(Mensaje);
    //    }
    //}
    
}
