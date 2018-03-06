using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;



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

                source = RTGMCore.Fuente.Sigamet;                

                log.Info("Inicia llamado a buscarDireccionEntrega" +
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

                log.Info("Finaliza ejecución de método buscarDireccionEntrega");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarDatoFiscal" +
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

                log.Info("Finaliza ejecución de método buscarDatoFiscal");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarGeorreferencia" +
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

                log.Info("Finaliza ejecución de método buscarGeorreferencia");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarCondicionesCredito" +
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

                log.Info("Finaliza ejecución de método buscarCondicionesCredito");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarEmpleado" +
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

                log.Info("Finaliza ejecución de método buscarEmpleado");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarPrecio" +
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

                log.Info("Finaliza ejecución de método buscarPrecio");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarConfiguracionSuministro" +
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

                log.Info("Finaliza ejecución de método buscarConfiguracionSuministro");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarZona" +
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

                log.Info("Finaliza ejecución de método buscarZona");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarRuta" +
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

                log.Info("Finaliza ejecución de método buscarRuta");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarZonaEconomica" +
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

                log.Info("Finaliza ejecución de método buscarZonaEconomica");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarProgramacionSuministro" +
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

                log.Info("Finaliza ejecución de método buscarProgramacionSuministro");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarGiroCliente" +
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarRamoCliente" +
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

                log.Info("Finaliza ejecución de método buscarRamoCliente");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarTipoCliente" +
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

                log.Info("Finaliza ejecución de método buscarTipoCliente");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarOrigenCliente" +
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

                log.Info("Finaliza ejecución de método buscarOrigenCliente");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarTarjetaCredito" +
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

                log.Info("Finaliza ejecución de método buscarTarjetaCredito");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarAgendaCobranza" +
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

                log.Info("Finaliza ejecución de método buscarAgendaCobranza");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarProducto" +
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarDescuento" +
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

                log.Info("Finaliza ejecución de método buscarDescuento");
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

                source = RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarTipoFacturacion" +
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

                log.Info("Finaliza ejecución de método buscarTipoFacturacion");
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
    
}
