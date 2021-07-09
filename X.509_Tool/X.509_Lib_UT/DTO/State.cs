#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System.Diagnostics.CodeAnalysis;

namespace X._509_Lib_IT.DTO
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    [ExcludeFromCodeCoverage]
    public class State
    {
        public string Company { get; set; }
        public string StateCode { get; set; }

        // ------------------------------------------------

        public override string ToString()
        {
            return string.Format("State: {0}\tCompany: {1}", StateCode, Company);
        }
    }
}
