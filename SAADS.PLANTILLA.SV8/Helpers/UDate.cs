namespace SAADS.PLANTILLA.SV8.Helpers
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Convierte el valor de tiempo de Unix en un objeto DateTime.
        /// </summary>
        /// <param name="unixtime">La marca de tiempo de Unix que desea convertir a DateTime.</param>
        /// <returns>Devuelve un objeto DateTime que representa el valor de la hora Unix.</returns>
        public static DateTime UnixTimeToDateTime(this long unixtime)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixtime).ToLocalTime();
            return dateTime;
        }

        /// <summary>
        /// Convertir una fecha y hora en una marca de tiempo unix
        /// </summary>
        /// <param name="MyDateTime">El objeto DateTime para convertir a Unix Time</param>
        /// <returns></returns>
        public static long DateTimeToUnix(this DateTime MyDateTime)
        {
            TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)timeSpan.TotalSeconds;
        }

    }
}
