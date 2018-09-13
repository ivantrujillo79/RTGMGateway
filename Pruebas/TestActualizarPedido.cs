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
        private string _CadenaConexion = "Server=192.168.1.30;Database=sigametdevtb;User Id=ROPIMA;Password = ROPIMA9999;";
        private byte _Modulo = 1;
        private string _URL = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";

        // Actualizar estatus boletín
        [TestCase(1505, 1, 205, "BOLETINADO")]
        public void pruebaActualizaBoletin(int pedido, int empresa, int celula, string estatus)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = _URL;

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
        [TestCase(1, 1638, 201, 20)]
        public void pruebaActualizarSaldo(int empresa, int pedido, int zona, decimal abono)
        {
            bool respuestaExitosa = true;

            RTGMActualizarPedido objGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            objGateway.URLServicio = _URL;

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
        [TestCase(1638, 201, 1, 1, 49)]
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
                Importe                     = 61.2M,
                Impuesto                    = 0M,
                Precio                      = 10.2M,
                CantidadSurtida             = 6,
                Total                       = 61.2M,

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
            objGateway.URLServicio = _URL;

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
                ,FolioRemision          = 7654321
                ,IDAutotanque           = 303
                ,IDEmpresa              = 1
                ,IDFolioAtt             = 927565
                ,IDFormaPago            = 3
                ,IDTipoCargo            = 1
                ,IDTipoPedido           = 1
                ,IDTipoServicio         = 1
                ,Importe                = 61.2M
                ,Impuesto               = 0M
                ,SerieRemision          = "E"
                ,Total                  = 61.2M
                ,Saldo                  = 0
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

            RTGMActualizarPedido obGateway = new RTGMActualizarPedido(_Modulo, _CadenaConexion);
            obGateway.URLServicio = _URL;

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




