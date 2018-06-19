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
        //[TestCase(502602197, "GMO PM ROSTICERIAS MI PAN SA DE CV")]
        //[TestCase(502602198, "BLAS RAMIREZ LUNA")]
        //[TestCase(null, "Venta al publico")]
        //[TestCase(0, "Venta al publico")]
        [TestCase(1, "")]
        public void pruebaRecuperaCliente(int Cliente, string Nombre)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                //Fuente = RTGMCore.Fuente.Sigamet,
                Fuente = RTGMCore.Fuente.CRM,
                IDCliente = Cliente,
                IDEmpresa = 1,
                Portatil = false,
                IDAutotanque = 52
            };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.Nombre.Trim(), Nombre);
        }

        [TestCase(1000578, 0, "57875000", "VIA MORELOS KM 14.5", "GEO INTERNACIONAL SA DE CV")]
        //[TestCase(6, 0, "56921243", "ANILLO PERIFERICO", "MARIA ELIZABETH URIBE GONZALEZ")]
        //[TestCase(3, 0, "56830138", "PRIV DE LOS CEDROS", "SIN EMPRESA")]
        //[TestCase(9, 0, " ", "", "SIN EMPRESA")]
        //[TestCase(16, 0, "", "!", "SIN EMPRESA")]
        public void pruebaRecuperaDatosFiscales(int Cliente, int Empresa, string Telefono, string Calle, string RazonSocial)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = 52,
                Telefono = Telefono,
                CalleNombre = Calle
            };
            
            RTGMCore.DatosFiscales objDatosFiscales = objGateway.buscarDatoFiscal(objRequest);

            Assert.AreEqual(objDatosFiscales.RazonSocial.Trim(), RazonSocial);
        }

        //[TestCase(8, 0, "55724061", "IGNACIO ARTEAGA", "SAN ANDRES", "0.00000000")]
        //[TestCase(9, 0, "", "LUIS G URBINA", "", "19.51694167")]
        //[TestCase(48, 0, "53978857", "", "", "0.00000000")]
        //[TestCase(0, 0, "", "", "SIN COLONIA", "19.51634360")]
        //[TestCase(null, 0, "", "HERRADURA", "", "19.51634360")]
        public void pruebaRecuperaGeorreferencia(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Latitud)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = 52,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia
            };

            RTGMCore.Georreferencia objGeorreferencia = objGateway.buscarGeorreferencia(objRequest);

            Assert.AreEqual(objGeorreferencia.Latitud, decimal.Parse(Latitud));
        }

        //[TestCase(57990055, 0, "57043430", "SERICULTURA", "VEINTE DE NOVIEMBRE 2DO TRAMO", "VENUSTIANO CARRANZA",
        //    "Sin Crédito")]
        //[TestCase(60012802, null, "30987000", "TLATILCO", "TLATILCO", "AZCAPOTZALCO", "Crédito Suspendido")]
        //[TestCase(60038863, 0, " ", " ", " ", " ", "Jurídico")]
        //[TestCase(60059055, 0, "/", "AV HENRY FORD", "ZONA INDUSTRIAL CUAUTITLAN IZCALLI", "CUAUTITLAN IZCALLI",
        //    "Morosa")]
        //[TestCase(0, null, "~", "*", "^", "`", "Sin Crédito")]
        public void pruebaRecuperaCondicionesCredito(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Municipio, string CarteraDescripcion)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = 52,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio
            };
            
            RTGMCore.CondicionesCredito objCondicionesCredito = objGateway.buscarCondicionesCredito(objRequest);
            
            Assert.AreEqual(objCondicionesCredito.CarteraDescripcion.Trim(), CarteraDescripcion);
        }

        //[TestCase(502246858, 0, "S/Teléfono", "CINE MEXICANO", "LOMAS ESTRELLA 1A SECCION", "IZTAPALAPA",
        //    null, 3314)]
        //[TestCase(502246860, null, "57355244", "8465/*-%", "DEL SOL", "NEZAHUALCOYOTL", 0, 3072)]
        //[TestCase(502246866, 0, "55586108       ", "XINANTECATL", "LOS VOLCANES", " ", 1, 0)]
        //[TestCase(502246911, 0, "'", "SECCION", "LOS HEROES COACALCO", "COACALCO", -8, 3593)]
        //[TestCase(null, 0, "-1", "", "", "", 7, 0)]
        public void pruebaRecuperaEmpleado(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Municipio, int Sucursal, int Empleado)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = 52,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal
            };
            
            RTGMCore.Empleado objSupervisorComercial = objGateway.buscarEmpleado(objRequest);

            Assert.AreEqual(objSupervisorComercial.IDEmpleado, Empleado);
        }

        //[TestCase(200023, 0, "55439234       ", "NEBRASKA", "NAPOLES", "BENITO JUAREZ", 0, "10.35")]
        public void pruebaRecuperaPrecio(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Municipio, int Sucursal, string Precio)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = 52,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal
            };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.PrecioPorDefecto.ValorPrecio, decimal.Parse(Precio));
        }

        //[TestCase(666013, 0, "5700002        ", "LUIS G SADA", "BENITO JUAREZ NORTE XALOSTOC", "ECATEPEC", -23,
        //    52, "+03.0")]
        //[TestCase(502298378, 0, "52473233", "VALLE DE BRAVO", "LOMA DE VALLESCONDIDO", "~", 0, 7, "+03.0")]
        //[TestCase(502638831, 0, "", "OTUMBA", " ", "ATIZAPAN DE ZARAGOZA", 0, 21, "+03.0")]
        //[TestCase(803218, 0, " ", "NEZAHUALCOYOTL", "AGUA AZUL", "NEZAHUALCOYOTL", 0, 295, "+25.0")]
        //[TestCase(1000075, null, "00000000", "SIRACUSA", "LOMAS ESTRELLA 2A SECCION", "IZTAPALAPA", 0, 295, "+03.0")]
        public void pruebaRecuperaConfiguracionSuministro(int Cliente, int Empresa, string Telefono, string Calle, 
            string Colonia, string Municipio, int Sucursal, int Autotanque, string Ajustes)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
            };
            
            RTGMCore.ConfiguracionSuministro objConfiguracionSuministro = objGateway.buscarConfiguracionSuministro(objRequest);

            Assert.AreEqual(objConfiguracionSuministro.Ajustes.Trim(), Ajustes);
        }

        //[TestCase(3700003, 0, "00000000", "LUIS G SADA", "BENITO JUAREZ NORTE XALOSTOC", "ECATEPEC", 0, 52,
        //    "JAVIER TORRES", 202)]
        //[TestCase(3800009, null, "53800009  ", "LUIS G SADA", "INDUSTRIAL XALOSTOC", "ECATEPEC", 0, 21,
        //    "", 208)]
        //[TestCase(3990001, null, "53433199", "VIA LACTEA", "JARDINES DE SATELITE", "NAUCALPAN", 0, -1,
        //    " ", 206)]
        //[TestCase(4001004, null, "55955743", "CDA DE AHILES NUM 8", "JARDINES DEL PEDREGAL", "ALVARO OBREGON",
        //    0, 52, "\\", 208)]
        //[TestCase(4001039, null, "Número incorrecto", "ALBORADA", "PARQUE DEL PEDREGAL", "TLALPAN", -6, -6,
        //    "FRANCISCO DURAZA", 209)]
        public void pruebaRecuperaZona(int Cliente, int Empresa, string Telefono, string Calle, string Colonia, 
            string Municipio, int Sucursal, int Autotanque, string NombreCliente, int NumeroZona)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
                Nombre = NombreCliente
            };
            
            RTGMCore.Zona objZona = objGateway.buscarZona(objRequest);
            
            Assert.AreEqual(objZona.NumeroZona, NumeroZona);
        }

        [TestCase(6017, 0, "", "", "", "", 0, 52, "", 154, 16)]
        [TestCase(6018, 0, "", "", "", "", 0, 21, "", 0, 322)]
        [TestCase(6112, 0, "", "", "", "", 0, -9, "", -9, 24)]
        [TestCase(6308, 0, "", "", "", "", 0, 0, "", 0, 201)]
        [TestCase(6399, 0, "", "", "", "", 0, 0, "", -14, 16)]
        public void pruebaRecuperaRuta(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Municipio, int Sucursal, int Autotanque, string NombreCliente, int NumeroExterior,
            int NumeroRuta)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
                Nombre = NombreCliente,
                NumeroExterior = NumeroExterior
            };
            
            RTGMCore.Ruta objRuta = objGateway.buscarRuta(objRequest);
            
            Assert.AreEqual(objRuta.NumeroRuta, NumeroRuta);
        }

        //[TestCase(46020, null, "57942437       ", "", "", "", 0, 52, "", 181, null, 0)]
        //[TestCase(46270, null, "55784876       ", "", "", "", 0, null, "", 0, null, 0)]
        //[TestCase(46433, null, "54254841       ", "", "", "", 5000, 0, "", 6, "0", 0)]
        //[TestCase(50023263, null, "5760-4438      ", "", "", "", 0, -261, "", null, null, 0)]
        //[TestCase(50555, null, "00000000       ", "", "", "", 6, null, "", 14, "14", 0)]
        public void pruebaRecuperaZonaEconomica(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Municipio, int Sucursal, int Autotanque, string NombreCliente, int NumeroExterior, 
            string NumeroInterior, int IDZona)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
                Nombre = NombreCliente,
                NumeroExterior = NumeroExterior,
                NumeroInterior = NumeroInterior
            };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.ZonaEconomica.IDZonaEconomomica, IDZona);
        }

        //[TestCase(9294880, null, "", "CALLE 11", "", "", 0, 52, "PARTICULAR", 0, "MZ 14 LT 31 A", null, null)]
        //[TestCase(10037200, 0, "", "", "", "", 0, 0, "", 0, "", null, false)]
        //[TestCase(10075301, 0, "53937362", "", "", "", 0, 0, "", 8, "", null, true)]
        //[TestCase(60091771, 0, "6851888", "", "", "", 0, 0, "", 0, "", null, true)]
        //[TestCase(20010129, 0, "55262595", "CONSTANCIA", "MORELOS", "CUAUHTEMOC", 1, 52,
        //    "SILVIA CASARRUBIAS FLORES", 95, "10", 1, false)]
        public void pruebaRecuperaProgramacionSuministro(int Cliente, int Empresa, string Telefono, string Calle, 
            string Colonia, string Municipio, int Sucursal, int Autotanque, string NombreCliente, 
            int NumeroExterior, string NumeroInterior, int TipoServicio, bool ProgramacionActiva)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
                Nombre = NombreCliente,
                NumeroExterior = NumeroExterior,
                NumeroInterior = NumeroInterior,
                TipoServicio = TipoServicio
            };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.ProgramacionSuministro.ProgramacionActiva, ProgramacionActiva);
        }

        //[TestCase(9148467, 0, "57312168", "LA MALAGUEÑA", "{", "$", 0, 0, "CARMEN MEZA", 27, "", null, 1, 6)]
        //[TestCase(9191322, 0, "-1", "-1", "-1", "-1", -1, -1, " ", -1, "-1", null, 209, 54)]
        //[TestCase(40013692, 0, "00000000", "JOAQUIN PESADO", "OBRERA", "CUAUHTEMOC", null, null, "HOTEL VISTA ALEGRE",
        //    13, "", null, null, 4)]
        //[TestCase(60173789, 0, "", "", "", "", 1, -1, "-", 52, "", 0, 205, 12)]
        //[TestCase(60173924, 0, null, "/", "<", "¨", 0, 0, "ñ", -15, "\n", -2, -11, 16)]
        public void pruebaRecuperaRamoCliente(int Cliente, int Empresa, string Telefono, string Calle, string Colonia, 
            string Municipio, int Sucursal, int Autotanque, string NombreCliente, int NumeroExterior, 
            string NumeroInterior, int TipoServicio, int Zona, int IDRamo)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
                Nombre = NombreCliente,
                NumeroExterior = NumeroExterior,
                NumeroInterior = NumeroInterior,
                TipoServicio = TipoServicio,
                Zona = Zona
            };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.Ramo.IDRamoCliente, IDRamo);
        }

        //[TestCase(1000133, 0, "00000000", "", "", "", null, null, "", 0, "", null, null, 0, 2)]
        //[TestCase(1008147, 0, "", "", "", "", 0, 52, "JOSE LUIS VAZQUEZ", 0, "MZ 5 LT 8", 0, 0, 6, 3)]
        //[TestCase(1013912, 0, null, "VALLE DE SANTA MARIA", "VILLAS DE CUAUTITLAN", "0", 0, 52, "ROSALIA GARCIA HUERTA",
        //    0, "MZ5 LT5 CASA 5B1                                  ", 1, 1, 2, 1)]
        //[TestCase(60173798, null, "3170764        ", " ", " ", "CUAUTITLAN IZCALLI", 0, 14,
        //    "DANONE DE MEXICO SA DE CV *GVO BAZ*", 16, "B", null, null, null, 3)]
        //[TestCase(100024079, null, "", "AND 4 DE CHAPULTEPEC", "", "", null, null, "HERMILO  ROJAS VARGAS",
        //    null, "", null, null, null, 4)]
        public void pruebaRecuperaTipoCliente(int Cliente, int Empresa, string Telefono, string Calle, string Colonia,
            string Municipio, int Sucursal, int Autotanque, string NombreCliente, int NumeroExterior,
            string NumeroInterior, int TipoServicio, int Zona, int Ruta, int TipoCliente)
        {
            RTGMGateway objGateway = new RTGMGateway();
            objGateway.URLServicio = @"http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc";
            SolicitudGateway objRequest = new SolicitudGateway
            {
                Fuente = RTGMCore.Fuente.Sigamet,
                IDCliente = Cliente,
                IDEmpresa = Empresa,
                Portatil = false,
                IDAutotanque = Autotanque,
                Telefono = Telefono,
                CalleNombre = Calle,
                ColoniaNombre = Colonia,
                MunicipioNombre = Municipio,
                Sucursal = Sucursal,
                Nombre = NombreCliente,
                NumeroExterior = NumeroExterior,
                NumeroInterior = NumeroInterior,
                TipoServicio = TipoServicio,
                Zona = Zona,
                Ruta = Ruta
            };

            RTGMCore.DireccionEntrega objDireccionEntega = objGateway.buscarDireccionEntrega(objRequest);

            Assert.AreEqual(objDireccionEntega.TipoCliente.IDTipoCliente, TipoCliente);
        }        
    }// end TestRTGMGateway
}
