#region Copyright © 2018 Aflac Inc.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Security;

using Security.String.Extensions;

using Microsoft.Win32;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecurityStringExtensions_UT
{
    // ----------------------------------------------------
    /// <summary>
    ///     Summary description for SecurityExtensions_UT
    /// </summary>

    [TestClass]
    public class SecurityExtensions_UT
    {
        private string cr = $"{Environment.NewLine}";
        private string crt = $"{Environment.NewLine}\t";

        public SecurityExtensions_UT() { }

        // ------------------------------------------------
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        ///</summary>

        public TestContext TestContext { set; get; }

        #region Additional test attributes
#pragma warning disable
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
#pragma warning restore
        #endregion

        // ------------------------------------------------

        [TestMethod]
        [DataRow("jarr0w@afl", "")]
        [DataRow("THIS IS A PASSWORD", "")]
        [DataRow("this is a password", "")]
        [DataRow("B0GU5P@55W0rd", "Test Entropy")]
        [DataRow("this is a password", "32FDDE48-A0F3-4A92-BA60-F680F095B6BB")]
        public void Encryption_Security_String_Extensions_Encrypts_a_String(string testValue, string entropy)
        {
            // ---
            // Act

            var cipher = testValue.Encrypt(entropy);

            // ---
            // Log

            Console.WriteLine($"TestValue:{crt}{testValue}{cr}Entropy:{crt}'{entropy}'{cr}Cipher Length:{crt}{cipher.Length}{cr}Cipher:{crt}{cipher}");

            // ------
            // Assert

            Assert.IsTrue(cipher.Length > 50);
            Assert.AreNotEqual(testValue, cipher);
        }

        // ------------------------------------------------

        [TestMethod]
        public void Encryption_Security_String_Extensions_Encrypts_an_Empty_String_Throws_An_Exception()
        {
            // -------
            // Arrange

            var entropy = string.Empty;
            var testValue = string.Empty;

            // ----------
            // Act/Assert

            Assert.ThrowsException<ArgumentNullException>(() => { testValue.Encrypt(entropy); });
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow(true, "")]
        [DataRow(false, "Value")]
        public void IsNullOrEmpty_Security_String_Identifies_When_A_Secure_String_Is_Null_Or_Empty(bool expected, string testValue)
        {
            // -------
            // Arrange

            var sut = testValue.ToSecureString();

            // ---
            // Act

            var actual = sut.IsNullOrEmpty();

            // ------
            // Assert

            Assert.AreEqual(expected, actual);
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("jarr0w@afl", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAACAepvX1btAR9P4/cHkhoSeAAAAAASAAACgAAAAEAAAADF2VU0wgbm4bGQzDZ2P/EcYAAAA5NiHsnXPGA9NxGHFoXMSU8lwBABXvkLwFAAAAIJWsu/TCZ1tG7L6SYkHRSnwa32i", "")]
        [DataRow("this is a password", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==", "")]
        [DataRow("this is a password", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAABTjfiIGQPoHIgy1TrP9MThAAAAAASAAACgAAAAEAAAADehxfAFi76+X+MlMNHdltEoAAAA5dD75DRvBvhYxuKyev1S5Yj9BICQj1OmCWSrLB0lRC8p/hkpYU/yghQAAADm2433MWJuaa10/+eqLmGy5L/Uww==", "32FDDE48-A0F3-4A92-BA60-F680F095B6BB")]
        public void Decryption_Security_String_Extensions_Decrypts_an_Encrypted_String(string expected, string encryptedVal, string entropy)
        {
            // ----------------------------------------------------
            // Each machine will result in unique encrypted values.
            // We cannot store an encrypted value which could be 
            // decrypted everywhere.
            //
            // So, we will encrypt the expected value on the fly 
            // for comparison.

            var testValue = expected.Encrypt(entropy);

            // ---
            // Log

            Console.WriteLine($"Value to work with:{crt}{expected}{cr}Entropy:{crt}'{entropy}'{cr}");

            // ---
            // Act

            var result = encryptedVal.Decrypt(entropy);

            // ---
            // Log

            Console.WriteLine($"result:{crt}{result}{cr}{cr}TestValue:{crt}{testValue}");

            // ------
            // Assert

            Assert.AreEqual(expected, result);
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("")]
        [DataRow("0BF792C6-F013-41E5-9CC0-793DA4B58428")]
        public void Decryption_Security_String_Extensions_Decrypts_An_Empty_Encrypted_String_Throws_An_ArgumentNullException(string entropy)
        {
            // -------
            // Arrange

            var testValue = string.Empty;

            // ----------
            // Act/Assert

            Assert.ThrowsException<ArgumentNullException>(() => { testValue.Decrypt(entropy); });
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("B0GU5P@55W0rd")]
        [DataRow("this is a password")]
        public void ToSecureString_Security_String_Extensions_Wraps_a_String_in_a_SecureString_Object(string testValue)
        {
            // ---
            // Act

            var secureString = testValue.ToSecureString();

            // ---
            // Log

            Console.WriteLine($"TestValue:{crt}{testValue}{cr}UnWrapped SecureString: {secureString.Unwrap()}");

            // ------
            // Assert

            Assert.AreEqual(testValue, secureString.Unwrap());
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("B0GU5P@55W0rd", "")]
        [DataRow("this is a password", "")]
        [DataRow("THIS IS A PASSWORD", "32FDDE48-A0F3-4A92-BA60-F680F095B6BB")]
        public void EncryptSecureString_Security_String_Extensions_Encrypts_a_SecureString_Object_Duh(string testValue, string entropy)
        {
            // ---
            // Act

            var encryptedSecureString = testValue.ToSecureString().Encrypt(entropy);

            // ---
            // Log

            Console.WriteLine($"TestValue:{crt}{testValue}{cr}Entropy:{crt}'{entropy}'{cr}Encrypted SecureString:{crt}{encryptedSecureString}");

            // ------
            // Assert

            Assert.AreNotEqual(testValue, encryptedSecureString);
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("", 
                 "this is a password",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==")]

        [DataRow("32FDDE48-A0F3-4A92-BA60-F680F095B6BB",
                 "this is a password",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAABTjfiIGQPoHIgy1TrP9MThAAAAAASAAACgAAAAEAAAADehxfAFi76+X+MlMNHdltEoAAAA5dD75DRvBvhYxuKyev1S5Yj9BICQj1OmCWSrLB0lRC8p/hkpYU/yghQAAADm2433MWJuaa10/+eqLmGy5L/Uww==")]
        public void DecryptToSecureString_Security_String_Extensions_Decrypts_an_Encrypted_SecureString_Object(string entropy, string expected, string testValue)
        {
            // ---
            // Act

            var decryptedSecureString = testValue.DecryptToSecure(entropy);

            // ---
            // Log

            Console.WriteLine($"TestValue:{crt}{testValue}{cr}Decrypted SecureString: {decryptedSecureString.Unwrap()}");

            // ------
            // Assert

            Assert.AreEqual(expected, decryptedSecureString.Unwrap());
        }

        // ------------------------------------------------

        [TestMethod]
        public void DecryptToSecureString_Security_String_Extensions_Decrypts_an_Empty_SecureString_Object_Throws_An_Exception()
        {
            // -------
            // Arrange
            
            var testValue = string.Empty;

            // ----------
            // Act/Assert

            Assert.ThrowsException<ArgumentNullException>(() => { testValue.DecryptToSecure(); });
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow(false,
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAACTO4HWsndsUdHeyvw573D/AAAAAASAAACgAAAAEAAAAFGodJgMFe9EwclV/hrg5xooAAAA1BOlyAOnqC7HDGv7CuU9tMtSnvKA2n8aIl6oFj2a/yZolFcnxdZUDhQAAAB03ekjjDGd91ngubJ7DjuQrVDqoA==",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==")]

        [DataRow(true,
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAACTO4HWsndsUdHeyvw573D/AAAAAASAAACgAAAAEAAAAFGodJgMFe9EwclV/hrg5xooAAAA1BOlyAOnqC7HDGv7CuU9tMtSnvKA2n8aIl6oFj2a/yZolFcnxdZUDhQAAAB03ekjjDGd91ngubJ7DjuQrVDqoA==",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAACTO4HWsndsUdHeyvw573D/AAAAAASAAACgAAAAEAAAAFGodJgMFe9EwclV/hrg5xooAAAA1BOlyAOnqC7HDGv7CuU9tMtSnvKA2n8aIl6oFj2a/yZolFcnxdZUDhQAAAB03ekjjDGd91ngubJ7DjuQrVDqoA==")]

        [DataRow(true,
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==")]
        public void Matches_Security_String_Extensions_Compares_Two_Different_Encrypted_SecureString_Objects(bool expected, string object1, string object2)
        {
            // ---
            // Act

            var secure1 = object1.DecryptToSecure();
            var secure2 = object2.DecryptToSecure();

            // ---
            // Log

            Console.WriteLine("Object1: {1}{0}Object2: {2}", 
                              Environment.NewLine, 
                              secure1.Unwrap(), 
                              secure2.Unwrap());
            // ------
            // Assert

            Assert.AreEqual(expected, secure1.Matches(secure2));
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==")]
        
        [DataRow("AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAACTO4HWsndsUdHeyvw573D/AAAAAASAAACgAAAAEAAAAFGodJgMFe9EwclV/hrg5xooAAAA1BOlyAOnqC7HDGv7CuU9tMtSnvKA2n8aIl6oFj2a/yZolFcnxdZUDhQAAAB03ekjjDGd91ngubJ7DjuQrVDqoA==",
                 "")]

        [DataRow("AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAACTO4HWsndsUdHeyvw573D/AAAAAASAAACgAAAAEAAAAFGodJgMFe9EwclV/hrg5xooAAAA1BOlyAOnqC7HDGv7CuU9tMtSnvKA2n8aIl6oFj2a/yZolFcnxdZUDhQAAAB03ekjjDGd91ngubJ7DjuQrVDqoA==",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA0AHectjngEe+C7MbSSJE4AQAAAACAAAAAAADZgAAwAAAABAAAAAQWZkCWteWDQiPDxNqNJEXAAAAAASAAACgAAAAEAAAAJ+CrnFVJgnfR0kHFWpdVrwoAAAAY90PxAjUuisOgdcwd7+7hlPcFO+WzbLC+Wq9UHdRnCP6ITvt0j1GLhQAAAD63uECAz0AFe5UN+CCjEsqn1hdig==")]
        public void Matches_Negative_Security_String_Extensions_Compares_An_Encrypted_SecureString_To_A_Null(string object1, string object2)
        {
            // -------
            // Arrange

            var secure1 = null as SecureString;
            var secure2 = null as SecureString;

            if(!string.IsNullOrEmpty(object1))
            {
                secure1 = object1.DecryptToSecure();
            }

            if(!string.IsNullOrEmpty(object2))
            {
                secure2 = object2.DecryptToSecure();
            }

            // ------
            // Assert

            Assert.IsFalse(secure1.Matches(secure2));
        }

        // ------------------------------------------------

        [TestMethod]
        public void Matches_Edge_Case_Security_String_Extensions_Compares_Two_Null_Values()
        {
            // -------
            // Arrange

            var secure1 = null as SecureString;
            var secure2 = null as SecureString;

            // ------
            // Assert

            Assert.IsTrue(secure1.Matches(secure2));
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("This is a string", "This is a very different string")]
        [DataRow("This is a very different string", "This is a string")]
        public void Matches_Edge_Case_Security_String_Extensions_Compares_Two_Different_Values(string thing1, string thing2)
        {
            // -------
            // Arrange

            var secure1 = thing1.ToSecureString();
            var secure2 = thing2.ToSecureString();

            // ------
            // Assert

            Assert.IsFalse(secure1.Matches(secure2));
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow("this is a password", "F456EFB8-1576-42A8-8394-8CDAB7D89B2F", "F456EFB8-1576-42A8-8394-8CDAB7D89B2F", true)]
        [DataRow("this is a password", "F456EFB8-1576-42A8-8394-8CDAB7D89B2F", "8FDEA673-258B-44A0-869C-05C1458580E3", false)]
        public void Entropy_Security_String_Extensions_Effects_Of_Entropy_On_Encryption(string testValue, string entropyIn, string entropyOut, bool expected)
        {
            // ---
            // Act

            var encryptedData = testValue.Encrypt(entropyIn);

            // ---
            // Log

            Console.WriteLine($"Input Entropy:{crt}{entropyIn}{cr}Output Entropy:{crt}{entropyOut}{cr}");
            Console.WriteLine($"TestValue:{crt}{testValue}{cr}UnWrapped SecureString:{crt}{encryptedData}{cr}");

            // ------
            // Assert

            try
            {
                var decrypted = encryptedData.Decrypt(entropyOut);
                Console.WriteLine($"Decrypted Value:{crt}{decrypted}");

                Assert.AreEqual(testValue, decrypted);
            }
            catch(Exception)
            {
                Assert.IsFalse(expected);
                Console.WriteLine($"Decrypted Value:{crt}<SUCCESSFULLY FAILED>");
            }
        }

        // ------------------------------------------------

        [TestMethod]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\RegCredTest\ASCII", "UserID", "E99965")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\AflacDataStoreTEST", "UserID", "E99965")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\ADLDSUnitTest", "UserID", @"AFLHQ\E99965")]
        [DataRow(@"HKEY_LOCAL_MACHINE\SOFTWARE\AflacApps\AflacDataStore\ASPNET_SETREG", "UserName", @"AFLHQ\E99965")]
        public void DecryptRegistryEntry_Security_String_Extensions_Decrypts_A_Value_Encrypted_Using_Krypto(string path, string nodeName, string expectedValue)
        {
            // ---
            // Log

            Console.WriteLine("Registry Path:{0}\t{1}{0}", Environment.NewLine, path);

            // ----------------------
            // Read from the Registry

            var val = Registry.GetValue(path, nodeName, string.Empty) as byte[];
            var encrypted = Convert.ToBase64String(val);

            // ---
            // Act

            var decrypted = encrypted.Decrypt();

            // ---
            // Log

            Console.WriteLine($"Expected:\t{expectedValue}{Environment.NewLine}Actual:\t\t{decrypted}");

            // ------
            // Assert

            Assert.IsTrue(expectedValue.Equals(decrypted));
        }
    }
}
