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
        [TestCase("201820147549", 1, 502627606)]
        public void pruebaRecuperaDireccionEntrega(string PedidoReferencia, int IDEmpresa, int IDDireccionEntrega)
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
                PedidoReferencia = PedidoReferencia,
                IDDireccionEntrega = null,
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
                IDPedido = null
            };

            List<RTGMCore.Pedido> objPedido = objPedidoGateway.buscarPedidos(objRequest);

            Assert.AreEqual(IDDireccionEntrega, objPedido[0].IDDireccionEntrega);
        }

        //  Georreferencia
        [TestCase("201820146299", 1, "19.54064367")]
        public void pruebaRecuperaGeorreferencia(string PedidoReferencia, int IDEmpresa, string Latitud)
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
                PedidoReferencia = PedidoReferencia,
                IDDireccionEntrega = null,
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
                IDPedido = null
            };

            RTGMCore.Georreferencia objGeorreferencia = objPedidoGateway.buscarGeorreferencia(objRequest);

            Assert.AreEqual(decimal.Parse(Latitud), objGeorreferencia.Latitud);
        }

        [TestCase(502615080, 1, 2)]
        public void pruebaRecuperaZona(int Cliente, int IDEmpresa, int IDZona)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {

                IDEmpresa = IDEmpresa,
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.RegistroPedido,
                FechaCompromisoInicio = DateTime.Now.Date,
                EstatusBoletin = "BOLETIN",
                IDDireccionEntrega = Cliente,
                PedidoReferencia = null,
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
