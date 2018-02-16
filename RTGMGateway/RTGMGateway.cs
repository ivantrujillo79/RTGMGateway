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
        private double longitudRepuesta;
        private int tiempoEspera;
        private bool guardarLog;

        public RTGMGateway()
        {
            log.Info("Nueva instancia del Gateway ha sido creada.");
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

        #region METODOS

        /// <summary>
        /// Método que recupera las direcciones de entrega
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudDireccionEntrega</param>
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                //log.Info("Inicia llamado a BusquedaDireccionEntrega, Source: " + ParSolicitud.Fuente + " ID: " + ParSolicitud.IDDireccionEntrega + " Empresa: " + ParSolicitud.IDEmpresa + " Portatil: " + ParSolicitud.Portatil + " Autotanque: " + ParSolicitud.Autotanque);
                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }

            return direcciones[0];
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                //log.Info("Inicia llamado a BusquedaDatosFiscales, Source: " + ParSolicitud.Fuente + " ID: " + ParSolicitud.IDDireccionEntrega + " Empresa: " + ParSolicitud.IDEmpresa + " Portatil: " + ParSolicitud.Portatil + " Autotanque: " + ParSolicitud.Autotanque);
                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recuperan los datos fiscales en método buscarDatoFiscal");
            return direcciones[0].DatosFiscales;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera georreferencia en método buscarGeorreferencia");
            return direcciones[0].Georreferencia;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera condición de credito en método buscarCondicionesCredito");
            return direcciones[0].CondicionesCredito;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera empleado en método buscarEmpleado");
            return direcciones[0].SupervisorComercial;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera precio en método buscarPrecio");
            return direcciones[0].PrecioPorDefecto;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera configuración suministro en método buscarConfiguracionSuministro");
            return direcciones[0].ConfiguracionSuministro;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera zona en método buscarZona");
            return direcciones[0].ZonaSuministro;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera ruta en método buscarRuta");
            return direcciones[0].Ruta;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera zona económica en método buscarZonaEconomica");
            return direcciones[0].ZonaEconomica;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera programación suministro en método buscarProgramacionSuministro");
            return direcciones[0].ProgramacionSuministro;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera ramo cliente en método buscarRamoCliente");
            return direcciones[0].Ramo;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera tipo cliente en método buscarTipoCliente");
            return direcciones[0].TipoCliente;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera origen cliente en método buscarOrigenCliente");
            return direcciones[0].OrigenCliente;
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera tarjeta de crédito en método buscarTarjetaCredito");
            return direcciones[0].TarjetasCredito[0];
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera descuento en método buscarDescuento");
            return direcciones[0].Descuentos[0];
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

                Enum.TryParse<RTGMCore.Fuente>("SIGAMET", out source);

                log.Info("Inicia llamado a BusquedaDireccionEntrega" +
                    ", Source: "            + ParSolicitud.Fuente               + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + ParSolicitud.IDEmpresa            + ", Sucursal: "        + ParSolicitud.Sucursal +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
  
                direcciones = serviceClient.BusquedaDireccionEntrega(ParSolicitud.Fuente, ParSolicitud.IDCliente, 
                                                                    ParSolicitud.IDEmpresa, ParSolicitud.Sucursal,
                                                                    ParSolicitud.Telefono, ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre, ParSolicitud.MunicipioNombre, 
                                                                    ParSolicitud.Nombre, ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior, ParSolicitud.TipoServicio, 
                                                                    ParSolicitud.Zona, ParSolicitud.Ruta, 
                                                                    ParSolicitud.ZonaEconomica, ParSolicitud.ZonaLecturista, 
                                                                    ParSolicitud.Portatil, ParSolicitud.Usuario, 
                                                                    ParSolicitud.Referencia, ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);

                log.Info("Finaliza ejecución de método BusquedaDireccionEntrega");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            log.Info("Se recupera tipo facturación en método buscarTipoFacturacion");
            return direcciones[0].TipoFacturacion;
        }

        #endregion
    }

    
    public struct SolicitudGateway
    {

        private RTGMCore.Fuente fuente;

        private System.Nullable<int> idCliente;

        private int idEmpresa;

        private System.Nullable<int> sucursal;

        private string telefono;

        private string calleNombre;

        private string coloniaNombre;

        private string municipioNombre;

        private string nombre;

        private System.Nullable<int> numeroExterior;

        private string numeroInterior;

        private System.Nullable<int> tipoServicio;

        private System.Nullable<int> zona;

        private System.Nullable<int> zonaEconomica;

        private int zonaLecturista;

        private bool portatil;

        private string usuario;

        private string referencia;

        private System.Nullable<int> idAutotanque;

        private System.Nullable<int> ruta;

        private System.Nullable<System.DateTime> fechaConsulta;

        #region PROPIEDADES
        public RTGMCore.Fuente Fuente
        {
            get
            {
                return fuente;
            }
            set
            {
                fuente = value;
            }
        }

        public System.Nullable<int> IDCliente
        {
            get
            {
                return idCliente;
            }
            set
            {
                idCliente = value;
            }
        }

        public int IDEmpresa
        {
            get
            {
                return idEmpresa;
            }
            set
            {
                idEmpresa = value;
            }
        }

        public System.Nullable<int> Sucursal
        {
            get
            {
                return sucursal;
            }
            set
            {
                sucursal = value;
            }
        }

        public string Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
            }
        }

        public string CalleNombre
        {
            get
            {
                return calleNombre;
            }
            set
            {
                calleNombre = value;
            }
        }

        public string ColoniaNombre
        {
            get
            {
                return coloniaNombre;
            }
            set
            {
                coloniaNombre = value;
            }
        }

        public string MunicipioNombre
        {
            get
            {
                return municipioNombre;
            }
            set
            {
                municipioNombre = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public System.Nullable<int> NumeroExterior
        {
            get
            {
                return numeroExterior;
            }
            set
            {
                numeroExterior = value;
            }
        }

        public string NumeroInterior
        {
            get
            {
                return numeroInterior;
            }
            set
            {
                numeroInterior = value;
            }
        }

        public System.Nullable<int> TipoServicio
        {
            get
            {
                return tipoServicio;
            }
            set
            {
                tipoServicio = value;
            }
        }

        public System.Nullable<int> Zona
        {
            get
            {
                return zona;
            }
            set
            {
                zona = value;
            }
        }

        public System.Nullable<int> ZonaEconomica
        {
            get
            {
                return zonaEconomica;
            }
            set
            {
                zonaEconomica = value;
            }
        }

        public int ZonaLecturista
        {
            get
            {
                return zonaLecturista;
            }
            set
            {
                zonaLecturista = value;
            }
        }

        public bool Portatil
        {
            get
            {
                return portatil;
            }
            set
            {
                portatil = value;
            }
        }

        public string Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        public string Referencia
        {
            get
            {
                return referencia;
            }
            set
            {
                referencia = value;
            }
        }

        public System.Nullable<int> IDAutotanque
        {
            get
            {
                return idAutotanque;
            }
            set
            {
                idAutotanque = value;
            }
        }

        public System.Nullable<int> Ruta
        {
            get
            {
                return ruta;
            }
            set
            {
                ruta = value;
            }
        }

        public System.Nullable<System.DateTime> FechaConsulta
        {
            get
            {
                return fechaConsulta;
            }
            set
            {
                fechaConsulta = value;
            }
        }
        #endregion

    }//end SolicitudGateway

}
