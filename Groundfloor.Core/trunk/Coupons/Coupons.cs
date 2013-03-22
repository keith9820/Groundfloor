
namespace Groundfloor
{
    public class Coupons
    {
        public static string Url { get; set; }

        // www.couponsinc.com - WebBrick(TM) Logic
        // http://bricks.coupons.com/TAF.asp?eb=1&o=87922&c=KR&p=100000065815687&cpt=tm1XyBtunF569tDUHXpT&cista=202
        /// Returns the encrypted pin code, offercode, short CipherKey and long CipherKey.
        /// <param name=”pinCode”>User’s unique identifier, as assigned by the client.</param>
        /// <param name=”offerCode”>Offer code for the coupon, as assigned by Coupons, Inc..</param>
        /// <param name=”shortKey”>Short CipherKey for the coupon, as assigned by Coupons, Inc..</param>
        /// <param name=”longKey”>Long CipherKey for the coupon, as assigned by Coupons, Inc..</param>
        /// <returns>An encrypted string, also known as the CPT parameter.</returns>
        internal static string EncodeCPT(string pinCode, int offerCode, string shortKey, string longKey)
        {
            string decodeX = " abcdefghijklmnopqrstuvwxyz0123456789!$%()*+,-.@;<=>?[]^_{|}~";
            int[] encodeModulo = new int[256];
            int[] vob = new int[2];
            int ocode = (offerCode.ToString().Length == 5) ? offerCode % 10000 : offerCode;

            vob[0] = ocode % 100;
            vob[1] = (ocode - vob[0]) / 100;

            for (int i = 0; i < 61; i++)
                encodeModulo[(int)char.Parse(decodeX.Substring(i, 1))] = i;

            pinCode = pinCode.ToLower() + offerCode.ToString();

            if (pinCode.Length < 20)
            {
                pinCode = pinCode + " couponsincproduction";
                pinCode = pinCode.Substring(0, 20);
            }

            int q = 0;
            int j = pinCode.Length;
            int k = shortKey.Length;
            int s1, s2, s3;

            System.Text.StringBuilder cpt = new System.Text.StringBuilder();
            for (int i = 0; i < j; i++)
            {
                s1 = encodeModulo[(int)char.Parse(pinCode.Substring(i, 1))];
                s2 = 2 * encodeModulo[(int)char.Parse(shortKey.Substring(i % k, 1))];
                s3 = vob[i % 2];
                q = (q + s1 + s2 + s3) % 61;
                cpt.Append(longKey.Substring(q, 1));
            }
            return cpt.ToString();
        }

        public static class Html
        {
            public static string WriteLink(long userId, int pinCode, string shortKey, string longKey, string linkText)
            {
                return string.Format("<a href='{0}?eb=1&o={1}&c=KR&p={2}&cpt={3}&cista=202'>{4}</a>"
                                        , Url
                                        , pinCode
                                        , userId
                                        , EncodeCPT(userId.ToString(), pinCode, shortKey, longKey)
                                        , linkText
                                    );
            }
        }

        static Coupons()
        {
            Url = "http://bricks.coupons.com/TAF.asp";
        }
    }
}