using Microsoft.JSInterop;
using System.Text.Json;

namespace SAADS.PLANTILLA.SV8.Helpers
{
    public class DataTable : IDataTable
    {
        private readonly IJSRuntime js;

        public DataTable(IJSRuntime js)
        {
            this.js = js;
        }
        private JsonSerializerOptions OpcionesPorDefectoJSON =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public async Task Init(string tableId, List<Columdef>? columnDefs = null, bool? responsive = true, bool? paging = true,bool ? searching = true, bool? ordering = true, bool? info = true)
        {
            
            await js.InvokeVoidAsync("ReadyDataTable", tableId, responsive, paging, searching, ordering, info, columnDefs);
            
        }
        public async Task Destroy(string tableId)
        {
            await js.InvokeVoidAsync("RemoveDataTable", tableId);
        }
    }
    public class Columdef
    {
        public int responsivePriority { get; set; }
        public int targets { get; set; }
        public bool orderable { get; set; } = false;
    }
}
