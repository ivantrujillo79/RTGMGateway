using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RTGMGateway
{
    [TestFixture]
    class TestRTGM
    {
        private const string CONSULTADIRECCION = "ConsultaDireccion";
        private const string CONSULTAPEDIDO = "ConsultaPedido";
        private const string ACTUALIZARPEDIDO = "ActualizarPedido";

        [TestCase(502627606, "JORGE MORALES LOPEZ", "201820147549", RTGMCore.Fuente.Sigamet)]
        public void pruebaLiquidacion(int cliente, string nombre, string pedidoReferencia, RTGMCore.Fuente fuente)
        {
            string rutaConsultaDireccion = AppDomain.CurrentDomain.BaseDirectory + "/" + fuente.ToString() + ".xml";
            
            RTGMGateway obGateway = new RTGMGateway();
            obGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            RTGMPedidoGateway obGatewayPedido = new RTGMPedidoGateway();
            obGatewayPedido.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            RTGMActualizarPedido obGatewayActualizar = new RTGMActualizarPedido();
            obGatewayActualizar.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            //  Consultar direccion entrega
            SolicitudGateway obSolicitudGateway = new SolicitudGateway
            {
                Fuente              = fuente,
                IDCliente           = cliente,
                IDEmpresa           = 1
            };

            RTGMCore.DireccionEntrega obDireccionEntrega = new RTGMCore.DireccionEntrega();
            obDireccionEntrega = obGateway.buscarDireccionEntrega(obSolicitudGateway);

            Assert.IsNotNull(obDireccionEntrega.Nombre);
            Assert.AreEqual(nombre, obDireccionEntrega.Nombre.Trim());

            Utilerias.ExportarAXML(obSolicitudGateway, rutaConsultaDireccion);
            Utilerias.ExportarAXML(obDireccionEntrega, rutaConsultaDireccion, true);

            //  Consultar pedido
            SolicitudPedidoGateway obSolicitudPedido = new SolicitudPedidoGateway
            {
                FuenteDatos             = fuente,
                IDEmpresa               = 1,
                TipoConsultaPedido      = RTGMCore.TipoConsultaPedido.Boletin,
                FechaCompromisoInicio   = DateTime.Now.Date,
                EstatusBoletin          = "BOLETIN",
                PedidoReferencia        = pedidoReferencia,
            };

            List<RTGMCore.Pedido> obPedidos = obGatewayPedido.buscarPedidos(obSolicitudPedido);
            Assert.AreEqual(cliente, obPedidos[0].IDDireccionEntrega);

            //  Actualizar pedido
            List<RTGMCore.Pedido> obPedidosActualizar = new List<RTGMCore.Pedido>();
            obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo
            {
                IDPedido            = obPedidos[0].IDPedido,
                IDZona              = obPedidos[0].IDZona,
                AnioPed             = obPedidos[0].AnioPed,
                Abono               = obPedidos[0].Total == null ? 0 : (decimal)obPedidos[0].Total,
                PedidoReferencia    = obPedidos[0].PedidoReferencia
            });

            SolicitudActualizarPedido obSolicitudActualizar = new SolicitudActualizarPedido
            {
                Fuente              = fuente,
                IDEmpresa           = 1,
                Pedidos             = obPedidosActualizar,
                TipoActualizacion   = RTGMCore.TipoActualizacion.Saldo,
                Usuario             = "ROPIMA"
            };

            List<RTGMCore.Pedido> obPedidosRespuesta = obGatewayActualizar.ActualizarPedido(obSolicitudActualizar);

            Assert.AreEqual(1, obPedidosRespuesta.Count);
        }
    }
}
