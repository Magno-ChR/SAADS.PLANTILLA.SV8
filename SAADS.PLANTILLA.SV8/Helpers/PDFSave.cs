
using Microsoft.JSInterop;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public class PDFSave : IFilePDF
    {
        private readonly IJSRuntime js;

        public PDFSave(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task SavePDF(string base64)
        {
            await js.InvokeVoidAsync("DonwloadPDF", base64);
        }
    }
}
