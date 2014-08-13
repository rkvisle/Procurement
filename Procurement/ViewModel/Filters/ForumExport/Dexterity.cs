using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    public class Dexterity : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }

        public Dexterity()
            : base("Dexterity", "Item with Dexterity", "DEXTERITY")
        { }
    }
}