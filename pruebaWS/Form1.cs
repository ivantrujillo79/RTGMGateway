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

            int IDCLienteText;
            if (Int32.TryParse(textBox1.Text.Trim(), out IDCLienteText))
            {
                RTGMGateway.RTGMGateway objGateway = new RTGMGateway.RTGMGateway(Pruebas.Variables.GLOBAL_Modulo, Pruebas.Variables.GLOBAL_CadenaConexion, RTGMCore.Fuente.Sigamet);
                objGateway.URLServicio = Pruebas.Variables.GLOBAL_URLGateway;

                SolicitudGateway objRequest = new SolicitudGateway
                {
                    IDCliente = IDCLienteText,
                    Portatil = false,
                    IDAutotanque = null,
                    FechaConsulta = null,
                    Nombre = null,
                    Fuente = RTGMCore.Fuente.Sigamet
                };

                RTGMCore.DireccionEntrega objDireccionEntrega = objGateway.buscarDireccionEntrega(objRequest);



                RTGMGateway.RTGMGateway objGatewayCRM = new RTGMGateway.RTGMGateway(Pruebas.Variables.GLOBAL_Modulo, Pruebas.Variables.GLOBAL_CadenaConexion, RTGMCore.Fuente.CRM);
                objGatewayCRM.URLServicio = Pruebas.Variables.GLOBAL_URLGateway;

                SolicitudGateway objRequestCRM = new SolicitudGateway
                {
                    IDCliente = IDCLienteText,
                    Portatil = false,
                    IDAutotanque = null,
                    FechaConsulta = null,
                    Nombre = null,
                    Fuente = RTGMCore.Fuente.CRM
                };

                RTGMCore.DireccionEntrega objDireccionEntregaCRM = objGateway.buscarDireccionEntrega(objRequestCRM);

                MessageBox.Show("Consultas a SIGAMET Y CRM finalizadas para el cliente " + textBox1.Text);
            }
            else
            {
                MessageBox.Show("Indica un id de cliente");
            }


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
