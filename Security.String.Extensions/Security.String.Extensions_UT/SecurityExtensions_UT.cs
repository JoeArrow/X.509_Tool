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

        [TestMethod]
        [DataRow("jarr0w@afl", "")]
        [DataRow("THIS IS A PASSWORD", "")]
        [DataRow("this is a password", "")]
        [DataRow("B0GU5P@55W0rd", "Test Entropy")]
        [DataRow("this is a password", "32FDDE48-A0F3-4A92-BA60-F680F095B6BB")]
        public void Encrypt_Security_String_Extensions(string testValue, string entropy)
        {
            // ---
            // Act

            var cipher = testValue.Encrypt(entropy);

            // ---
            // Log

            Console.WriteLine($"Entropy:{crt}'{entropy}'{cr}" +
                              $"TestValue:{crt}{testValue}{cr}" +
                              $"TestValue Length:{crt}{testValue.Length}{cr}" +
                              $"Cipher Length:{crt}{cipher.Length}{cr}" +
                              $"Cipher:{crt}{cipher}");
            // ------
            // Assert

            Assert.AreNotEqual(testValue, cipher);
            Assert.IsTrue(cipher.Length > testValue.Length);
        }

        // ------------------------------------------------

        [TestMethod]
        public void Encrypt_Security_String_Extensions_Throws()
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
        public void IsNullOrEmpty_SecureString(bool expected, string testValue)
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
        [DataRow("jarr0w@afl", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAAby6gPfhe9xnDpkEAl4ijVAAAAAASAAACgAAAAEAAAAHiPqNLl+LeesM/ICTV40rIYAAAAsUJTGjIYUlXnAWzPyE7tEyZuVARnJ3KLFAAAAPmN39gyEjiOWQgIcmdHuBcp9AZU", "")]
        [DataRow("this is a password", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAACBl8LhZMi0PJfV/dlZ1jxxAAAAAASAAACgAAAAEAAAABXNZy3sLWVOKL2qwb3J6WwoAAAAr4aUNrtbTD6HoMi2PsYKmhr8ZVed2CDCqRgBjRx+rXLCioaIL1zTgBQAAACCZr7xGkwfp45lh34bPJGn33Llqw==", "")]
        [DataRow("this is a password", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAABqgE8p+JRDKzGHhGlZQDgvAAAAAASAAACgAAAAEAAAAIXLH6g3suBPX9ye6dHR/isoAAAAutXvhER0joXj6CO8B0U59kbF1s75esXhFaI67EkZwpeE+vgCh0FYVRQAAABHHe74lV2vsG20ZukfudUVzUh8cw==", "32FDDE48-A0F3-4A92-BA60-F680F095B6BB")]
        public void Decrypt_Security_String_Extensions(string expected, string encryptedVal, string entropy)
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
        public void Decrypt_Security_String_Extensions_Throws(string entropy)
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
        public void ToSecureString_Security_String_Extensions(string testValue)
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
        public void Encrypt_SecureString(string testValue, string entropy)
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
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAACBl8LhZMi0PJfV/dlZ1jxxAAAAAASAAACgAAAAEAAAABXNZy3sLWVOKL2qwb3J6WwoAAAAr4aUNrtbTD6HoMi2PsYKmhr8ZVed2CDCqRgBjRx+rXLCioaIL1zTgBQAAACCZr7xGkwfp45lh34bPJGn33Llqw==")]

        [DataRow("32FDDE48-A0F3-4A92-BA60-F680F095B6BB",
                 "this is a password",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAABqgE8p+JRDKzGHhGlZQDgvAAAAAASAAACgAAAAEAAAAIXLH6g3suBPX9ye6dHR/isoAAAAutXvhER0joXj6CO8B0U59kbF1s75esXhFaI67EkZwpeE+vgCh0FYVRQAAABHHe74lV2vsG20ZukfudUVzUh8cw==")]
        public void DecryptToSecureString_Security_String_Extensions(string entropy, string expected, string testValue)
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
        public void DecryptToSecureString_Security_String_Extensions_Throws()
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
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAC9gF1p+A9r6dTrHhZ1glugAAAAAASAAACgAAAAEAAAAG6atuu2uaw2+PoX67sNyiEwAAAAUEb8YJrg3Ep1PUvXqLKAAHNlnZ/P0abSt6NJ0Dx6257Av2JZFbpzQ20zP+k8oUeSFAAAAC7lQiVe7adagMQjRzvxXkJ5QtgB",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAB3C0CXxCAoBvMWRF+AWWwdAAAAAASAAACgAAAAEAAAAIbzfSPB7/4Bn47HpBiXqxE4AAAAFXLNWeZ+0r1ZEpPY4usCoNMUOE7FQrJyFE6LQrupW05nf7vAFmu4iqVQRmpkZ6PbSXTEGj8+BY0UAAAA67uF41D4JbEIEvUkb3+xo/LSdBg=")]

        [DataRow(true,
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAACBl8LhZMi0PJfV/dlZ1jxxAAAAAASAAACgAAAAEAAAABXNZy3sLWVOKL2qwb3J6WwoAAAAr4aUNrtbTD6HoMi2PsYKmhr8ZVed2CDCqRgBjRx+rXLCioaIL1zTgBQAAACCZr7xGkwfp45lh34bPJGn33Llqw==",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAACBl8LhZMi0PJfV/dlZ1jxxAAAAAASAAACgAAAAEAAAABXNZy3sLWVOKL2qwb3J6WwoAAAAr4aUNrtbTD6HoMi2PsYKmhr8ZVed2CDCqRgBjRx+rXLCioaIL1zTgBQAAACCZr7xGkwfp45lh34bPJGn33Llqw==")]

        [DataRow(true,
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAB3C0CXxCAoBvMWRF+AWWwdAAAAAASAAACgAAAAEAAAAIbzfSPB7/4Bn47HpBiXqxE4AAAAFXLNWeZ+0r1ZEpPY4usCoNMUOE7FQrJyFE6LQrupW05nf7vAFmu4iqVQRmpkZ6PbSXTEGj8+BY0UAAAA67uF41D4JbEIEvUkb3+xo/LSdBg=",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAB3C0CXxCAoBvMWRF+AWWwdAAAAAASAAACgAAAAEAAAAIbzfSPB7/4Bn47HpBiXqxE4AAAAFXLNWeZ+0r1ZEpPY4usCoNMUOE7FQrJyFE6LQrupW05nf7vAFmu4iqVQRmpkZ6PbSXTEGj8+BY0UAAAA67uF41D4JbEIEvUkb3+xo/LSdBg=")]
        public void Matches_SecureString(bool expected, string object1, string object2)
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
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAB3C0CXxCAoBvMWRF+AWWwdAAAAAASAAACgAAAAEAAAAIbzfSPB7/4Bn47HpBiXqxE4AAAAFXLNWeZ+0r1ZEpPY4usCoNMUOE7FQrJyFE6LQrupW05nf7vAFmu4iqVQRmpkZ6PbSXTEGj8+BY0UAAAA67uF41D4JbEIEvUkb3+xo/LSdBg=")]
        
        [DataRow("AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAB3C0CXxCAoBvMWRF+AWWwdAAAAAASAAACgAAAAEAAAAIbzfSPB7/4Bn47HpBiXqxE4AAAAFXLNWeZ+0r1ZEpPY4usCoNMUOE7FQrJyFE6LQrupW05nf7vAFmu4iqVQRmpkZ6PbSXTEGj8+BY0UAAAA67uF41D4JbEIEvUkb3+xo/LSdBg=",
                 "")]

        [DataRow("AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAB3C0CXxCAoBvMWRF+AWWwdAAAAAASAAACgAAAAEAAAAIbzfSPB7/4Bn47HpBiXqxE4AAAAFXLNWeZ+0r1ZEpPY4usCoNMUOE7FQrJyFE6LQrupW05nf7vAFmu4iqVQRmpkZ6PbSXTEGj8+BY0UAAAA67uF41D4JbEIEvUkb3+xo/LSdBg=",
                 "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiJxHNd4RvUWFwpk4CSGfFgQAAAACAAAAAAADZgAAwAAAABAAAAC9gF1p+A9r6dTrHhZ1glugAAAAAASAAACgAAAAEAAAAG6atuu2uaw2+PoX67sNyiEwAAAAUEb8YJrg3Ep1PUvXqLKAAHNlnZ/P0abSt6NJ0Dx6257Av2JZFbpzQ20zP+k8oUeSFAAAAC7lQiVe7adagMQjRzvxXkJ5QtgB")]
        public void Matches_SecureString_Negative(string object1, string object2)
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
        public void Matches_SecureString_Null_Values()
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
        [DataRow("This is a string", "This is A string")]
        [DataRow("This is a string", "This is a strinG")]
        [DataRow("This is a string", "This is a very different string")]
        [DataRow("This is a very different string", "This is a string")]
        public void Matches_SecureString_Different_Values(string thing1, string thing2)
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
        public void Encrypt_Security_String_Extensions_With_Entropy(string testValue, string entropyIn, string entropyOut, bool expected)
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
        public void DecryptRegistryEntry_Security_String_Extensions(string path, string nodeName, string expectedValue)
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
