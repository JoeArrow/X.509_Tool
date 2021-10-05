#region © 2021 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

namespace X_509_Lib
{
    // ----------------------------------------------------
    /// <summary>
    ///     OutputLengths Description
    /// </summary>

    public struct OutputLengths
    {
        public int SubjectLen { get; set; }
        public int ExpDateLen { get; set; }
        public int ThumbprintLen { get; set; }
        public int IssuerLen { get; set; }
        public int Total { get { return SubjectLen + ExpDateLen + ThumbprintLen + IssuerLen; } }
    }
}
