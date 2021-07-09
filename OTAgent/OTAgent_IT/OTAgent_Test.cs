#region © 2018 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using opentoken;
using AflacCommonObjects;
using Security.String.Extensions;

namespace OTAgent_IT
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for ArrowUnitTestXML1
    /// </summary>

    [TestClass]
    public class OTAgent_Test
    {
        public OTAgent_Test() { }

        // ------------------------------------------------
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the 
        ///     current test run.
        ///</summary>

        public TestContext TestContext { set; get; }

        // ------------------------------------------------

        #region Additional test attributes
#pragma warning disable

        // ------------------------------------------------
        // You can use the following additional attributes 
        // as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }

#pragma warning restore
        #endregion

        // ------------------------------------------------

        [DeploymentItem("TestData\\OTAgent_TD.xml"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    "|DataDirectory|\\OTAgent_TD.xml",
                    "ReadExpiredToken",
                    DataAccessMethod.Sequential),
         TestMethod]
        public void ReadExpiredToken_OTAgent()
        {
            // -------
            // Arrange

            var token = Convert.ToString(TestContext.DataRow["Token"]);
            var userType = Convert.ToString(TestContext.DataRow["UserType"]);
            var configPath = Convert.ToString(TestContext.DataRow["Config"]);

            var config = Configure(configPath, userType);

            var sut = new OTAgent(config);

            // ---
            // Log

            Console.WriteLine("Token:{0}\t{1}{0}", Environment.NewLine, token);

            // ---
            // Act

            var result = sut.ReadExpiredToken(token);

            // ---
            // Log

            foreach(var attr in result)
            {
                Console.WriteLine("Attribute:{0}\t{1}{0}", Environment.NewLine, attr.ToString());
            }

            // ------
            // Assert

            Assert.IsNotNull(result);
        }

        // ------------------------------------------------

        [DeploymentItem("TestData\\OTAgent_TD.xml"),
         DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    "|DataDirectory|\\OTAgent_TD.xml",
                    "ReadToken",
                    DataAccessMethod.Sequential),
         TestMethod]
        public void ReadToken_OTAgent()
        {
            // -------
            // Arrange

            var token = Convert.ToString(TestContext.DataRow["Token"]);
            var userType = Convert.ToString(TestContext.DataRow["UserType"]);
            var configPath = Convert.ToString(TestContext.DataRow["Config"]);

            var config = Configure(configPath, userType);

            var sut = new OTAgent(config);

            // ---
            // Log

            Console.WriteLine("Token:{0}\t{1}{0}", Environment.NewLine, token);

            // ---
            // Act

            var result = sut.ReadToken(token);

            // ---
            // Log

            foreach(var attr in result)
            {
                Console.WriteLine("Attribute:{0}\t{1}{0}", Environment.NewLine, attr.ToString());
            }

            // ------
            // Assert

            Assert.IsNotNull(result);
        }

        // ------------------------------------------------

        public AgentConfiguration Configure(string path, string userType, string env = "")
        {
            var retVal = new AgentConfiguration();
            var CfgReader = new ConfigFileReader(path);

            if(!string.IsNullOrEmpty(env))
            {
                env = string.Format(@"{0}\", env);
            }

            var regKey = string.Format(CfgReader["Credentials"], userType, env);

            var tokenName = RegCrypt.ReadRegistry(regKey, "userID");
            var password = RegCrypt.ReadRegistry(regKey, "password");

            retVal.TokenName = tokenName.Unwrap();
            retVal.SetPassword(password.Unwrap(), Token.CipherSuite.AES_128_CBC);

            retVal.CookiePath = CfgReader[string.Format("{0}_CookiePath", userType)];
            retVal.CookieDomain = CfgReader[string.Format("{0}_CookieDomain", userType)];
            retVal.UseCookie = Convert.ToBoolean(CfgReader[string.Format("{0}_UseCookie", userType)]);
            retVal.UseSunJCE = Convert.ToBoolean(CfgReader[string.Format("{0}_UseSunJCE", userType)]);
            retVal.TokenLifetime = Convert.ToInt32(CfgReader[string.Format("{0}_TokenLifetime", userType)]);
            retVal.SecureCookie = Convert.ToBoolean(CfgReader[string.Format("{0}_SecureCookie", userType)]);
            retVal.SessionCookie = Convert.ToBoolean(CfgReader[string.Format("{0}_SessionCookie", userType)]);
            retVal.RenewUntilLifetime = Convert.ToInt32(CfgReader[string.Format("{0}_TokenRenewUntil", userType)]);
            retVal.ObfuscatePassword = Convert.ToBoolean(CfgReader[string.Format("{0}_ObfuscatePassword", userType)]);
            retVal.NotBeforeTolerance = Convert.ToInt32(CfgReader[string.Format("{0}_NotBeforeTolerance", userType)]);

            return retVal;
        }
    }
}