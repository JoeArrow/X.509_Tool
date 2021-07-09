#region © 2019 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

namespace CIA
{
    // ----------------------------------------------------
    /// <summary>
    ///     ListItem Description
    /// </summary>

    public class ListItem
    {
        public string Name { set; get; }
        public object Value { set; get; }
        public string Abreviation { set; get; }

        // ------------------------------------------------

        public override string ToString()
        {
            return Name;
        }
    }
}
