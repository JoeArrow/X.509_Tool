#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Web.Script.Serialization;

using Security.String.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using X_509_Lib;

namespace X._509_Lib_IT
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for ArrowUnitTestXML1
    /// </summary>

    [TestClass]
    public class X_509
    {
        public X_509() { }

        public TestContext TestContext { set; get; }

        // ------------------------------------------------

        #region Additional test attributes
#pragma warning disable S125

        // You can use the following additional attributes as you write your tests:
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

#pragma warning restore S125
        #endregion

        // ------------------------------------------------

        [TestMethod]
        [DataRow(@"C:\Temp\Certs\AFLHQ_Eica_Bin.cer")]
        [DataRow(@"C:\Temp\Certs\NTLab_Eica_Bin.cer")]
        [DataRow(@"C:\Temp\Certs\AFLHQ_Orca_base64.cer")]
        public void Read_X_509_Returns_The_Raw_Data_form_a_Certificate_as_a_string(string path)
        {
            // ---
            // Log

            Console.WriteLine("Search for Cert: {0}", path);

            // ---
            // Act

            var output = X_509_CertTool.Read(path);

            // ---
            // Log

            Console.WriteLine("Raw Data Read from {1}:{0}{0}{2}", Environment.NewLine, path, output);

            // ------
            // Assert

            Assert.IsFalse(string.IsNullOrEmpty(output));
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow(@"C:\Temp\Certs\ic_customerprofilemgtapi.nt.lab.com.pfx", "CiamPmNonProd@19")]
        public void Read_X509(string path, string password)
        {
            // ---
            // Log

            Console.WriteLine("Search for Cert: {0}", path);

            // ---
            // Act

            var output = X_509_CertTool.Read(path, password.ToSecureString());

            // ---
            // Log

            Console.WriteLine("Raw Data Read from {1}:{0}{0}{2}", Environment.NewLine, path, output);

            // ------
            // Assert

            Assert.IsFalse(string.IsNullOrEmpty(output));
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("{'StoreName':'My','searchType':'FindBySubjectName','storeLocation':'LocalMachine','searchValue':'5CG0178WT2'}")]
        [DataRow("{'StoreName':'My','searchType':'FindBySubjectName','storeLocation':'LocalMachine','searchValue':'5CG0178WT2.hq.aflac.com'}")]
        [DataRow("{'StoreName':'My','searchType':'FindByThumbprint','storeLocation':'CurrentUser','validOnly':false,'searchValue':'f294e3e2ceb7206ef647b9d647a3132e7a4803cf'}")]
        [DataRow("{'StoreName':'My','searchType':'FindByThumbprint','storeLocation':'CurrentUser','validOnly':false,'searchValue':'b3ab70cb6d9846c03dda38eac707195f1c57e899'}")]
        public void Find_X_509_Retrieves_A_Certificate_List(string reqJson)
        {
            // -------
            // Arrange
            
            var serializer = new JavaScriptSerializer();

            var req = serializer.Deserialize<CertRequest>(reqJson);

            // ---
            // Log

            Console.WriteLine(req.ToString());

            // ---
            // Act

            var certs = X_509_CertTool.GetCertificates(req);

            // ------
            // Assert

            Assert.IsTrue(certs.Count > 0);

            // ---
            // Log

            Console.WriteLine("{0}{1}{0}{0}{2} Certificate(s) Found", Environment.NewLine, new string('-', 50), certs.Count.ToString());

            foreach(var cert in certs)
            {
                Console.WriteLine("{0}{1}{0}{0}{2}", Environment.NewLine, new string('-', 50), cert.ToString());
            }
        }
    }
}