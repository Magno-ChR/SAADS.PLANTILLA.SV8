using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public class FileSave : IFileSave
    {
        private readonly IJSRuntime js;

        public FileSave(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task SaveAS(string filename, string data, string type = "text/plain;charset-uft-8")
        {
            await js.InvokeVoidAsync("FileSaveBlazor.saveAs", filename, data, type);
        }
    }
}
