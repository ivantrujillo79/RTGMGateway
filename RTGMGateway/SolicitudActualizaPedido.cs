using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTGMGateway.RTGMCore;

namespace RTGMGateway
{
    public struct SolicitudActualizarPedido
    {
        public Fuente Fuente { get; set; }
        public int IDEmpresa { get; set; }
        public TipoActualizacion TipoActualizacion { get; set; }
        public bool Portatil { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public string Usuario { get; set; }
    }
}
