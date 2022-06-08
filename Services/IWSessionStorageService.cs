using Microsoft.JSInterop;
using System.Text.Json;

namespace Solutaris.InfoWARE.ProtectedBrowserStorage.Services
{
    internal class IWSessionStorageService : IWProtectedBrowserStorageBase, IIWSessionStorageService
    {
        public IWSessionStorageService(IJSRuntime jSRuntime, Encryption encryption)
        {
            m_jSRuntime = jSRuntime;
            m_encryption = encryption;
        }

        public T? GetItem<T>(string key)
        {
            try
            {
                IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
                string json = jSInProcess.Invoke<string>("getItemFromSessionStorage", key, m_encryption.Key);
                return string.IsNullOrWhiteSpace(json) ?
                    default : JsonSerializer.Deserialize<T>(json);

            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            try
            {
                string json = await m_jSRuntime.InvokeAsync<string>("getItemFromSessionStorage", key, m_encryption.Key);
                return string.IsNullOrWhiteSpace(json) ?
                    default : JsonSerializer.Deserialize<T>(json);

            }
            catch (Exception ex)
            {
                return default;
            }

        }

        public void SetItem<T>(string key, T value)
        {
            try
            {
                IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
                jSInProcess.InvokeVoid("setItemToSessionStorage", key, JsonSerializer.Serialize(value), m_encryption.Key);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            try
            {
                await m_jSRuntime.InvokeVoidAsync("setItemToSessionStorage", key, JsonSerializer.Serialize(value), m_encryption.Key);

            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public void RemoveItem(string key)
        {
            IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
            jSInProcess.InvokeVoid("sessionStorage.removeItem", key);
        }

        public void RemoveAllItems()
        {
            IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
            jSInProcess.InvokeVoid("sessionStorage.clear");
        }

        public async Task RemoveItemAsync(string key)
        {
            await m_jSRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
        }

        public async Task RemoveAllItemsAsync()
        {
            await m_jSRuntime.InvokeVoidAsync("sessionStorage.clear");
        }
    }
}