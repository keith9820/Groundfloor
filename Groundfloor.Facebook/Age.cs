using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Groundfloor.Facebook
{
    public class Age
    {
        public int min { get; internal set; }

        internal Age(dynamic age)
        {
            if (age != null)
                min = Convert.ToInt32(age.min);
        }
    }

}
