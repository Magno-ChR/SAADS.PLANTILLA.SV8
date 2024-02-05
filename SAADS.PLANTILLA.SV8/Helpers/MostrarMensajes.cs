using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public class MostrarMensajes : IMostrarMensajes
    {
        public string mode;
        private readonly IJSRuntime js;

        public MostrarMensajes(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task MostrarMensajeError(string mensaje)
        {
            await asignarThema();
            await MostrarMensaje("Error", mensaje, "error", mode);
        }

        public async Task MostrarMensajeExitoso(string mensaje)
        {
            await asignarThema();
            await MostrarMensaje("Exitoso", mensaje, "success", mode);


        }

        private async ValueTask MostrarMensaje(string titulo, string mensaje, string tipoMensaje, string theme)
        {
            await js.InvokeVoidAsync("alerta", titulo, mensaje, tipoMensaje, theme);
        }
        public async Task<bool> Confirmacion(string titulo, string mensaje, string icono)
        {
            await asignarThema();
            var confirmacion = await js.InvokeAsync<bool>("confirmar", titulo, mensaje, icono, mode);

            return confirmacion;
        }

        public async Task asignarThema()
        {
            var modes = await js.GetFromLocalStorage("theme");
            if (modes == "auto")
                mode = await js.InvokeAsync<string>("getPreferredTheme");
            else
                mode = modes;

            Console.WriteLine(mode);
        }

    }
}
