#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace X._509_Lib_IT.DTO
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    [ExcludeFromCodeCoverage]
    public class LicenseState
    {
        public string WritingNo { get; set; }
        public List<State> lstStates { get; set; }

        // ------------------------------------------------

        public override string ToString()
        {
            var retVal = new StringBuilder();

            retVal.AppendFormat("WritingNo:{0}\t{1}{0}Lic States:", Environment.NewLine, WritingNo);

            foreach(var state in lstStates)
            {
                retVal.AppendFormat("{0}\t{1}", Environment.NewLine, state.ToString());
            }

            return retVal.ToString();
        }
    }
}
