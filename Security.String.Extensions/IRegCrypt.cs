using System.Security;

namespace Security.String.Extensions
{
    public interface IRegCrypt
    {
        SecureString ReadRegistryValue(string strPath, string strNodeName);
        string ReadStringValue(string strPath, string strNodeName);
        void WriteRegistryValue(string strPath, string strNodeName, string value);
    }
}