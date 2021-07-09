#region © 2019 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System.Security;

namespace CIA
{
    // ----------------------------------------------------
    /// <summary>
    ///     TokenConfig Description
    /// </summary>

    public class TokenPassword
    {
        public string EntityType { get; set; }
        public string Environment { get; set; }
        public string CredLocation { get; set; }
        public SecureString Password { set; get; }
    }
}
