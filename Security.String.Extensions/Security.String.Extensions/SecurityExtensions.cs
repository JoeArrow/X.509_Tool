#region Copyright © 2018 Aflac Inc.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Linq;
using System.Text;
using System.Security;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

// ------------------------------------------------------------------
// I want to give credit where credit is due: 
// This work is a mixture of my own work and that of:
// Philipp Sumi - https://www.codeproject.com/Members/philippsumi or 
//                http://www.hardcodet.net.
// 
// I found it on Codeproject:
//
// https://www.codeproject.com/Articles/36449/String-Encryption-using-DPAPI-and-Extension-Method

[assembly: CLSCompliant(true)]
namespace Security.String.Extensions
{
    public static class SecurityExtensions
    {
        // ------------------------------------------------
        /// <summary>
        ///     Specifies the data protection scope of the DPAPI.
        /// </summary>

        private const DataProtectionScope Scope = DataProtectionScope.CurrentUser;

        // ------------------------------------------------
        /// <summary>
        ///     Encrypts a given string and returns the 
        ///     encrypted data as a base64 string.
        /// </summary>
        /// <param name="plainText">
        ///     An plaintext string that needs to be secured.
        /// </param>
        /// <param name="optionalEntropy">
        ///     An optional string which will be split into bytes
        ///     and used as entropy, if it has a value.
        /// </param>
        /// <returns>
        ///     A base64 encoded string that represents the 
        ///     encrypted binary data.
        /// </returns>
        /// <remarks>
        ///     This solution is not really secure since we are
        ///     keeping strings in memory. If runtime protection 
        ///     is essential, <see cref="SecureString"/> should be used.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="plainText"/> is a null reference.
        /// </exception>

        public static string Encrypt(this string plainText, string optionalEntropy = null)
        {
            if(string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText), "The parameter 'plainText' must not be null or empty");
            }

            // -------------------------------------------------------
            // Get byte arrays from the plainText and Optional Entropy

            var data = Encoding.Unicode.GetBytes(plainText);
            var entropy = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.Unicode.GetBytes(optionalEntropy);

            // ----------------
            // Encrypt the data

            var encrypted = ProtectedData.Protect(data, entropy, Scope);

            // -----------------------
            // return as base64 string

            return Convert.ToBase64String(encrypted);
        }

        // ------------------------------------------------
        /// <summary>
        ///     Decrypts a given string.
        /// </summary>
        /// <param name="cipher">
        ///     A base64 encoded string that was created
        ///     through the <see cref="Encrypt(string)"/> or
        ///     <see cref="Encrypt(SecureString)"/> extension methods.
        /// </param>
        /// <returns>The decrypted string.</returns>
        /// <remarks>
        ///     Keep in mind that the decrypted string remains in memory
        ///     and makes your application vulnerable per se. If runtime 
        ///     protection is essential, <see cref="SecureString"/> should be used.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="cipher"/> is a null reference or an empty string.
        /// </exception>

        public static string Decrypt(this string cipher, string optionalEntropy = null)
        {
            string retVal;
            byte[] data;

            if(string.IsNullOrEmpty(cipher))
            {
                throw new ArgumentNullException(nameof(cipher), "The parameter 'cipher' may not be null or empty");
            }

            // ---------------------------------------------------------------
            // Get a byte array from the encrypted cipher and Optional Entropy

            data = Convert.FromBase64String(cipher);

            var entropy = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.Unicode.GetBytes(optionalEntropy);

            // ----------------
            // decrypt the data

            var decrypted = ProtectedData.Unprotect(data, entropy, Scope);

            // -----------------------
            // Assume Unicode first...

            retVal = Encoding.Unicode.GetString(decrypted);

            // ------------------------------------------
            // Watch for encrypted Non-Unicode strings...

            if(!IsUicodeString(retVal))
            {
                retVal = Encoding.ASCII.GetString(decrypted);
            }

            return retVal;
        }

        // ------------------------------------------------
        /// <summary>
        ///     Encrypts the contents of a secure string.
        /// </summary>
        /// <param name="value">
        ///     An unencrypted string that needs to be secured.
        /// </param>
        /// <returns>
        ///     A base64 encoded string that represents the 
        ///     encrypted binary data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="value"/> is a null reference.
        /// </exception>

        public static string Encrypt(this SecureString value, string optionalEntropy = null)
        {
            string retVal = null;

            if(!value.IsNullOrEmpty())
            {
                var ptr = Marshal.SecureStringToCoTaskMemUnicode(value);

                try
                {
                    var buffer = new char[value.Length];
                    Marshal.Copy(ptr, buffer, 0, value.Length);

                    var data = Encoding.Unicode.GetBytes(buffer);
                    var entropy = string.IsNullOrEmpty(optionalEntropy) ? null : Encoding.Unicode.GetBytes(optionalEntropy);
                    var encrypted = ProtectedData.Protect(data, entropy, Scope);

                    // -----------------------
                    // return as base64 string

                    retVal = Convert.ToBase64String(encrypted);
                }
                finally
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(ptr);
                }
            }

            return retVal;
        }

        // ------------------------------------------------
        /// <summary>
        ///     Decrypts a base64 encoded encrypted string 
        ///     and returns the decrpyted data wrapped into a 
        ///     <see cref="SecureString"/> instance.
        /// </summary>
        /// <param name="cipher">
        ///     A base64 encoded string that was created
        ///     through the <see cref="Encrypt(string)"/> or
        ///     <see cref="Encrypt(SecureString)"/> extension methods.
        /// </param>
        /// <returns>
        ///     The decrypted string, wrapped into a
        ///     <see cref="SecureString"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="cipher"/> is a null reference.
        /// </exception>

        public static SecureString DecryptToSecure(this string cipher, string optionalEntropy = null)
        {
            var retVal = new SecureString();

            if(string.IsNullOrEmpty(cipher))
            {
                throw new ArgumentNullException(nameof(cipher), "The parameter 'cipher' must not be null");
            }

            // -------------------------
            // First, decrypt the cipher

            var decrypted = Decrypt(cipher, optionalEntropy);

            // ------------------------------------------
            // Add each character of the decrypted string
            // to a Secure String

            foreach(var chr in decrypted)
            {
                retVal.AppendChar(chr);
            }

            // -----------------
            // mark as read-only

            retVal.MakeReadOnly();
            return retVal;
        }

        // ------------------------------------------------
        /// <summary>
        ///     Wraps a managed string into a <see cref="SecureString"/> 
        ///     instance.
        /// </summary>
        /// <param name="value">
        ///     A string or char sequence that should be encapsulated.
        /// </param>
        /// <returns>
        ///     A <see cref="SecureString"/> that encapsulates the
        ///     submitted value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="value"/> is a null reference.
        /// </exception>

        public static SecureString ToSecureString(this IEnumerable<char> value)
        {
            var retVal = new SecureString();

            if(value != null)
            {
                var charArray = value.ToArray();

                foreach(var ch in charArray)
                {
                    retVal.AppendChar(ch);
                }
            }

            retVal.MakeReadOnly();
            return retVal;
        }

        // ------------------------------------------------
        /// <summary>
        ///     Unwraps the contents of a secured string and
        ///     returns the contained value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Be aware that the unwrapped managed string 
        ///     can be extracted from memory.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="value"/> is a null reference.
        /// </exception>

        public static string Unwrap(this SecureString value)
        {
            var ptr = Marshal.SecureStringToCoTaskMemUnicode(value);

            try
            {
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Marshal.ZeroFreeCoTaskMemUnicode(ptr);
            }
        }

        // ------------------------------------------------
        /// <summary>
        ///     Checks whether a <see cref="SecureString"/> is either
        ///     null or has a <see cref="SecureString.Length"/> of 0.
        /// </summary>
        /// <param name="value">
        ///     The secure string to be inspected.
        /// </param>
        /// <returns>
        ///     True if the string is either null or empty.
        /// </returns>

        public static bool IsNullOrEmpty(this SecureString value)
        {
            return value == null || value.Length == 0;
        }

        // ------------------------------------------------
        /// <summary>
        ///     Performs byte-wise comparison of two secure 
        ///     strings.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns>
        ///     True if the strings are equal.
        /// </returns>

        public static bool Matches(this SecureString value, SecureString other)
        {
            var retVal = false;

            if(value.IsNullOrEmpty() && other.IsNullOrEmpty())
            {
                retVal = true;
            }
            else if(value.IsNullOrEmpty() || other.IsNullOrEmpty() ||
                    value.Length != other.Length)
            {
                retVal = false;
            }
            else
            {
                var ptrA = Marshal.SecureStringToCoTaskMemUnicode(value);
                var ptrB = Marshal.SecureStringToCoTaskMemUnicode(other);

                try
                {
                    // -------------------
                    // If we get this far,
                    // we can be more optimistic.

                    retVal = true;

                    // ---------------------------
                    // parse characters one by one
                    // This doesn't change the fact that 
                    // we have them in memory however...

                    byte byteA = 1;
                    byte byteB = 1;

                    int index = 0;

                    while(index < value.Length * 2)
                    {
                        byteA = Marshal.ReadByte(ptrA, index);
                        byteB = Marshal.ReadByte(ptrB, index);

                        if(byteA != byteB)
                        {
                            // -------------------------------
                            // But if one byte doesn't match,
                            // then we are done.

                            retVal = false;
                            break;
                        }

                        index += sizeof(byte);
                    }
                }
                finally
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(ptrA);
                    Marshal.ZeroFreeCoTaskMemUnicode(ptrB);
                }
            }

            return retVal;
        }

        // ------------------------------------------------
        /// <summary>
        ///     This method is here to deal with a legacy 
        ///     situation.
        ///     Using the DPAPI_Wrapper, the client had a 
        ///     choice of using Unicode during the encryption
        ///     (ASPNET_SETREG) and Not using Unicode.
        ///     
        ///     This method allows the Security String Extension
        ///     to seamlessly decrypt either way. The client does
        ///     not need to care whether or not Unicode was used.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public static bool IsUicodeString(string input)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);

            return asciiBytesCount == unicodBytesCount;
        }
    }
}
