namespace SAADS.PLANTILLA.SV8.DTOs.Security
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Ad { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public int IdCargo { get; set; } = 0;
        public string CargoNombre { get; set; } = string.Empty;
        public int IdSede { get; set; } = 0;
        public string SedeNombre { get; set; } = string.Empty;
        public int IdSedeSesion { get; set; }
        public string SedeSesionNombre { get; set; } = string.Empty;
        public bool Estado { get; set; }

    }
}
