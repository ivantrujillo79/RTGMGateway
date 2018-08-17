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
        private string _CadenaConexion = "Server=192.168.1.30;Database=sigametdevtb;User Id=ROPIMA;Password = ROPIMA9999;";
        private byte _Modulo = 1;

        [TestCase(RTGMCore.Fuente.Sigamet)]
        public void pruebaActualizaBoletin(RTGMCore.Fuente FuentePrueba)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMDatos
            {
                IDPedido = 1214
                ,IDEmpresa = 1
                ,IDZona = 203
                ,EstatusBoletin = "BOLETINADO"
                //,AnioPed = 2018
            });
                        
            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
            {
                Pedidos = lstPedido,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Boletin,
                //Usuario = "ROPIMA"
            };

            List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);

            try
            {
                Assert.IsNotNull(ListaRespuesta[0]);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);

        }

        [TestCase(1, 1, "1", 205, -1.5)]
        public void pruebaActualizarSaldo(int empresa, int pedido, string pedidoReferencia, int zona, decimal abono)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo
            //lstPedido.Add(new RTGMCore.PedidoCRMDatos
            {
                IDEmpresa = empresa
                ,IDPedido = pedido
                ,PedidoReferencia = pedidoReferencia
                ,IDZona = zona
                ,Abono = abono
                //,AnioPed = 2018
            });

            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
            {
                Pedidos = lstPedido,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Saldo,
            };

            List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);

            try
            {
                Assert.IsNotNull(ListaRespuesta[0]);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);

        }
    }

    class TestRTGMActualizaPedidoBoletin
    {
        private string _CadenaConexion = "Server=192.168.1.30;Database=sigametdevtb;User Id=ROPIMA;Password = ROPIMA9999;";
        private byte _Modulo = 1;

        [TestCase]
        public void pruebaActualizaPedidoBoletin()
        {
            bool respuestaExitosa = true;
            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6162, IDZona = 6, AnioPed = 2018, PedidoReferencia = "6162" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6163, IDZona = 6, AnioPed = 2018, PedidoReferencia = "6163" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6164, IDZona = 6, AnioPed = 2018, PedidoReferencia = "6164" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6165, IDZona = 6, AnioPed = 2018, PedidoReferencia = "6165" });
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo { IDPedido = 6166, IDZona = 6, AnioPed = 2018, PedidoReferencia = "6166" });

            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
            {
                Pedidos = lstPedido,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Boletin,
                Usuario = "ROPIMA"
            };

            List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);

            try
            {
                Assert.IsNotNull(ListaRespuesta[0]);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);

        }




    }



}
