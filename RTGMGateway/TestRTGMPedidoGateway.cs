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

        [TestCase(4, 0, "19.55350667")]
        public void pruebaRecuperaGeorreferencia(int IDCliente, int IDEmpresa, string Latitud)
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
    }
}
