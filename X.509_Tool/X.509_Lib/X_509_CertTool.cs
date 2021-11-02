#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System.Text;
using System.Security;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;

namespace X_509_Lib
{
    public class X_509_CertTool
    {
        // ------------------------------------------------
        /// <summary>
        ///     Used to read the details from a Certificate 
        ///     File
        /// </summary>
        /// <param name="path2Cert"></param>
        /// <returns></returns>

        public static string Read(string path2Cert)
        {
            var sb = new StringBuilder();

            // ----------------------------------------------------
            // Load the certificate into an X509Certificate object.

            var cert = X509Certificate.CreateFromCertFile(path2Cert);

            // --------------
            // Get the value.

            var results = cert.GetRawCertData();

            // ----------------------------------------
            // Add the byte data to a string for return 

            foreach(var byt in results)
            {
                sb.Append(byt);
            }

            return sb.ToString();
        }
        // ------------------------------------------------
        /// <summary>
        ///     Used to read the details from a Certificate 
        ///     File
        /// </summary>
        /// <param name="path2Cert"></param>
        /// <returns></returns>

        public static string Read(string path2Cert, SecureString password)
        {
            var sb = new StringBuilder();

            // ----------------------------------------------------
            // Load the certificate into an X509Certificate object.

            var cert = new X509Certificate(path2Cert, password);

            // --------------
            // Get the value.

            var results = cert.GetRawCertData();

            // ----------------------------------------
            // Add the byte data to a string for return 

            foreach(var byt in results)
            {
                sb.Append(byt);
            }

            return sb.ToString();
        }

        // ------------------------------------------------
        /// <summary>
        ///     Retrieves a List of Certificates from the
        ///     identified Certificate Store, with the 
        ///     identified criteria
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        public static List<X509Certificate2> GetCertificates(CertRequest req)
        {
            var retVal = new List<X509Certificate2>();

            var store = new X509Store(req.storeName, req.storeLocation);

            // --------------------------
            // Open the Certificate Store

            store.Open(OpenFlags.ReadOnly);

            var searchValue = FixupSearchValue(req.searchType, req.searchValue).Trim();

            // ------------------------------------------
            // Find all Certificates meeting our criteria

            var certs = store.Certificates.Find(req.searchType, searchValue, req.validOnly);

            // ---------------------------
            // Close the Certificate Store

            store.Close();

            // -----------------------------------------------
            // Add each found certificate to a List for return

            foreach(var cert in certs)
            {
                retVal.Add(cert);
            }

            return retVal;
        }

        //// ------------------------------------------------
        ///// <summary>
        /////     Retrieves a List of Certificates from the
        /////     identified Certificate Store, with the 
        /////     identified criteria
        ///// </summary>
        ///// <param name="req"></param>
        ///// <returns></returns>

        //public static List<IX509Certificate2> FindCertificates(CertRequest req, X509_DependencyDto depends = null)
        //{
        //    depends = depends ?? new X509_DependencyDto();
        //    var factory = depends.X509StoreFactory ?? new X509StoreFactory();

        //    var retVal = new List<IX509Certificate2>();

        //    var store = factory.Create(req.storeName, req.storeLocation);

        //    // --------------------------
        //    // Open the Certificate Store

        //    store.Open(OpenFlags.ReadOnly);

        //    var searchValue = FixupSearchValue(req.searchType, req.searchValue);

        //    // ------------------------------------------
        //    // Find all Certificates meeting our criteria

        //    var certs = store.Certificates.Find(req.searchType, searchValue, req.validOnly);

        //    // ---------------------------
        //    // Close the Certificate Store

        //    store.Close();

        //    // -----------------------------------------------
        //    // Add each found certificate to a List for return

        //    foreach(X509Certificate2 cert in certs)
        //    {
        //        retVal.Add(new X509Certificate2Wrap(cert));
        //    }

        //    return retVal;
        //}

        // ------------------------------------------------
        /// <summary>
        ///     Returns the string with everything except 
        ///     alpha-numeric characters removed.
        ///     Includes removing spaces.
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>

        private static string FixupSearchValue(X509FindType searchType, string searchValue)
        {
            var retVal = searchValue;

            if(searchType == X509FindType.FindByThumbprint)
            {
                // ------------------------------------------
                // For the Thumbprint, we need to worry about
                // hidden characters and spaces.

                retVal = Regex.Replace(searchValue, @"[^\da-fA-F]", string.Empty);
            }

            return retVal.ToString();
        }
    }
}
