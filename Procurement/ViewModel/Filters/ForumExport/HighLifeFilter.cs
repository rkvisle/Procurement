using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POEApi.Model;
using System.Text.RegularExpressions;
using Procurement.ViewModel.Filters.ForumExport;

namespace Procurement.ViewModel.Filters
{
    public class HighLifeFilter : IFilter
    {
        public HighLifeFilter()
        { }

        public bool CanFormCategory
        {
            get { return false; }
        }

        public string Keyword
        {
            get { return "High Maximum Life"; }
        }

        public string Help
        {
            get { return "Jewelery with life >=55 others >= 85"; }
        }
        
        public bool Applicable(Item item)
        {
            if (item.Explicitmods == null || !(item is Gear))
                return false;

            int life = 0;
            Gear gear = item as Gear;
            if (gear != null)
            {
                if (gear.Implicitmods != null)
                {
                    foreach (var mod in gear.Implicitmods)
                    {
                        if (mod.Contains("to maximum Life"))
                        {
                            life += Int32.Parse(Regex.Match(mod, @"(\d+)").Value);
                        }
                    }
                }

                foreach (var mod in item.Explicitmods)
                {
                    if (mod.Contains("to maximum Life"))
                    {
                        //Amulets,Rings
                        life += Int32.Parse(Regex.Match(mod, @"(\d+)").Value);
                    }
                }

                if (gear.GearType == GearType.Amulet || gear.GearType == GearType.Ring)
                {
                    if (life >= 55)
                        return true;
                }
                else
                {
                    if (life >= 85)
                        return true;
                }
            }

            return false;
        }

        public FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }
    }
}
