using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RTGMGateway;

namespace Pruebas
{
    [TestFixture]
    class TestActualizarPedido
    {
        // Actualizar estatus boletín
        [TestCase(1505, 1, 205, "BOLETINADO")]
        public void pruebaActualizaBoletin(int pedido, int empresa, int celula, string estatus)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            objGateway.URLServicio = Variables.GLOBAL_URLGateway;

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

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedidoBoletin);
        }

        // Actualizar saldo
        [TestCase(30002431, 201, 70, 1)]
        public void pruebaActualizarSaldo(int pedido, int zona, decimal abono, int empresa)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            objGateway.URLServicio = Variables.GLOBAL_URLGateway;

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMSaldo
            {
                IDEmpresa               = empresa
                ,IDPedido               = pedido
                ,PedidoReferencia       = Convert.ToString(pedido)
                ,IDZona                 = zona
                ,Abono                  = abono
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

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedidoSaldo);

        }

        // Liquidar pedido
        [TestCase(30002431, 201, 2, 1, 502763311)]
        public void pruebaLiquidacion(int? pedido, int zona, int ruta, int producto, int direccionEntrega)
        {
            bool respuestaExitosa = true;
            List<RTGMCore.DetallePedido> listaDetallePedidos = new List<RTGMCore.DetallePedido>();
            RTGMCore.RutaCRMDatos obRuta = new RTGMCore.RutaCRMDatos { IDRuta = ruta };
            RTGMCore.Producto obProducto = new RTGMCore.Producto { IDProducto = producto };
            RTGMCore.DetallePedido obDetalle = new RTGMCore.DetallePedido
            {
                Producto                    = obProducto,
                DescuentoAplicado           = 0,
                Importe                     = 258.6207M,
                Impuesto                    = 41.3793M,
                Precio                      = 9.96M,
                CantidadSurtida             = 30.05M,
                Total                       = 300M,

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

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            objGateway.URLServicio = Variables.GLOBAL_URLGateway;

            List<RTGMCore.Pedido> lstPedido = new List<RTGMCore.Pedido>();
            lstPedido.Add(new RTGMCore.PedidoCRMDatos
            {
                IDPedido                = pedido
                ,IDZona                 = zona
                ,RutaSuministro         = obRuta
                ,DetallePedido          = listaDetallePedidos
                ,IDDireccionEntrega     = direccionEntrega
                ,AnioAtt                = 2018
                ,FSuministro            = DateTime.Now
                ,FolioRemision          = 16182
                ,IDAutotanque           = 303
                ,IDEmpresa              = 1
                ,IDFolioAtt             = 927565
                ,IDFormaPago            = 4
                ,IDTipoCargo            = 1
                ,IDTipoPedido           = 1
                ,IDTipoServicio         = 1
                ,Importe                = 258.6207M
                ,Impuesto               = 41.3793M
                ,SerieRemision          = "E"
                ,Total                  = 300M
                ,Saldo                  = 300M
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

            Utilerias.Exportar(Solicitud, ListaRespuesta, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedidoLiquidacion);
        }

        // Cancelar liquidación
        [TestCase(1584, 2018, 303, 47697, 17327695, 14, "E")]
        public void pruebaCancelarLiquidacion(int parPedido, int parAnioAtt, int parAutotanque,
                                                int parFolioAtt, int parFolioRemision, int parRuta,
                                                string parSerieRemision)
        {
            bool respuestaExitosa = true;
            RTGMCore.RutaCRMDatos obRuta = new RTGMCore.RutaCRMDatos { IDRuta = parRuta };
            List<RTGMCore.Pedido> lsPedidos = new List<RTGMCore.Pedido>();

            RTGMActualizarPedido obGateway = new RTGMActualizarPedido(Variables.GLOBAL_Modulo, Variables.GLOBAL_CadenaConexion);
            obGateway.URLServicio = Variables.GLOBAL_URLGateway;

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

            Utilerias.Exportar(obSolicitud, lsRespuesta, obGateway.Fuente, respuestaExitosa, EnumMetodoWS.ActualizarPedidoCancelarLiquidacion);
        }
    }
}




