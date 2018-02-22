using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RTGMGateway
{
    [TestFixture]
    class TestRTGMGateway
    {
        [TestCase(502602197, "GMO PM ROSTICERIAS MI PAN SA DE CV")]
        [TestCase(502602198, "BLAS RAMIREZ LUNA")]
        [TestCase(null, "Venta al publico")]
        [TestCase(0, "Venta al publico")]
        public void pruebaRecuperaCliente(int Cliente, string Nombre)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway { Fuente = RTGMCore.Fuente.Sigamet, IDCliente = Cliente, IDEmpresa = 0, Portatil = false, IDAutotanque = 52 };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.Nombre.Trim(), Nombre);
        }
    }
}
