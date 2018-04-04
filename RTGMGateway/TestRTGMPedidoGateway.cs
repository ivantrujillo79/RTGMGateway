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
        //  DireccionEntrega
        [TestCase(502692435, 0, "ELIZABETH LEON")]
        public void pruebaRecuperaDireccionEntrega(int IDCliente, int IDEmpresa, string Cliente)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                IDEmpresa = IDEmpresa,
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.Boletin,
                IDDireccionEntrega = IDCliente,
                FechaCompromisoInicio = DateTime.Now.Date,
                IDZona = 201,
                EstatusBoletin = "BOLETIN",
                Portatil = false,
                IDUsuario = null,
                IDSucursal = null,
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
                IDPedido = null,
                PedidoReferencia = null
            };

            List<RTGMCore.Pedido> objPedido = objPedidoGateway.buscarPedidos(objRequest);

            Assert.AreEqual(objPedido[0].DireccionEntrega.Nombre.Trim(), Cliente);
        }

        //  Georreferencia
        [TestCase(4, 0, "19.55350667")]
        public void pruebaRecuperaGeorreferencia(int IDCliente, int IDEmpresa, string Latitud)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                IDEmpresa = IDEmpresa,
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.Boletin,
                FechaCompromisoInicio = DateTime.Now.Date,
                IDZona = 201,
                EstatusBoletin = "BOLETIN",
                Portatil = false,
                IDUsuario = null,
                IDDireccionEntrega = null,
                IDSucursal = null,
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
                IDPedido = null,
                PedidoReferencia = null
            };

            List<RTGMCore.Pedido> objPedido = objPedidoGateway.buscarPedidos(objRequest);

            Assert.AreEqual(objPedido[0].Georreferencia.Latitud, decimal.Parse(Latitud));
        }

        [TestCase(88763, 0, 201)]
        public void pruebaRecuperaZona(int IDCliente, int IDEmpresa, int IDZona)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {

                IDEmpresa = 0,
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.Boletin,
                Portatil = false,
                IDUsuario = null,
                IDDireccionEntrega = null,
                IDSucursal = null,
                FechaCompromisoInicio = DateTime.Now.Date,
                FechaCompromisoFin = null,
                FechaSuministroInicio = null,
                FechaSuministroFin = null,
                IDZona = 201,
                IDRutaOrigen = null,
                IDRutaBoletin = null,
                IDRutaSuministro = null,
                IDEstatusPedido = null,
                EstatusPedidoDescripcion = null,
                IDEstatusBoletin = null,
                EstatusBoletin = "BOLETIN",
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
                IDPedido = null,
                PedidoReferencia = null
            };

            RTGMCore.Zona objZona = objPedidoGateway.buscarZona(objRequest);

            Assert.AreEqual(objZona.IDZona, IDZona);
        }

        //  Pedidos
        [TestCase(14876, 0, "201820614876")]
        public void pruebaRecuperaPedidoReferencia(int Pedido, int IDEmpresa, string PedidoReferencia)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                IDEmpresa = IDEmpresa,
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.Boletin,
                IDDireccionEntrega = null,
                FechaCompromisoInicio = DateTime.Now.Date,
                IDZona = 206,
                EstatusBoletin = "BOLETIN",
                Portatil = false,
                IDUsuario = null,
                IDSucursal = null,
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
                IDPedido = Pedido,
                PedidoReferencia = null
            };

            List<RTGMCore.Pedido> objPedido = objPedidoGateway.buscarPedidos(objRequest);

            Assert.AreEqual(objPedido[0].PedidoReferencia.Trim(), PedidoReferencia);
        }
    }
}
