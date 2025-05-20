using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace FileGrid.Services
{
    public interface IClipboardService
    {
        Task InitializeAsync();
        Task<bool> CopyToClipboardAsync(string text);
    }

    public class ClipboardService : IClipboardService, IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference _module;
        private bool _initialized = false;

        public ClipboardService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
                return;

            _module = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./js/clipboard-helper.js");

            _initialized = true;
        }

        public async Task<bool> CopyToClipboardAsync(string text)
        {
            if (!_initialized)
                await InitializeAsync();

            try
            {
                return await _module.InvokeAsync<bool>("copyToClipboard", text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"剪贴板操作失败: {ex.Message}");
                return false;
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_module != null)
                {
                    await _module.DisposeAsync();
                }
            }
            catch (JSDisconnectedException)
            {
                //当页面已经关闭，或SignalR断开连接，这时JSRuntime不可用，也就不需要再Dispose了。
            }

        }
    }
}
