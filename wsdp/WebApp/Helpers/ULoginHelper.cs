using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.WebPages;

namespace WebApp.Helpers
{
    public class ULoginHelper
    {
        #region Public Methods and Operators

        public static ULoginUser GetULoginUser(string token, string serverName)
        {
            var url = string.Format("http://ulogin.ru/token.php?token={0}&host={1}", token, serverName);
            var getRequest = WebRequest.Create(url);

            using (var resp = getRequest.GetResponse())
            {
                using (var stream = resp.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            return new JavaScriptSerializer().Deserialize<ULoginUser>(sr.ReadToEnd());
                        }
                    }
                }
            }

            return null;
        }

        #endregion

        public class ULoginUser
        {
            #region Public Properties

            public string Email { get; set; }

            public string First_Name { get; set; }

            public string Identity { get; set; }

            public string Last_Name { get; set; }

            public string Network { get; set; }

            public string NickName { get; set; }

            public string Phone { get; set; }

            public string Profile { get; set; }

            public string Sex { get; set; }

            public string Uid { get; set; }

            #endregion
        }
        /// <summary>
        /// Generate new name for user via Firs_Name and Last_Name properties
        /// </summary>
        public static string GetName(ULoginUser user)
        {
            var name = String.Empty;

            if (user.First_Name != null) name = user.First_Name;
            if (user.Last_Name != null) name += " " + user.Last_Name;
            if (name.IsEmpty()) name = Guid.NewGuid().ToString();

            return name;
        }
    }
}