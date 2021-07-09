#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

namespace X._509_Lib_IT.DTO
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    [ExcludeFromCodeCoverage]
    public class Address
    {
        public string Fax { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string IsDefaultAddress { get; set; }

        // ------------------------------------------------

        public override string ToString()
        {
            return string.Format("Address{0}\t\tLine 1: {1}{0}\t\tCity: {2}{0}\t\tState: {3}{0}\t\tZip: {4}", 
                                 Environment.NewLine, 
                                 Address1, 
                                 City, 
                                 State, 
                                 PostalCode);
        }
    }
}
