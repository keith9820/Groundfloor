using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using Groundfloor.Facebook.Config;
using System.Web.Script.Serialization;

namespace Groundfloor.Facebook
{
    public sealed class FacebookFactory
    {
        public static FacebookInstance anon;

        public FacebookInstance GetInstance(FacebookConfigElement elem, NameValueCollection paramCollection)
        {
            string signed_request = paramCollection["signed_request"].Default().DecodeUrl();
            FacebookInstance instance = null;

            string json = null;
            if (!signed_request.isEmpty())
            {
                #region check the signature of the signed request
                string hash = signed_request.Part('.', 0).Decode64();
                json = signed_request.Part('.', 1).Decode64();

                string evidence = signed_request.Part('.', 1).HashForKey(elem.appSecret);
                if (hash.NotEquals(evidence))
                    throw new FormatException("Invalid Signature");
                #endregion
            }
            instance = new FacebookInstance(elem, json);
            instance.HttpParameters = paramCollection.ToDictionary();
            return instance;
        }    
    }
}
