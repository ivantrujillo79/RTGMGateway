using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RTGMGateway;

namespace Pruebas
{
    [TestFixture]
    class TestConsultarPedidos
    {
        //[TestCase("201820147549",502627606, RTGMCore.Fuente.Sigamet)]
        [TestCase(30002431)]
        public void pruebaRecuperaPorIDPedido(int Pedido)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            objPedidoGateway.URLServicio = Variables.GLOBAL_URLGateway;
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
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(objRequest, objPedido, objPedidoGateway.Fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);

        }

        [TestCase(201)]
        public void pruebaRecuperaPorIDZona(int parZona)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            objPedidoGateway.URLServicio = Variables.GLOBAL_URLGateway;
            List<RTGMCore.Pedido> objPedido = new List<RTGMCore.Pedido>();

            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.RegistroPedido
                ,
                IDZona = parZona
            };

            try
            {
                objPedido = objPedidoGateway.buscarPedidos(objRequest);
                Assert.IsNotNull(objPedido[0]);
                Assert.True(objPedido[0].Success);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(objRequest, objPedido, objPedidoGateway.Fuente, respuestaExitosa, EnumMetodoWS.ConsultarPedidos);
        }

        //  Servicios técnicos
        [TestCase(201)]
        public void recuperaServiciosTecnicos(int parZona)
        {
            bool respuestaExitosa = true;
            RTGMPedidoGateway objPedidoGateway = new RTGMPedidoGateway(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            objPedidoGateway.URLServicio = Variables.GLOBAL_URLGateway;
            List<RTGMCore.Pedido> objPedido = new List<RTGMCore.Pedido>();

            SolicitudPedidoGateway objRequest = new SolicitudPedidoGateway
            {
                TipoConsultaPedido = RTGMCore.TipoConsultaPedido.ServiciosTecnicos
                ,EstatusPedidoDescripcion = "ACTIVO"
                ,FechaCompromisoInicio = new DateTime(2018, 9, 7)
                ,FechaCompromisoFin = new DateTime(2018, 9, 7, 11, 59, 59)
                ,IDZona = parZona
            };

            try
            {
                objPedido = objPedidoGateway.buscarPedidos(objRequest);
                Assert.IsNotNull(objPedido[0]);
                Assert.IsNull(objPedido[0].Message);
                //Assert.True(objPedido[0].Success);
            }
            catch (Exception)
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
            RTGMPedidoGateway obPedidoGateway = new RTGMPedidoGateway(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            obPedidoGateway.URLServicio = Variables.GLOBAL_URLGateway;
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

    }
}
