namespace SAADS.PLANTILLA.SV8.Helpers
{
    public interface IDataTable
    {
        Task Init(string tableId, List<Columdef>? columnDefs = null, bool? responsive = true, bool? paging = true, bool? searching = true, bool? ordering = true, bool? info = true);
        Task Destroy(string tableId);
    }
}
