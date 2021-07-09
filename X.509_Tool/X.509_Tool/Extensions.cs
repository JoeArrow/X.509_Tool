#region © 2017 Aflac.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace X._509_Tool
{
    // ----------------------------------------------------
    /// <summary>
    ///     
    /// </summary>

    public static class Extensions
    {
        public static string ToDisplayString(this X509Certificate2 cert)
        {

            string outputFormat = "{0}:\t{1}\r\n";

            Type obj = cert.GetType();
            PropertyInfo[] properties = obj.GetProperties();

            StringBuilder sb = new StringBuilder();

            // ------------------------------------------
            // Step through the properties of the object.

            foreach(PropertyInfo prop in properties)
            {
                try
                {
                    object val = prop.GetValue(cert, null);
                    string propName = prop.Name;

                    // ---------------------------------------------------
                    // We only want to show properties which have a value.

                    if(val != null && val.ToString() != string.Empty)
                    {
                        sb.AppendFormat(outputFormat, propName, val.ToString());
                    }
                }
                catch(Exception ex)
                {
                    // --------------------------------------------------
                    // If we can't get the value for the object, at least
                    // we can report why...

                    sb.AppendFormat(outputFormat, prop.Name, ex.Message);
                }
            }

            return string.Format("{0}", sb.ToString());
        }
    }
}
