using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    public class ManaLeech : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }

        public ManaLeech()
            : base("Mana Leech", "Items with Mana Leech", "LEECHED AS MANA")
        { }
    }
}
