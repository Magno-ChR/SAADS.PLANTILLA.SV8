namespace SAADS.PLANTILLA.SV8.DTOs.Security
{
    public class InterfaceDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string EnlaceTutorial { get; set; } = string.Empty;
        public int Tipo { get; set; }
        public int IdModulo { get; set; }
        public string Icono { get; set; } = string.Empty;
        public bool Estado { get; set; }

        public List<TareaDTO> Tareas { get; set; } = new List<TareaDTO>();

    }

    public class TareaDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdInterface { get; set; }
        public int Tipo { get; set; }

    }
}
