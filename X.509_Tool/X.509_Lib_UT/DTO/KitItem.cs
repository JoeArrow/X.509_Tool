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
    public class KitItem
    {
        public string KitNo { get; set; }
        public string ItemKitNo { get; set; }
        public int? ItemKitQuantity { get; set; }
        public string KitDescription { get; set; }
        public string ItemKitDescription { get; set; }
        public string KitDetailDescription { get; set; }
    }
}
