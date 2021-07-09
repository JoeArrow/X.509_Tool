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
    public class GetUserDetailResponse
    {
        public string ADGroup { get; set; }
        public string Territory { get; set; }
        public string MarketOffice { get; set; }
        public string AgentHireDate { get; set; }
        public Address Addresses { get; set; }
        public string BudgetCenterNumber { get; set; }
        public List<LicenseState> LicenseStates { get; set; }

        // ------------------------------------------------

        public GetUserDetailResponse()
        {
            LicenseStates = new List<LicenseState>();
        }

        // ------------------------------------------------

        public override string ToString()
        {
            var retVal = new StringBuilder();

            retVal.AppendFormat("Territory:{0}\t{1}{0}", Environment.NewLine, Territory);
            retVal.AppendFormat("Addresses:{0}\t{1}{0}", Environment.NewLine, Addresses);

            if(LicenseStates != null)
            {
                foreach(var licState in LicenseStates)
                {
                    retVal.AppendFormat("{1}{0}", Environment.NewLine, licState.ToString());
                }
            }

            return retVal.ToString();
        }
    }
}
