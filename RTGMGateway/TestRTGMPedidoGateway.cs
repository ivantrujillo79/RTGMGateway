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

        //[TestCase(4, 0, "1")]
        public void pruebaRecuperaGeorreferencia(int IDCliente, int IDEmpresa, string Latitud)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                IDDireccionEntrega = IDCliente,
                FechaCompromisoInicio = Convert.ToDateTime("02/07/2002"),
                IDZona = 209
                //IDAutotanque = 52
            };

            List<RTGMCore.Pedido> objPedido = objPedidoGateway.buscarPedidos(objRequest);

            Assert.AreEqual(objPedido[0].Georreferencia.Latitud, decimal.Parse(Latitud));
        }

        //[TestCase(88763, 0, 1)]
        public void pruebaRecuperaZona(int IDCliente, int IDEmpresa, int IDZona)
        {
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway();
            objPedidoGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                FuenteDatos = RTGMCore.Fuente.Sigamet,
                //IDEmpresa = IDEmpresa,
                IDDireccionEntrega = IDCliente,
                //Portatil = false,
                //IDAutotanque = 52
                IDZona = 1
            };

            RTGMCore.Zona objZona = objPedidoGateway.buscarZona(objRequest);

            Assert.AreEqual(objZona.IDZona, IDZona);
        }
    }
}
