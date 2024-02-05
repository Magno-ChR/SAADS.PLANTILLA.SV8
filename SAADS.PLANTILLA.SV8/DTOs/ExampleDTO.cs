using System.ComponentModel.DataAnnotations;

namespace SAADS.PLANTILLA.SV8.DTOs
{
    public class ExampleDTO
    {

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public int cantidad { get; set; }

        [DataType(DataType.Date, ErrorMessage = "La fecha es obligatoria.")]
        [Required(ErrorMessage = "Ingrese la fecha")]
        public DateTime? fecha { get; set; } = null;

        [DataType(DataType.Time, ErrorMessage = "La hora es obligatoria.")]
        [Required(ErrorMessage = "Hora no válida.")]
        public TimeOnly? hora { get; set; } = null;

        [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que cero.")]
        [Required(ErrorMessage = "El costo es obligatorio")]
        public double? costo { get; set; } = null;

        public bool estado { get; set; } = true;
    }
}
