using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Data;
using System.ServiceModel.Description;
using RTGMCore;

namespace RTGMGateway
{
    public class RTGMGateway
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string URLServicio { get; set; }
        RTGMCore.GasMetropolitanoRuntimeServiceClient serviceClient;
        private BasicHttpBinding _BasicHttpBinding;
        private EndpointAddress _EndpointAddress;
        private double longitudRepuesta;
        private int tiempoEspera = 180;
        private bool guardarLog;
        private const long MAX_CAPACITY = 2147483647;
                                        
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
                _BasicHttpBinding = new BasicHttpBinding();
                _BasicHttpBinding.MaxBufferPoolSize = MAX_CAPACITY;
                _BasicHttpBinding.MaxReceivedMessageSize = MAX_CAPACITY;
                //_BasicHttpBinding.ReaderQuotas.
                _BasicHttpBinding.MaxBufferSize = Int32.MaxValue;

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

                log.Info("Instancia de RTGMGateway creada.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
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
                log.Info("===   Inicia ejecución de método buscarDireccionEntrega   ===");

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                //RTGMCore.Fuente.Sigamet;                

                log.Info("Inicia llamado a buscarDireccionEntrega" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo                      + ", Sucursal: "        + "" +
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

            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " + 
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n"+ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarDireccionEntrega   ===");
            }
            if (direcciones != null && direcciones.Count > 0)
                return direcciones[0];
            else
                return null;
        }


        /// <summary>
        /// Método que recupera las direcciones de entrega de una lista de cliente
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo lista int</param>
        public List<RTGMCore.DireccionEntrega>  busquedaDireccionEntregaLista(List<int?> ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            string IdClientes = "";
            try
            {
                log.Info("===   Inicia ejecución de método BusquedaDireccionEntregaLista   ===");

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                //RTGMCore.Fuente.Sigamet;                
                foreach (var item in ParSolicitud)
                {
                    IdClientes = IdClientes + item.ToString() + ", ";
                }
                log.Info("Inicia llamado a BusquedaDireccionEntregaLista" +
                    ", Source: " + source + ", Clientes: " + IdClientes + ".");

                direcciones = serviceClient.BusquedaDireccionEntregaLista(source, ParSolicitud,
                                                                    _Corporativo, null,
                                                                    null, null,
                                                                    null, null,
                                                                    null, null,
                                                                    null, null,
                                                                    null, null,
                                                                    null, null,
                                                                    false , null,
                                                                    null, null,
                                                                    null);

                foreach (RTGMCore.DireccionEntrega dir in direcciones)
                {
                    log.Info(Utilerias.SerializarAString(dir));
                }

            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarDireccionEntrega   ===");
            }
            if (direcciones != null && direcciones.Count > 0)
                return direcciones;
            else
                return null;
        }

        /// <summary>
        /// Método que recupera una lista de direcciones de entrega
        /// </summary>
        /// <param name="ParSolicitud"></param>
        /// <returns></returns>
        public List<RTGMCore.DireccionEntrega> buscarDireccionesEntrega(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> Direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("===   Inicia ejecución de método buscarDireccionesEntrega   ===");

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                ChannelFactory<IGasMetropolitanoRuntimeService> factory = new ChannelFactory<IGasMetropolitanoRuntimeService>(_BasicHttpBinding, _EndpointAddress);
                foreach (OperationDescription op in factory.Endpoint.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dataContractBehavior =
                                op.Behaviors.Find<DataContractSerializerOperationBehavior>()
                                as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = Int32.MaxValue;
                    }
                }
                IGasMetropolitanoRuntimeService serviceClient = factory.CreateChannel();

                RTGMCore.Fuente source;

                source = _Fuente;    
                
                log.Info("Inicia llamado a buscarDireccionesEntrega" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo                      + ", Sucursal: "        + "" +
                    ", Telefono: "          + ParSolicitud.Telefono             + ", Calle: "           + ParSolicitud.CalleNombre +
                    ", Colonia: "           + ParSolicitud.ColoniaNombre        + ", Municipio: "       + ParSolicitud.MunicipioNombre +
                    ", Nombre: "            + ParSolicitud.Nombre               + ", Numero exterior: " + ParSolicitud.NumeroExterior+
                    ", Numero interior: "   + ParSolicitud.NumeroInterior       + ", Tipo servicio: "   + ParSolicitud.TipoServicio +
                    ", Zona: "              + ParSolicitud.Zona                 + ", Ruta: "            + ParSolicitud.Ruta + 
                    ", Zona económina: "    + ParSolicitud.ZonaEconomica        + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: "          + ParSolicitud.Portatil             + ", Usuario: "         + ParSolicitud.Usuario +
                    ", Referencia: "        + ParSolicitud.Referencia           + ", Autotanque: "      + ParSolicitud.IDAutotanque + ".");
                
                Direcciones = serviceClient.BusquedaDireccionEntrega(source,                        ParSolicitud.IDCliente,
                                                                    _Corporativo,                   null,
                                                                    ParSolicitud.Telefono,          ParSolicitud.CalleNombre,
                                                                    ParSolicitud.ColoniaNombre,     ParSolicitud.MunicipioNombre,
                                                                    ParSolicitud.Nombre,            ParSolicitud.NumeroExterior,
                                                                    ParSolicitud.NumeroInterior,    ParSolicitud.TipoServicio,
                                                                    ParSolicitud.Zona,              ParSolicitud.Ruta,
                                                                    ParSolicitud.ZonaEconomica,     ParSolicitud.ZonaLecturista,
                                                                    ParSolicitud.Portatil,          ParSolicitud.Usuario,
                                                                    ParSolicitud.Referencia,        ParSolicitud.IDAutotanque,
                                                                    ParSolicitud.FechaConsulta);
                                
                foreach (RTGMCore.DireccionEntrega dir in Direcciones)
                {
                    log.Info(Utilerias.SerializarAString(dir));
                }

            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                /*if (serviceClient.State == CommunicationState.Faulted)
                {
                    serviceClient.Abort();
                }
                else
                {
                    serviceClient.Close();
                }*/
                log.Info("===   Finaliza ejecución de método buscarDireccionesEntrega   ===");
            }

            return Direcciones;
        }

        /// <summary>
        /// Método que recupera clientes de acuerdo a su zona
        /// </summary>
        /// <param name="ParSolicitud">Objeto del tipo SolicitudGateway</param>
        public List<RTGMCore.DireccionEntrega> buscarClientesPorZona(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("===   Inicia ejecución de método buscarClientesPorZona   ===");

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;

                log.Info("Inicia llamado a buscarClientesPorZona" +
                    ", Source: "            + source                            + ", Cliente: "         + ParSolicitud.IDCliente + 
                    ", Empresa: "           + _Corporativo                      + ", Sucursal: "        + "" +
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

                if (direcciones[0].Message != null && 
                    (direcciones[0].Message.Contains("La consulta no regreso datos") 
                    || direcciones[0].Message.Contains("La consulta no produjo resultados")))
                {
                    direcciones.Clear();
                }

                foreach (RTGMCore.DireccionEntrega dir in direcciones)
                {
                    log.Info(Utilerias.SerializarAString(dir));
                }

            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n"+ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarClientesPorZona   ===");
            }

            if (direcciones != null && direcciones.Count > 0)
                return direcciones;
            else
                return null;
        }
        
        public RTGMCore.DatosFiscales buscarDatoFiscal(SolicitudGateway ParSolicitud)
        {
            List<RTGMCore.DireccionEntrega> direcciones = new List<RTGMCore.DireccionEntrega>();
            try
            {
                log.Info("Inicia ejecución de método buscarDatoFiscal");

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarDatoFiscal   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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

            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarGeorreferencia   ===");
            }
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarCondicionesCredito   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarEmpleado   ===");
            }
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarPrecio   ===");
            }
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarConfiguracionSuministro   ===");
            }
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarZona   ===");
            }
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarRuta   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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
                log.Info("===   Finaliza ejecución de método buscarZonaEconomica   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

                RTGMCore.Fuente source;

                source = _Fuente;
                //RTGMCore.Fuente.Sigamet;

                log.Info("Inicia llamado a buscarGiroCliente" +
                    ", Source: " + source + ", Cliente: " + ParSolicitud.IDCliente +
                    ", Empresa: " + _Corporativo + ", Sucursal: " + _Sucursal +
                    ", Telefono: " + ParSolicitud.Telefono + ", Calle: " + ParSolicitud.CalleNombre +
                    ", Colonia: " + ParSolicitud.ColoniaNombre + ", Municipio: " + ParSolicitud.MunicipioNombre +
                    ", Nombre: " + ParSolicitud.Nombre + ", Numero exterior: " + ParSolicitud.NumeroExterior +
                    ", Numero interior: " + ParSolicitud.NumeroInterior + ", Tipo servicio: " + ParSolicitud.TipoServicio +
                    ", Zona: " + ParSolicitud.Zona + ", Ruta: " + ParSolicitud.Ruta +
                    ", Zona económina: " + ParSolicitud.ZonaEconomica + ", Zona lecturista: " + ParSolicitud.ZonaLecturista +
                    ", Portatil: " + ParSolicitud.Portatil + ", Usuario: " + ParSolicitud.Usuario +
                    ", Referencia: " + ParSolicitud.Referencia + ", Autotanque: " + ParSolicitud.IDAutotanque + ".");

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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

                log.Info("===   Finaliza ejecución de método buscarGiroCliente   ===");
            }
            
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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarRamoCliente   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarTipoCliente   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarOrigenCliente   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarTarjetaCredito   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarAgendaCobranza   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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

                log.Info("===   Finaliza ejecución de método buscarProducto   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarDescuento   ===");
            }

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

                _BasicHttpBinding.CloseTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.OpenTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.ReceiveTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _BasicHttpBinding.SendTimeout = TimeSpan.FromSeconds(tiempoEspera);
                _EndpointAddress = new EndpointAddress(this.URLServicio);

                serviceClient = new RTGMCore.GasMetropolitanoRuntimeServiceClient(_BasicHttpBinding, _EndpointAddress);

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
            }
            catch (TimeoutException toe)
            {
                var rtgmtoe = new RTGMTimeoutException("Se ha excedido el tiempo de espera de " +
                    TimeSpan.FromSeconds(tiempoEspera).Seconds.ToString() + " segundos en la consulta al RTGM.");
                log.Error(toe.Message);
                log.Error(rtgmtoe.Mensaje);

                throw rtgmtoe;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (direcciones == null || direcciones.Count == 0)
                {
                    throw new Exception("El servicio RunTimeGM respondió con error.\n" + ex.Message);
                }
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

                log.Info("===   Finaliza ejecución de método buscarTipoFacturacion   ===");
            }

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
