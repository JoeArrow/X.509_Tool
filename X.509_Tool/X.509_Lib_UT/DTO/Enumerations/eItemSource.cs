#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace X._509_Lib_IT.DTO.Enumerations
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    [JsonConverter(typeof(StringEnumConverter))]
    public enum eItemSource
    {
        WebOrdering,
        DuckGear
    }
}
