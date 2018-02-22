using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTGMGateway
{
    public struct SolicitudGateway
    {
        #region VARIABLES
        private RTGMCore.Fuente fuente;

        private System.Nullable<int> idCliente;

        private int idEmpresa;

        private System.Nullable<int> sucursal;

        private string telefono;

        private string calleNombre;

        private string coloniaNombre;

        private string municipioNombre;

        private string nombre;

        private System.Nullable<int> numeroExterior;

        private string numeroInterior;

        private System.Nullable<int> tipoServicio;

        private System.Nullable<int> zona;

        private System.Nullable<int> zonaEconomica;

        private int zonaLecturista;

        private bool portatil;

        private string usuario;

        private string referencia;

        private System.Nullable<int> idAutotanque;

        private System.Nullable<int> ruta;

        private System.Nullable<System.DateTime> fechaConsulta;
        #endregion

        #region PROPIEDADES
        public RTGMCore.Fuente Fuente
        {
            get
            {
                return fuente;
            }
            set
            {
                fuente = value;
            }
        }

        public System.Nullable<int> IDCliente
        {
            get
            {
                return idCliente;
            }
            set
            {
                idCliente = value;
            }
        }

        public int IDEmpresa
        {
            get
            {
                return idEmpresa;
            }
            set
            {
                idEmpresa = value;
            }
        }

        public System.Nullable<int> Sucursal
        {
            get
            {
                return sucursal;
            }
            set
            {
                sucursal = value;
            }
        }

        public string Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
            }
        }

        public string CalleNombre
        {
            get
            {
                return calleNombre;
            }
            set
            {
                calleNombre = value;
            }
        }

        public string ColoniaNombre
        {
            get
            {
                return coloniaNombre;
            }
            set
            {
                coloniaNombre = value;
            }
        }

        public string MunicipioNombre
        {
            get
            {
                return municipioNombre;
            }
            set
            {
                municipioNombre = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public System.Nullable<int> NumeroExterior
        {
            get
            {
                return numeroExterior;
            }
            set
            {
                numeroExterior = value;
            }
        }

        public string NumeroInterior
        {
            get
            {
                return numeroInterior;
            }
            set
            {
                numeroInterior = value;
            }
        }

        public System.Nullable<int> TipoServicio
        {
            get
            {
                return tipoServicio;
            }
            set
            {
                tipoServicio = value;
            }
        }

        public System.Nullable<int> Zona
        {
            get
            {
                return zona;
            }
            set
            {
                zona = value;
            }
        }

        public System.Nullable<int> ZonaEconomica
        {
            get
            {
                return zonaEconomica;
            }
            set
            {
                zonaEconomica = value;
            }
        }

        public int ZonaLecturista
        {
            get
            {
                return zonaLecturista;
            }
            set
            {
                zonaLecturista = value;
            }
        }

        public bool Portatil
        {
            get
            {
                return portatil;
            }
            set
            {
                portatil = value;
            }
        }

        public string Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        public string Referencia
        {
            get
            {
                return referencia;
            }
            set
            {
                referencia = value;
            }
        }

        public System.Nullable<int> IDAutotanque
        {
            get
            {
                return idAutotanque;
            }
            set
            {
                idAutotanque = value;
            }
        }

        public System.Nullable<int> Ruta
        {
            get
            {
                return ruta;
            }
            set
            {
                ruta = value;
            }
        }

        public System.Nullable<System.DateTime> FechaConsulta
        {
            get
            {
                return fechaConsulta;
            }
            set
            {
                fechaConsulta = value;
            }
        }
        #endregion

    }//end SolicitudGateway
}
