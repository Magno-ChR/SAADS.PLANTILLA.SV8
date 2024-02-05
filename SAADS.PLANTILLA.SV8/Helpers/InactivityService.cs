using Microsoft.JSInterop;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    /// <summary>
    /// Servicio que rastrea la inactividad del usuario y dispara un evento cuando se alcanza el tiempo de inactividad especificado.
    /// </summary>
    public class InactivityService
    {
        /// <summary>
        /// Evento que se dispara cuando el usuario está inactivo durante un período de tiempo especificado.
        /// </summary>
        public event Action OnUserInactive;

        private readonly IJSRuntime jsRuntime;
        private readonly System.Timers.Timer inactivityTimer;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InactivityService"/>.
        /// </summary>
        /// <param name="jsRuntime">La interfaz de ejecución de JavaScript.</param>
        public InactivityService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            // Establece el intervalo del temporizador (30 minutos)
            this.inactivityTimer = new System.Timers.Timer(30 * 60 * 1000);
            this.inactivityTimer.Elapsed += (sender, e) => OnTimerElapsed();
        }

        /// <summary>
        /// Inicia el servicio de detección de inactividad.
        /// </summary>
        public void Start()
        {
            // Invoca la función JavaScript para iniciar el detector de inactividad, pasando una referencia al objeto .NET (this).
            jsRuntime.InvokeVoidAsync("startInactivityDetector", DotNetObjectReference.Create(this));
            //
        
          
            // Inicia el temporizador de inactividad.
            inactivityTimer.Start();
        }

        public void StartStorageEvent<T>(T dotNetObject)
        {
             jsRuntime.InvokeVoidAsync("StorageEvent", dotNetObject);
        }

        /// <summary>
        /// Reinicia el temporizador de inactividad.
        /// </summary>
        public void ResetTimer()
        {
            // Detiene y reinicia el temporizador de inactividad.
            inactivityTimer.Stop();
            inactivityTimer.Start();
        }

        /// <summary>
        /// Se llama cuando el temporizador de inactividad alcanza el intervalo especificado, lo que indica que el usuario está inactivo.
        /// Dispara el evento <see cref="OnUserInactive"/>.
        /// </summary>
        private void OnTimerElapsed()
        {
            // Invoca el evento indicando que el usuario está inactivo.
            OnUserInactive?.Invoke();
        }
    }

}
