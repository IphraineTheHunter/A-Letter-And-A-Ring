using System;
using Assets.Scripts._PersonOfInterest;

namespace Assets.Scripts._cityScripts
{
    public class Lackey
    {
        PersonOfInterest lord;

        internal void PledgeAllegianceTo(PersonOfInterest poi)
        {
            if (lord != null)
            {
                lord.lackeys.Remove(this);
            }
            lord = poi;
            lord.lackeys.Add(this);
        }
    }
}