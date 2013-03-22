using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Groundfloor.Facebook
{
    public sealed class Page
    {
        public long id { get; internal set; }
        public bool liked { get; internal set; }
        public bool admin { get; internal set; }

        internal Page(dynamic pg)
        {
            if (pg != null)
            {
                try { id = Convert.ToInt64(pg.id); }
                catch { id = 0; }

                try { liked = Convert.ToBoolean(pg.liked); }
                catch { liked = false; }

                try { admin = Convert.ToBoolean(pg.admin); }
                catch { admin = false; }
            }
            else
            {
                liked = admin = false;
            }
        }
    }
}
