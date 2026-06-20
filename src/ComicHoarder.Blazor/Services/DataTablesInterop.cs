using Microsoft.JSInterop;

namespace ComicHoarder.Blazor.Services
{
    public class DataTablesInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public DataTablesInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitDataTableAsync(string tableId)
        {
            await _jsRuntime.InvokeVoidAsync("dataTablesInterop.initDataTable", tableId);
        }

        public async Task DestroyDataTableAsync(string tableId)
        {
            await _jsRuntime.InvokeVoidAsync("dataTablesInterop.destroyDataTable", tableId);
        }
    }
}