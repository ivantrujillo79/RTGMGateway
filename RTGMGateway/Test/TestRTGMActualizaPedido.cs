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
                Assert.True(ListaRespuesta[0].Success);
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
                Assert.True(ListaRespuesta[0].Success);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);

        }

        [TestCase(1584, 201, 14, 1)]
        public void pruebaLiquidacion(int? pedido, int zona, int ruta, int producto)
        {
            bool respuestaExitosa = true;
            List<RTGMCore.DetallePedido> listaDetallePedidos = new List<RTGMCore.DetallePedido>();
            RTGMCore.RutaCRMDatos obRuta = new RTGMCore.RutaCRMDatos { IDRuta = ruta };
            RTGMCore.Producto obProducto = new RTGMCore.Producto { IDProducto = producto };
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
                IDPedido                = pedido
                ,IDZona                  = zona
                ,RutaSuministro         = obRuta
                ,DetallePedido          = listaDetallePedidos
                ,IDDireccionEntrega     = 37
                ,AnioAtt                = 2018
                ,FSuministro            = DateTime.Now
                ,FolioRemision          = 17327695
                ,IDAutotanque           = 303
                ,IDEmpresa              = 1
                ,IDFolioAtt             = 47697
                ,IDFormaPago            = 5
                ,IDTipoCargo            = 1
                ,IDTipoPedido           = 1
                ,IDTipoServicio         = 1
                ,Importe                = 291.25M
                ,Impuesto               = 0M
                ,SerieRemision          = "E"
                ,Total                  = 290.25M
            });

            SolicitudActualizarPedido Solicitud = new SolicitudActualizarPedido
            {
                Pedidos = lstPedido,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Liquidacion,
                Usuario = "ROPIMA"
            };

            List<RTGMCore.Pedido> ListaRespuesta = objGateway.ActualizarPedido(Solicitud);

            try
            {
                Assert.IsNotNull(ListaRespuesta[0]);
                Assert.True(ListaRespuesta[0].Success);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);
        }

        [TestCase(1584, 2018, 303, 47697, 17327695, 14, "E")]
        public void pruebaCancelarLiquidacion(int parPedido, int parAnioAtt, int parAutotanque,
                                                int parFolioAtt, int parFolioRemision, int parRuta,
                                                string parSerieRemision)
        {
            bool respuestaExitosa = true;
            RTGMCore.RutaCRMDatos obRuta = new RTGMCore.RutaCRMDatos { IDRuta = parRuta };
            List<RTGMCore.Pedido> lsPedidos = new List<RTGMCore.Pedido>();

            RTGMActualizarPedido obGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            obGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

            lsPedidos.Add(new RTGMCore.PedidoCRMDatos
            {
                IDPedido            = parPedido,
                AnioAtt             = parAnioAtt,
                IDAutotanque        = parAutotanque,
                IDFolioAtt          = parFolioAtt,
                FolioRemision       = parFolioRemision,
                RutaSuministro      = obRuta,
                SerieRemision       = parSerieRemision
            });

            SolicitudActualizarPedido obSolicitud = new SolicitudActualizarPedido
            {
                Pedidos = lsPedidos,
                Portatil = false,
                TipoActualizacion = RTGMCore.TipoActualizacion.Cancelacion
            };

            List<RTGMCore.Pedido> lsRespuesta = new List<RTGMCore.Pedido>();

            try
            {
                lsRespuesta = obGateway.ActualizarPedido(obSolicitud);
                Assert.IsNotNull(lsRespuesta[0]);
                Assert.True(lsRespuesta[0].Success);
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            Utilerias.Exportar(obSolicitud, lsRespuesta, obGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedido);
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




