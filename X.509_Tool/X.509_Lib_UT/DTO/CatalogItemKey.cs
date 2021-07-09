#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;

using X._509_Lib_IT.DTO.Enumerations;

namespace X._509_Lib_IT.DTO
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    [ExcludeFromCodeCoverage]
    public class CatalogItemKey
    {
        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public eItemSource ItemSource { get; set; }

        // ------------------------------------------------

        public string ToQueryString()
        {
            return string.Format("{0}?ItemSource={1}", Id, ItemSource);
        }
    }
}
