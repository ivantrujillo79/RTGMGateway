using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RTGMGateway
{
    [TestFixture]
    class TestRTGMActualizaPedido
    {
        [TestCase(RTGMCore.Fuente.Sigamet)]
        [TestCase(RTGMCore.Fuente.CRM)]
        public void pruebaActualizaPedido(RTGMCore.Fuente FuentePrueba)
        {
            RTGMActualizarPedido objGateway = new RTGMActualizarPedido();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6162, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6162" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6163, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6163" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6164, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6164" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6165, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6165" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6166, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6166" });
            
            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido { Fuente = FuentePrueba, IDEmpresa = 1, Pedidos = lstPedido, Portatil = false, TipoActualizacion = RTGMCore.TipoActualizacion.Saldo, Usuario = "ROPIMA" };

            List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);

            Assert.IsNotNull(ListaRespuesta);
            Assert.AreEqual(ListaRespuesta.Count, 5);
        }




    }
}
