namespace SAADS.PLANTILLA.SV8.DTOs.Security
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string CorreoPersonal { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int SedeId { get; set; }
    }
}
