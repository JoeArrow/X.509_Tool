#region © 2018 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Security;

using Microsoft.Win32;

namespace Security.String.Extensions
{
    // ----------------------------------------------------
    /// <summary>
    ///     RegCrypt is designed to assist with encrypting
    ///     to and decrypting from the Registry.
    /// </summary>

    public class RegCrypt : IRegCrypt
    {
        // ------------------------------------------------
        /// <summary>
        ///     Write an encrypted value to the Registry
        /// </summary>
        /// <param name="strPath">Location within the Registry to write a value</param>
        /// <param name="strNodeName">Name of the Node to write to</param>
        /// <param name="value">Value to be written</param>

        public void WriteRegistry(string strPath, string strNodeName, string value)
        {
            var encrypted = Convert.FromBase64String(value.Encrypt());
            Registry.SetValue(strPath, strNodeName, encrypted);
        }

        // ------------------------------------------------
        /// <summary>
        ///     Decrypt a value from the Registry and return 
        ///     it as a Secure String
        /// </summary>
        /// <param name="strPath">Location within the Registry to read a value</param>
        /// <param name="strNodeName">Name of the Node to read from</param>
        /// <returns></returns>

        public SecureString ReadRegistry(string strPath, string strNodeName)
        {
            return GetEncrypted(strPath, strNodeName).DecryptToSecure();
        }

        // ------------------------------------------------
        /// <summary>
        ///     Decrypt a value from the Registry and return 
        ///     it as a String
        /// </summary>
        /// <param name="strPath">Location within the Registry to read a value</param>
        /// <param name="strNodeName">Name of the Node to read from</param>
        /// <returns></returns>

        public string ReadString(string strPath, string strNodeName)
        {
            return GetEncrypted(strPath, strNodeName).Decrypt();
        }

        // ------------------------------------------------
        /// <summary>
        ///     Decrypt a value from the Registry and return 
        ///     it as a base64 encodedString
        /// </summary>
        /// <param name="strPath">Location within the Registry to read a value</param>
        /// <param name="strNodeName">Name of the Node to read from</param>
        /// <returns></returns>

        private string GetEncrypted(string strPath, string strNodeName)
        {
            var val = Registry.GetValue(strPath, strNodeName, string.Empty) as byte[];
            return Convert.ToBase64String(val);
        }
    }
}
