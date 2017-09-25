using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace Helpers
{
   public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);


            try
            {
                entry.RefreshCache();
              
                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;

                    DirectorySearcher search = new DirectorySearcher(entry);

                    search.Filter = "(SAMAccountName=" + username + ")";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();

                    if (null == result)
                    {
                        return false;
                    }

                    //Update the new path to the user in the directory.
                    _path = result.Path;
                    _filterAttribute = (string)result.Properties["cn"][0];
              
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public string GetGroups()
        {

            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }

     
        public static string GetPrefijoPais(string countryCode)
        {
            string valor = "arg";
            try
            {
                if (!string.IsNullOrEmpty(countryCode))
                {
                    switch (countryCode.ToUpper())
                    {

                        case "AR":
                            valor = "arg";
                            break;
                        case "US":
                            valor = "usa";
                            break;
                        case "BR":
                            valor = "br";
                            break;
                        case "CL":
                            valor = "ch";
                            break;
                        case "CO":
                            valor = "co";
                            break;
                        case "CR":
                            valor = "cr";
                            break;
                        case "EC":
                            valor = "ec";
                            break;
                        case "SV":
                            valor = "sv";
                            break;
                        case "ES":
                            valor = "es";
                            break;
                        case "GT":
                            valor = "gu";
                            break;
                        case "MX":
                            valor = "mx";
                            break;
                        case "PY":
                            valor = "pa";
                            break;
                        case "PE":
                            valor = "pe";
                            break;
                        case "UY":
                            valor = "ur";
                            break;
                        case "NI":
                            valor = "ni";
                            break;
                    }
                }
                return valor;
            }
            catch (Exception ex)
            {
                return valor;

            }
        }
    }
}
