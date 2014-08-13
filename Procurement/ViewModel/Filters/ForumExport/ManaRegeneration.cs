using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    internal class ManaRegeneration : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Attacks; }
        }

        public ManaRegeneration()
            : base("Mana Regeneration", "Mana Regeneration", "Mana Regeneration")
        { }
    }
}
