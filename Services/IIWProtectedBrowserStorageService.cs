namespace Solutaris.InfoWARE.ProtectedBrowserStorage.Services
{
    public interface IIWProtectedBrowserStorageService
    {
        #region Synchronous Methods
        /// <summary>
        /// Use this from Blazor WASM only 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetItem<T>(string key, T value);

        /// <summary>
        /// Use this from Blazor WASM only 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? GetItem<T>(string key);

        /// <summary>
        /// Use this from Blazor WASM only 
        /// </summary>
        /// <param name="key"></param>
        public void RemoveItem(string key);

        /// <summary>
        /// Use this from Blazor WASM only - It remove all Local storage items
        /// </summary>
        public void RemoveAllItems();
        #endregion


        #region Asynchronous Methods

        /// <summary>
        /// Use this for Blazor Server or Blazor WASM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task SetItemAsync<T>(string key, T value);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T?> GetItemAsync<T>(string key);

        /// <summary>
        ///  Use this for Blazor Server or Blazor WASM
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task RemoveItemAsync(string key);

        /// <summary>
        /// Use this for Blazor Server or Blazor WASM - It remove all Local storage items
        /// </summary>
        /// <returns></returns>
        public Task RemoveAllItemsAsync();
        #endregion
    }
}
