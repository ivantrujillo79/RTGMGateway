using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace RTGMGateway
{
    class DAO
    {
        private string _CadenaConexion;
        private byte _Modulo;
        public DAO(byte Modulo, string CadenaConexion)
        {
            _Modulo = Modulo;
            _CadenaConexion = CadenaConexion;
        }

        public RTGMCore.Fuente consultarFuente(byte Modulo, string CadenaConexion)
        {
            string valorParametro = "";
            RTGMCore.Fuente fuenteRetorno = new RTGMCore.Fuente();
            try
            {
                SqlConnection cnn = new SqlConnection(CadenaConexion);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 ISNULL(VALOR,'') FROM PARAMETRO WHERE MODULO = " + Modulo + " AND PARAMETRO = 'FUENTECRM'; ", cnn);
                cmd.CommandType = System.Data.CommandType.Text;
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                }
                valorParametro = cmd.ExecuteScalar().ToString();
                if (cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
                switch (valorParametro.ToUpper().Trim())
                {
                    case "CRM":
                        fuenteRetorno = RTGMCore.Fuente.CRM;
                        break;
                    case "SIGAMET":
                        fuenteRetorno = RTGMCore.Fuente.Sigamet;
                        break;
                    case "SIGAMETPORTATIL":
                        fuenteRetorno = RTGMCore.Fuente.SigametPortatil;
                        break;
                    case "":
                        fuenteRetorno = RTGMCore.Fuente.Sigamet;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fuenteRetorno;
        }

        public DataRow consultarCorporativoSucursal(byte Modulo, string CadenaConexion)
        {
            DataTable tablaParametros = new DataTable("Parametros");

            DataColumn Corporativo = new DataColumn();
            Corporativo.DataType = System.Type.GetType("System.Int32");
            Corporativo.ColumnName = "Corporativo";
            tablaParametros.Columns.Add(Corporativo);

            DataColumn Sucursal = new DataColumn();
            Sucursal.DataType = System.Type.GetType("System.Int32");
            Sucursal.ColumnName = "Sucursal";
            tablaParametros.Columns.Add(Sucursal);

            DataRow drParametros = tablaParametros.NewRow();

            try
            {
                SqlConnection cnn = new SqlConnection(CadenaConexion);
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 ISNULL(CORPORATIVO,1) AS CORPORATIVO, ISNULL(SUCURSAL,1) AS SUCURSAL FROM PARAMETRO WHERE MODULO = " + Modulo + " AND PARAMETRO = 'FUENTECRM'; ", cnn);
                cmd.CommandType = System.Data.CommandType.Text;
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    drParametros["Corporativo"] = Convert.ToByte(dr[0]);
                    drParametros["Sucursal"] = Convert.ToByte(dr[1]);
                }

                if (cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return drParametros;
        }
    
    }
}
