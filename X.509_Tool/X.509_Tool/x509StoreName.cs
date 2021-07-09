#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System.Security.Cryptography.X509Certificates;

namespace X._509_Tool
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    public class x509StoreName
    {
        public string Name { get; set; }
        public StoreName storeName { get; set; }

        // ------------------------------------------------

        public override string ToString()
        {
            return Name;
        }
    }
}
