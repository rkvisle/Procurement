using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    class PlusOneLevelAllGems : StatFilter
    {
        public PlusOneLevelAllGems() 
            : base("Plus Gem Level", "Plus to Level of gems in item", "to Level of Gems in this item")
        { }

        public override FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }

    }
}

