namespace SAADS.PLANTILLA.SV8.DTOs
{
    public class ResponseListDTO<T>
    {
        public ResponseListDTO()
        {

        }
        public ResponseListDTO(List<T> valores, Dictionary<string, string> datosPaginacion)
        {
            value = valores;

            quanty = int.Parse(datosPaginacion["quanty"]);
            page = int.Parse(datosPaginacion["page"]);
            total = int.Parse(datosPaginacion["total"]);
        }

        public int page { get; set; }
        public int total { get; set; }
        public int quanty { get; set; }
        public List<T>? value { get; set; }

    }
    public class ResponseErrorDTO
    {

        public ResponseErrorDTO()
        {
        }
        public ResponseErrorDTO(int StatusCode, string mensaje)
        {
            this.Message = mensaje;
            this.StatusCode = StatusCode;
        }

        public int StatusCode { get; set; } = 400;
        public string Message { get; set; } = "Error";

    }

    public class ResponseDTO<T>
    {
        //instanciar clase
        public ResponseDTO()
        {

        }

        public ResponseDTO(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;

        }

        public ResponseDTO(int statusCode, string message, T? value)
        {
            StatusCode = statusCode;
            Message = message;
            this.value = value;
        }

        public ResponseDTO(T value)
        {
            this.value = value;
        }



        public bool Success
        {
            get
            {
                // si el codigo es 200, es exitoso el request 
                return StatusCode >= 200 && StatusCode < 300;

            }
        }
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "OK";
        public T? value { get; set; }


    }
}
