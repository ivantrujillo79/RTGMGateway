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
        private string _CadenaConexion = "Server=192.168.1.30;Database=sigametdevtb;User Id=ROPIMA;Password = ROPIMA9999;";
        private byte _Modulo = 1;

        [TestCase(502627606, "ROBLEDO 24 COL. REAL ESMERALDA, ATIZAPAN DE ZARAGOZA", "201820147549", RTGMCore.Fuente.Sigamet)]
        [TestCase(6, "HERRADURA 00     AGUILERA  AZCAPOTZALCO", "1215", RTGMCore.Fuente.CRM)]
        public void pruebaLiquidacion(int cliente, string direccion, string pedidoReferencia, RTGMCore.Fuente fuente)
        {
            bool respuestaExitosa = true;
            
            RTGMGateway obGateway = new RTGMGateway(_Modulo, _CadenaConexion);
            obGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            obGateway.GuardarLog = true;
            RTGMPedidoGateway obGatewayPedido = new RTGMPedidoGateway(_Modulo, _CadenaConexion);
            obGatewayPedido.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            RTGMActualizarPedido obGatewayActualizar = new RTGMActualizarPedido();
            obGatewayActualizar.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            /*      BUSQUEDA DIRECCION ENTREGA      */
            SolicitudGateway obSolicitudGateway = new SolicitudGateway
            {
                //Fuente              = fuente,
                IDCliente           = cliente,
                //IDEmpresa           = 1
            };

            RTGMCore.DireccionEntrega obDireccionEntrega = new RTGMCore.DireccionEntrega();
            obDireccionEntrega = obGateway.buscarDireccionEntrega(obSolicitudGateway);

            try
            {
                Assert.IsNotNull(obDireccionEntrega.DireccionCompleta);
                Assert.AreEqual(direccion, obDireccionEntrega.DireccionCompleta.Trim());
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(obSolicitudGateway, obDireccionEntrega, fuente, respuestaExitosa, EnumMetodoWS.BusquedaDireccionEntrega);

            /*      CONSULTAR PEDIDO        */
            respuestaExitosa = true;
            SolicitudPedidoGateway obSolicitudPedido = new SolicitudPedidoGateway
            {
                TipoConsultaPedido      = RTGMCore.TipoConsultaPedido.RegistroPedido,
                //FechaCompromisoInicio   = DateTime.Now.Date,
                EstatusBoletin          = "BOLETIN",
                IDPedido                = 1
                //PedidoReferencia        = pedidoReferencia,
                //IDZona                  = 201
            };

            List<RTGMCore.Pedido> obPedidos = obGatewayPedido.buscarPedidos(obSolicitudPedido);

            try
            {
                Assert.AreEqual(cliente, obPedidos[0].IDDireccionEntrega);
            }
            catch(Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(obSolicitudPedido, obPedidos, fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);

            /*    ACTUALIZAR PEDIDO     */
            respuestaExitosa = true;

            //List<RTGMCore.Pedido> obPedidosActualizar = new List<RTGMCore.Pedido>();
            //obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo
            //{
            //    IDPedido            = obPedidos[0].IDPedido,
            //    IDZona              = obPedidos[0].IDZona,
            //    AnioPed             = obPedidos[0].AnioPed,
            //    Abono               = obPedidos[0].Total == null ? 0 : (decimal)obPedidos[0].Total,
            //    PedidoReferencia    = obPedidos[0].PedidoReferencia
            //});

            List<RTGMCore.Pedido> obPedidosActualizar = new List<RTGMCore.Pedido>();
            obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6162, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6162" });
            obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6163, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6163" });
            obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6164, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6164" });
            obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6165, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6165" });
            obPedidosActualizar.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6166, IDZona = 6, AnioPed = 2018, Abono = 200, PedidoReferencia = "6166" });

            SolicitudActualizarPedido obSolicitudActualizar = new SolicitudActualizarPedido
            {
                Fuente              = fuente,
                IDEmpresa           = 1,
                Pedidos             = obPedidosActualizar,
                TipoActualizacion   = RTGMCore.TipoActualizacion.Saldo,
                Usuario             = "ROPIMA"
            };

            List<RTGMCore.Pedido> obPedidosRespuesta = obGatewayActualizar.ActualizarPedido(obSolicitudActualizar);

            try
            {
                Assert.AreEqual(5, obPedidosRespuesta.Count);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(obSolicitudActualizar, obPedidosRespuesta, fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);
        }


    }
}

