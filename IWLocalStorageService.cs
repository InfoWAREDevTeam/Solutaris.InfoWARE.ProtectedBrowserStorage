using Microsoft.JSInterop;
using System.Text.Json;

namespace Solutaris.InfoWARE.ProtectedBrowserStorage.Services
{
    internal class IWLocalStorageService : IWProtectedBrowserStorageBase, IIWLocalStorageService
    {
        public IWLocalStorageService(IJSRuntime jSRuntime, Encryption encryption)
        {
            m_jSRuntime = jSRuntime;
            m_encryption = encryption;
        }

        public T? GetItem<T>(string key)
        {
            try
            {
                IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
                string json = jSInProcess.Invoke<string>("getItemFromLocalStorage", key, m_encryption.Key);
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
                string json = await m_jSRuntime.InvokeAsync<string>("getItemFromLocalStorage", key, m_encryption.Key);
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
                jSInProcess.Invoke<bool>("setItemToLocalStorage", key, JsonSerializer.Serialize(value), m_encryption.Key);
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
                await m_jSRuntime.InvokeAsync<bool>("setItemToLocalStorage", key, JsonSerializer.Serialize(value), m_encryption.Key);

            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public void RemoveItem(string key)
        {
            IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
            jSInProcess.InvokeVoid("localStorage.removeItem", key);
        }

        public void RemoveAllItems()
        {
            IJSInProcessRuntime jSInProcess = (IJSInProcessRuntime)m_jSRuntime;
            jSInProcess.InvokeVoid("localStorage.clear");
        }

        public async Task RemoveItemAsync(string key)
        {
            await m_jSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task RemoveAllItemsAsync()
        {
            await m_jSRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}
