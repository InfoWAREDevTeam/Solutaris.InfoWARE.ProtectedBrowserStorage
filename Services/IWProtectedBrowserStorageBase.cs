using Microsoft.JSInterop;

namespace Solutaris.InfoWARE.ProtectedBrowserStorage.Services
{
    internal abstract class IWProtectedBrowserStorageBase
    {
        protected IJSRuntime m_jSRuntime;
        protected Encryption m_encryption;
    }
}
