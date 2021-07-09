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
    public class CatalogItem
    {
        public bool IsKit { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CatalogItemKey CatalogItemKey { get; set; }
        
        // ---------------
        // Web Ordering...

        public string LobCode { get; set; }  
        public string PDFName { get; set; }
        public string TypeCode { get; set; }
        public KitItem KitItem { get; set; }
        public string StateCode { get; set; }
        public string PdfImageUrl { get; set; }
        public decimal? CostAmount { get; set; }
        public int? BundleQuantity { get; set; }	
        public string CommodityCode { get; set; }		
        public string LineOfBusiness { get; set; }
        public string TypeDescription { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public eCompany? Company { get; set; }

        // ------------------------------------------------

        public CatalogItem()
        {
            KitItem = new KitItem();
            CatalogItemKey = new CatalogItemKey();
        }
    }
}
