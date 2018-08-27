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

        [TestCase(1505, 1, 205, "BOLETINADO")]
        public void pruebaActualizaBoletin(int pedido, int empresa, int celula, string estatus)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMDatos
            {
                IDPedido = pedido
                ,IDEmpresa = empresa
                ,IDZona = celula
                ,EstatusBoletin = estatus
                //,AnioPed = 2018
            });
                        
            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
            {
                Pedidos = lstPedido,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Boletin,
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

        [TestCase(1, 2, "2", 205, 20)]
        public void pruebaActualizarSaldo(int empresa, int pedido, string pedidoReferencia, int zona, decimal abono)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo
            {
                IDEmpresa = empresa
                ,IDPedido = pedido
                ,PedidoReferencia = pedidoReferencia
                ,IDZona = zona
                ,Abono = abono
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

        [TestCase(1505, 205)]
        public void pruebaLiquidacion(int? pedido, int zona)
        {
            bool respuestaExitosa = true;
            List<RTGMCore.DetallePedido> listaDetallePedidos = new List<RTGMCore.DetallePedido>();
            RTGMCore.RutaCRMDatos obRuta = new RTGMCore.RutaCRMDatos { IDRuta = 616 };
            RTGMCore.Producto obProducto = new RTGMCore.Producto { IDProducto = 1 };
            RTGMCore.DetallePedido obDetalle = new RTGMCore.DetallePedido
            {
                Producto                    = obProducto,
                DescuentoAplicado           = 0,
                Importe                     = 290.25M,
                Impuesto                    = 0M,
                Precio                      = 10.75M,
                CantidadSurtida             = 27,
                Total                       = 290.25M,

                CantidadLectura             = 0,
                CantidadLecturaAnterior     = 0,
                CantidadSolicitada          = 0,
                DescuentoAplicable          = 0,
                DiferenciaDeLecturas        = 0,
                IDDetallePedido             = 0,
                IDPedido                    = 0,
                ImpuestoAplicable           = 0,
                PorcentajeTanque            = 0,
                PrecioAplicable             = 0,
                RedondeoAnterior            = 0,
                TotalAplicable              = 0
            };

            listaDetallePedidos.Add(obDetalle);

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMDatos
            {
                //IDPedido                = pedido
                IDZona                  = zona
                ,RutaSuministro         = obRuta
                ,DetallePedido          = listaDetallePedidos
                ,IDDireccionEntrega     = 14
                ,AnioAtt                = 2018
                ,FSuministro            = DateTime.Now
                ,FolioRemision          = 17327695
                ,IDAutotanque           = 303
                ,IDEmpresa              = 0
                ,IDFolioAtt             = 47697
                ,IDFormaPago            = 5
                ,IDTipoCargo            = 1
                ,IDTipoPedido           = 3
                ,IDTipoServicio         = 1
                ,Importe                = 291.25M
                ,Impuesto               = 100M
                ,SerieRemision          = "E"
                ,Total                  = 291.25M
            });

            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
            {
                Pedidos = lstPedido,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Liquidacion,
                Usuario = "JEBANA"
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




