using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    public class Intelligence : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }

        public Intelligence()
            : base("Intelligence", "Item with Intelligence", "INTELLIGENCE")
        { }
    }
}
