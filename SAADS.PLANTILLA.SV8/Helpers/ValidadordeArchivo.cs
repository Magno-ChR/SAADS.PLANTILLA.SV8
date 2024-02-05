using Microsoft.AspNetCore.Components.Web.Virtualization;
using SAADS.PLANTILLA.SV8.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public class ValidadordeArchivo
    {
        public List<string> Imagen = new List<string>() { ".png", ".jpg", ".jpeg", ".png", ".jfif", ".webp" };
        public List<string> Documento = new List<string>() { ".pdf", ".txt", ".doc", ".xls", ".html", ".htm", ".ppt", ".docx","Application/pdf" };
        public List<string> audio = new List<string>() { ".mp3", ".aud", };
        public List<string> video = new List<string>() { ".mp4", ".avi", };
        private List<List<string>> listaTipo = new List<List<string>>();
        public ValidadordeArchivo()
        {
            listaTipo = new List<List<string>>() { Imagen, Documento, audio, video };
        }
        public Dato TipoDato = Dato.otro;
        public int tipoForposition;
        public bool SearchTipo(string? tipo, int position = 0)
        {
            var encontrado = false;
            if (position < listaTipo.Count)
            {
                for (int i = 0; i < listaTipo[position].Count; i++)
                {
                    var item = listaTipo[position][i];
                    if (item == tipo)
                    {
                        encontrado = true;
                        break;
                    }
                }
                if(encontrado == false)
                    return SearchTipo(tipo, (position + 1));
                else
                {
                    tipoForposition = position;
                    TipoDato = _Tipo;
                    return true;
                }
            }
            TipoDato = Dato.undefined;
            return false;
        }
        private Dato _Tipo
        {
            get
            {
                switch (tipoForposition)
                {
                    case 0:
                        return Dato.imagen;
                    case 1:
                        return Dato.documento;
                    case 2:
                        return Dato.audio;
                    case 3:
                        return Dato.video;
         
                    default:
                        return Dato.otro;
                }
            }
        }

    }
}
