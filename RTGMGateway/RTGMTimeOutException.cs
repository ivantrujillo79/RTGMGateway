using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTGMGateway
{
    public class RTGMTimeoutException : Exception
    {
        public string Mensaje { get; set; }

        public RTGMTimeoutException() { }

        public RTGMTimeoutException(string mensaje)
        {
            this.Mensaje = mensaje;
        }
    }
}
