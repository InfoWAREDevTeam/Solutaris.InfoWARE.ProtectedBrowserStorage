namespace Solutaris.InfoWARE.ProtectedBrowserStorage.Services
{
    internal class Encryption
    {
        private readonly string m_key;

        public Encryption(string key)
        {
            m_key = key;
        }

        public string Key
        {

            get
            {
                return m_key;
            }

            private set { }

        }
    }
}
