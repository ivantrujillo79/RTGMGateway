using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RTGMGateway
{
    [TestFixture]
    class TestRTGMPedidoGateway
    {
        private string _CadenaConexion = "Server=192.168.1.30;Database=sigametdevtb;User Id=ROPIMA;Password = ROPIMA9999;";
        private byte _Modulo = 1;

        //  DireccionEntrega
        //[TestCase("201820147549",502627606, RTGMCore.Fuente.Sigamet)]
        [TestCase(1555)]
        public void pruebaRecuperaPorIDPedido(int Pedido)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            List<RTGMCore.Pedido> objPedido = new List<RTGMCore.Pedido>();

            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.RegistroPedido
                ,IDPedido = Pedido
            };

            try
            {
                objPedido = objPedidoGateway.buscarPedidos(objRequest);
                Assert.IsNotNull(objPedido[0]);
                Assert.True(objPedido[0].Success);
            }
            catch (Exception ex)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(objRequest, objPedido, objPedidoGateway.Fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);

        }

        //  Servicios técnicos
        [TestCase(205)]
        public void recuperaServiciosTecnicos(int parZona)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            List<RTGMCore.Pedido> objPedido = new List<RTGMCore.Pedido>();

            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.ServiciosTecnicos
                ,EstatusPedidoDescripcion = "ACTIVO"
                ,FechaCompromisoInicio = new DateTime(2018, 8, 1)
                ,FechaCompromisoFin = new DateTime(2018, 8, 1)
                ,IDZona = parZona
                //,TipoServicio = 5
            };

            try
            {
                objPedido = objPedidoGateway.buscarPedidos(objRequest);
                Assert.IsNotNull(objPedido[0]);
                Assert.True(objPedido[0].Success);
            }
            catch (Exception ex)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(objRequest, objPedido, objPedidoGateway.Fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);

        }

        //  Estatus pedido
        [TestCase("ACTIVO")]
        public void pruebaRecuperaPorEstatusPedido(string parEstatus)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway obPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            obPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            List<RTGMCore.Pedido> Pedidos = new List<RTGMCore.Pedido>();

            SolicitudPedidoGateway obSolicitud = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.RegistroPedido,
                EstatusPedidoDescripcion = parEstatus
            };

            try
            {
                Pedidos = obPedidoGateway.buscarPedidos(obSolicitud);
                Assert.IsNotNull(Pedidos[0]);
                Assert.True(Pedidos[0].Success);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(obSolicitud, Pedidos, obPedidoGateway.Fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);
        }

        //  Georreferencia
        [TestCase("201820146299", 1, "19.54064367")]
        public void pruebaRecuperaGeorreferencia(string PedidoReferencia, int IDEmpresa, string Latitud)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.Boletin,
                FechaCompromisoInicio = DateTime.Now.Date,
                IDZona = 201,
                EstatusBoletin = "BOLETIN",
                PedidoReferencia = PedidoReferencia,
                IDDireccionEntrega = null,
                Portatil = false,
                IDUsuario = null,
                FechaCompromisoFin = null,
                FechaSuministroInicio = null,
                FechaSuministroFin = null,
                IDRutaOrigen = null,
                IDRutaBoletin = null,
                IDRutaSuministro = null,
                IDEstatusPedido = null,
                EstatusPedidoDescripcion = null,
                IDEstatusBoletin = null,
                IDEstatusMovil = null,
                EstatusMovilDescripcion = null,
                IDAutotanque = null,
                IDAutotanqueMovil = null,
                SerieRemision = null,
                FolioRemision = null,
                SerieFactura = null,
                FolioFactura = null,
                IDZonaLecturista = null,
                TipoPedido = null,
                TipoServicio = null,
                AñoPed = null,
                IDPedido = null
            };

            RTGMCore.Georreferencia objGeorreferencia = objPedidoGateway.buscarGeorreferencia(objRequest);

            Assert.AreEqual(decimal.Parse(Latitud), objGeorreferencia.Latitud);
        }

        [TestCase(502615080, 1, 2)]
        public void pruebaRecuperaZona(int Cliente, int IDEmpresa, int IDZona)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.RegistroPedido,
                FechaCompromisoInicio = DateTime.Now.Date,
                EstatusBoletin = "BOLETIN",
                IDDireccionEntrega = Cliente,
                PedidoReferencia = null,
                Portatil = false,
                IDUsuario = null,
                FechaCompromisoFin = null,
                FechaSuministroInicio = null,
                FechaSuministroFin = null,
                IDRutaOrigen = null,
                IDRutaBoletin = null,
                IDRutaSuministro = null,
                IDEstatusPedido = null,
                EstatusPedidoDescripcion = null,
                IDEstatusBoletin = null,
                IDEstatusMovil = null,
                EstatusMovilDescripcion = null,
                IDAutotanque = null,
                IDAutotanqueMovil = null,
                SerieRemision = null,
                FolioRemision = null,
                SerieFactura = null,
                FolioFactura = null,
                IDZona = null,
                IDZonaLecturista = null,
                TipoPedido = null,
                TipoServicio = null,
                AñoPed = null,
                IDPedido = null
            };

            RTGMCore.Zona objZona = objPedidoGateway.buscarZona(objRequest);

            Assert.AreEqual(IDZona, objZona.IDZona);
        }
    }
}
