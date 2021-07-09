#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Security.Cryptography.X509Certificates;

namespace X_509_Lib
{
    // ----------------------------------------------------
    /// <summary>
    ///     Used to request one or more certificates 
    /// </summary>

    public class CertRequest
    {
        public bool validOnly { set; get; }
        public string searchValue { set; get; }
        public StoreName storeName { set; get; }
        public X509FindType searchType { set; get; }
        public StoreLocation storeLocation { set; get; }

        // ------------------------------------------------
        
        public CertRequest()
        {
            validOnly = true;
        }

        // ------------------------------------------------

        public override string ToString()
        {
            return string.Format("[Search Type]{0}  {1}{0}{0}[Search Value]{0}  {2}{0}{0}[Store Name]{0}  {3}{0}{0}[Store Location]{0}  {4}", 
                                 Environment.NewLine, searchType.ToString(), searchValue, storeName, storeLocation);
        }
    }
}
