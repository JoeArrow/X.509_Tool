#region © 2018 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using Microsoft.Win32;

using Security.String.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Security.String.Extensions_UT
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for RegCrypt_UT
    /// </summary>

    [TestClass]
    public class RegCrypt_UT
    {
        private string cr = $"{Environment.NewLine}";
        private string crt = $"{Environment.NewLine}\t";

        public RegCrypt_UT() { }

        // ------------------------------------------------
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the 
        ///     current test run.
        ///</summary>

        public TestContext TestContext { set; get; }

        // ------------------------------------------------

        #region Additional test attributes

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

        #endregion

        // ------------------------------------------------

        [TestMethod]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\ADLDSUnitTest", "UserID", @"AFLHQ\E99965")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\Continuum", "UserID", @"AFLHQ\contindeployNT-user")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\SecureStringExtension", "UserID", @"AFLHQ\contindeployNT-user")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\AzureFTP", "UserID", @"AZRCCLMAccountSearch\$AZRCCLMAccountSearch")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\SecureStringExtension\ASCII", "UserID", @"AFLHQ\contindeployNT-user")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\SecureStringExtension\UNICODE", "UserID", @"AFLHQ\contindeployNT-user")]
        public void ReadRegistry_RegCrypt_Reads_An_Encrypted_Value_From_The_Registry(string path, string nodeName, string expected)
        {
            // ---
            // Log

            Console.WriteLine($"Path:{crt}{path}{cr}Node Name:{crt}{nodeName}{cr}Expected Value:{crt}{expected}{cr}");

            // ---
            // Act

            var val = RegCrypt.ReadRegistry(path, nodeName);
            var insecure = val.Unwrap();

            // ---
            // Log

            Console.WriteLine($"Actual:{crt}{insecure}{cr}");

            // ------
            // Assert

            Assert.AreEqual(expected, insecure);
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\SecureStringExtension\Volatile", "UserID", @"AFLHQ\contindeployNT-user")]
        public void ReadString_RegCrypt_Reads_An_Encrypted_Value_From_The_Registry(string path, string nodeName, string expected)
        {
            // ---
            // Log

            Console.WriteLine($"Path:{crt}{path}{cr}Node Name:{crt}{nodeName}{cr}Expected Value:{crt}{expected}{cr}");

            // ---
            // Act

            var val = RegCrypt.ReadString(path, nodeName);

            // ---
            // Log

            Console.WriteLine($"Value Retrieved:{crt}{val}");

            // ------
            // Assert

            Assert.AreEqual(expected, val);
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("UserID", @"AFLHQ\contindeployNT-user", @"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\SecureStringExtension\Volitile")]
        public void WriteRegistry_RegCrypt_Adds_An_Encrypted_Value_To_The_Registry(string nodeName, string entryValue, string path)
        {
            // ---
            // Log

            Console.WriteLine("Path:{0}\t{1}{0}Node Name:{0}\t{2}{0}Entry to Add:{0}\t{3}{0}", 
                              Environment.NewLine, 
                              path, 
                              nodeName, 
                              entryValue);
            // ---
            // Act

            RegCrypt.WriteRegistry(path, nodeName, entryValue);

            // ---
            // Log

            var val = RegCrypt.ReadRegistry(path, nodeName);

            var insecure = val.Unwrap();
            Console.WriteLine($"Value Retrieved:{crt}{insecure}{cr}");

            // ------
            // Assert

            Assert.AreEqual(entryValue, insecure);

            // -------
            // Cleanup

            Registry.LocalMachine.DeleteSubKeyTree(path.Substring(path.IndexOf(@"\") + 1), false);

            // ---
            // Log

            Console.WriteLine($"Entry Removed:{crt}{path}");
        }
    }
}