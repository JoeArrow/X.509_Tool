#region © 2018 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System.Collections.Generic;

namespace opentoken
{
    // ----------------------------------------------------
    /// <summary>
    ///     OTAgent derives from opentoken-agent and adds 
    ///     a method allowing for reading an expired 
    ///     opentoken.
    /// </summary>

    public class OTAgent : Agent
    {
        private AgentConfiguration Config { set; get; }

        // ------------------------------------------------

        public OTAgent(AgentConfiguration cfg)
            : base(cfg)
        {
            Config = cfg;
        }

        // ------------------------------------------------
        /// <summary>
        ///     ReadExpiredToken simply ignores the expiration
        ///     datetime and returns the information from
        ///     the token.
        ///     
        ///     NOTE:
        ///     Valid Ping Credentials are still required to
        ///     work with an opentoken.
        /// </summary>
        /// <param name="token">An opentoken which needs to be read (expired or not)</param>

        public Dictionary<string, string> ReadExpiredToken(string token)
        {
            var dictionaries = Token.decode(token, Config.GetDecryptKey);
            Dictionary<string, string> strs = Config.FlattenMultiStringDictionary(dictionaries);
            return strs;
        }
    }
}
