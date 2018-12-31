using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTGMGateway;

namespace pruebaWS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool respuestaExitosa = true;
            RTGMGateway.RTGMGateway objGateway = new RTGMGateway.RTGMGateway(Pruebas.Variables.GLOBAL_Modulo, Pruebas.Variables.GLOBAL_CadenaConexion);
            objGateway.URLServicio = Pruebas.Variables.GLOBAL_URLGateway;

            SolicitudGateway objRequest = new SolicitudGateway
            {
                IDCliente = null,
                Portatil = false,
                IDAutotanque = null,
                FechaConsulta = null,
                Nombre = "%Mariano%"

            };

            RTGMCore.DireccionEntrega objDireccionEntrega = objGateway.buscarDireccionEntrega(objRequest);

            try
            {
                /*Assert.IsNotNull(objDireccionEntrega);
                Assert.True(objDireccionEntrega.Success);*/
            }
            catch (Exception)
            {
                respuestaExitosa = false;
            }

            //Utilerias.Exportar(objRequest, objDireccionEntrega, objGateway.Fuente, respuestaExitosa, EnumMetodoWS.BusquedaDireccionEntrega);

        }
    }
}
