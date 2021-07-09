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
    public class CatalogItemDetail
    {
        public int? minlimit { get; set; }
        public bool? preOrder { get; set; }
        public int? maxlimit { get; set; }
        public bool? overLimit { get; set; }
        public string message { get; set; }
        public bool? outOfStock { get; set; } 
        public CatalogItem Item { get; set; }
        public int? QuantityOnHand { get; set; }
        public int? item_bundle_qty { get; set; }
        public IEnumerable<KitComponent> KitComponents { get; set; }

        // ------------------------------------------------

        public override string ToString()
        {
            var sb = new StringBuilder();
            var outputFormat = "{0}:\r\n\t{1}\r\n\r\n";

            var obj = GetType();
            var properties = obj.GetProperties();

            // -----------------------------------------------
            // Step through each property in the event object.

            foreach(var prop in properties)
            {
                try
                {
                    object val = prop.GetValue(this, null);

                    // -------------------------------------------
                    // Only output the property if it has a value.

                    if(val != null && val.ToString() != string.Empty && val.ToString() != "0")
                    {
                        sb.AppendFormat(outputFormat, prop.Name, val.ToString());
                    }
                }
                catch(Exception ex)
                {
                    sb.AppendFormat(outputFormat, prop.Name, ex.Message);
                }
            }

            return sb.ToString();
        }
    }
}
