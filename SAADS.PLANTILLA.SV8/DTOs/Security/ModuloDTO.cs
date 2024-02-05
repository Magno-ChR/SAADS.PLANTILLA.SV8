namespace SAADS.PLANTILLA.SV8.DTOs.Security
{
    public class ModuloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ruta { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
