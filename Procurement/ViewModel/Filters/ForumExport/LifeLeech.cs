using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    public class LifeLeech : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }

        public LifeLeech()
            : base("Life Leech", "Items with Life Leech", "LEECHED AS LIFE")
        { }
    }
}