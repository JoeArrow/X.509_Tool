#region © 2018 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Security.Cryptography.X509Certificates;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using X_509_Lib;
using X._509_Lib_IT.DTO;

namespace X._509_Lib_IT
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for ArrowUnitTestXML1
    /// </summary>

    [TestClass]
    public class SSL_Call_IT
    {
        public SSL_Call_IT() { }
        
        private static string SerialNumber; 

        // ------------------------------------------------
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the 
        ///     current test run.
        ///</summary>

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
        [DataRow("Items", "1BBA4482B6AF2FB84B81AC578A0C4739", "https://localhost/desknet_consumer_api/", 
                 "/A-10643?StateCode=AL,AK&amp;WritingNo=02SNG&amp;ItemSource=WebOrdering",
                 "{'StoreName':'My', 'searchType':'FindByThumbprint', 'storeLocation':'CurrentUser', " +
                  "'validOnly':false, 'searchValue':'f294e3e2ceb7206ef647b9d647a3132e7a4803cf'}")]
        public void SSL_Call_With_The_Wrong_Cert_X_509_Tool_Fails_To_Complete_The_SSL_Call(string methodPath, string serial, string baseUri, 
                                                                                           string queryString, string reqJson)
        {
            // -------
            // Arrange

            var serializer = new JavaScriptSerializer();
            var httpClientHandler = new WebRequestHandler();

            SerialNumber = FixupValue(serial);

            var req = serializer.Deserialize<CertRequest>(reqJson);

            // --------------------------------------
            // Pull the Cert and add it to the client

            var certs = X_509_CertTool.GetCertificates(req);
            httpClientHandler.ClientCertificates.Add(certs[0]);

            Console.WriteLine("Cert Used: {1}{0}", Environment.NewLine, certs[0].Subject);

            var client = new HttpClient(httpClientHandler);

            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // ---
            // Act

            var resp = null as HttpResponseMessage;

            try
            {
                var call = string.Format("{0}{1}", methodPath, queryString);

                Console.WriteLine("Web Call being made:{0}\t{1}{0}", Environment.NewLine, call);

                ServicePointManager.ServerCertificateValidationCallback = AflacValidationCallBack;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                // ------------------------------------
                // This cert will never pass validation
                // resp will remain null, and an exception
                // will be thrown

                resp = client.GetAsync(call).Result;

                Console.WriteLine($"{(int)resp.StatusCode} - {resp.ReasonPhrase}");

                if(resp.IsSuccessStatusCode)
                {
                    var ItemDetail = resp.Content.ReadAsAsync<List<CatalogItemDetail>>().Result;
                    Console.WriteLine(ItemDetail.ToString());
                }
            }
            catch(Exception exp)
            {
                RecursiveLog(exp);
            }

            // ------
            // Assert

            Assert.IsNull(resp);
        }

        // ------------------------------------------------
        // Used to follow InnerExceptions for as far as 
        // they go for logging

        private void RecursiveLog(Exception exp)
        {
            Console.WriteLine(exp.Message);

            if(exp.InnerException != null)
            {
                RecursiveLog(exp.InnerException);
            }
        }

        // ------------------------------------------------
        // Accept the Certificate sent by the Server if it
        // has the correct Serial Number.

        private static bool AflacValidationCallBack(object sender, X509Certificate certificate,
                                                    X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var retVal = false;
            var cert = new X509Certificate2(certificate);

            if(sslPolicyErrors == SslPolicyErrors.None)
            {
                retVal = true;
            }
            else
            {
                retVal = cert.Verify();
                var boundary = string.Format("{0}{1}{0}", Environment.NewLine, new string('-', 50));

                Console.WriteLine("{1}AflacValidationCallBack:{0}\t{2}{0}Certificate SerialNumber Used:{0}\t{3}{0}Subject:{0}\t{4}{1}", 
                                  Environment.NewLine,
                                  boundary,
                                  retVal, 
                                  cert.GetSerialNumberString(), 
                                  cert.Subject);
            }

            return retVal;
        }

        // ------------------------------------------------

        private string FixupValue(string val)
        {
            var retVal = new StringBuilder();

            foreach(var chr in val)
            {
                if((chr >= '0' && chr <= '9') ||
                   (chr >= 'a' && chr <= 'z') ||
                   (chr >= 'A' && chr <= 'Z'))
                {
                    retVal.Append(chr);
                }
            }

            return retVal.ToString();
        }
    }
}