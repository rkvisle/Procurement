using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters.ForumExport
{
    internal class Accuracy : StatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.Attacks; }
        }

        public Accuracy()
            : base("Accuracy", "Accuracy", "Accuracy")
        { }
    }
}

