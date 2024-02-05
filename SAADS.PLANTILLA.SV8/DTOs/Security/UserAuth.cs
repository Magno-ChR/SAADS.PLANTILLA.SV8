namespace SAADS.PLANTILLA.SV8.DTOs.Security
{
    public class UserAuth
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CargoNombre { get; set; } = string.Empty;
        public int CargoId { get; set; }
        public int SedeId { get; set; }
        public string SedeNombre { get; set; } = string.Empty;
        public int AplicacionId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long Expiracion { get; set; }
        public string Token { get; set; } = string.Empty;
        public string timezone { get; set; } = string.Empty;
        // public bool IsAuthenticated { get; set; } = false;

        //IsAuthenticated true si ExpiracionDatetime > DateTime.Now
        public bool IsAuthenticated => ExpiracionDateTime > DateTime.Now;


        //convertir long a datetime
        public DateTime ExpiracionDateTime
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(Expiracion);
            }
        }
    }
}
