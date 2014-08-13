using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters.ForumExport
{
    internal class EnergyShield : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Attacks; }
        }

        public EnergyShield()
            : base("Energy Shield", "Energy Shield", "Energy Shield")
        { }
    }
}
