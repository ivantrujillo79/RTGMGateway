﻿using System;
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
        [TestCase("201820147549", 1, 502627606, RTGMCore.Fuente.Sigamet)]
        [TestCase(2, 1, 502627606, RTGMCore.Fuente.CRM)]
        public void pruebaRecuperaDireccionEntrega(int Pedido, int IDEmpresa, int IDDireccionEntrega, RTGMCore.Fuente Fuente)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                IDEmpresa = IDEmpresa,
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.Boletin,
                //IDPedido = Pedido
            };

            List<RTGMCore.Pedido> objPedido = objPedidoGateway.buscarPedidos(objRequest);

            try
            {
                Assert.IsNotNull(objPedido[0].IDDireccionEntrega);
            }
            catch(Exception ex)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(objRequest, objPedido, Fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);

        }

        //  Georreferencia
        [TestCase("201820146299", 1, "19.54064367")]
        public void pruebaRecuperaGeorreferencia(string PedidoReferencia, int IDEmpresa, string Latitud)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
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
