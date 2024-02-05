using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public interface IMostrarMensajes
    {
        Task MostrarMensajeError(string mensaje);
        Task MostrarMensajeExitoso(string mensaje);
        Task<bool> Confirmacion(string titulo, string mensaje, string icono);
    }
}
