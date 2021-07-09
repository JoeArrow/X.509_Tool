#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

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
    public class ItemState
    {
        public string WritingNo { get; set; }
        public List<string> StateCode { get; set; }

        // ------------------------------------------------

        public ItemState()
        {
            StateCode = new List<string>();
        }

        // ------------------------------------------------

        public string ToQueryString()
        {
            var retVal = new StringBuilder();

            if(StateCode != null && StateCode.Count > 0)
            {
                var delimiter = string.Empty;

                retVal.Append("StateCode=");

                foreach(var state in StateCode)
                {
                    retVal.AppendFormat("{0}{1}", delimiter, state);
                    delimiter = ",";
                }
            }

            if(!string.IsNullOrEmpty(WritingNo))
            {
                retVal.AppendFormat("&WritingNo={0}", WritingNo);
            }

            return retVal.ToString();
        }
    }
}
