using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Groundfloor.Facebook
{
    public class User
    {
        public ulong id { get; internal set; }
        public string country { get; internal set; }
        public string locale { get; internal set; }
        public Age age { get; internal set; }

        internal User(dynamic fb)
        {
            if (fb != null && fb.user != null)
            {
                try { country = fb.user.country; }
                catch { country = ""; }

                try { locale = fb.user.locale; }
                catch { locale = ""; }

                try { id = Convert.ToUInt64(fb.user_id); }
                catch { id = 0; }

                age = new Age(fb.user.age);
            }
            else
            {
                age = new Age(null);
            }
        }
    }
}
