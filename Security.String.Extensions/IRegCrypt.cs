using System.Security;

namespace Security.String.Extensions
{
    public interface IRegCrypt
    {
        SecureString ReadRegistry(string strPath, string strNodeName);
        string ReadString(string strPath, string strNodeName);
        void WriteRegistry(string strPath, string strNodeName, string value);
    }
}