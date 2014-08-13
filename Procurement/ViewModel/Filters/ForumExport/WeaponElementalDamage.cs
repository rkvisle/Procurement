using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters
{
    internal class WeaponElementalDamage : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Attacks; }
        }

        public WeaponElementalDamage()
            : base("Elemental Damage With Weapons", "Elemental Damage With Weapons", "Elemental Damage With Weapons")
        { }
    }
}
