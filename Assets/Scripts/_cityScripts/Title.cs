using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._PersonOfInterest;

namespace Assets.Scripts._cityScripts
{
    public class Title
    {
        public static List<Title> _all = new List<Title>();

        public PersonOfInterest holder = null;
        public int wealthGain = 0;
        public string name = "";

        public Title(PersonOfInterest poi)
        {
            holder = poi;
            poi.heldTitles.Add(this);
            _all.Add(this);
        }

        public Title()
        {
            _all.Add(this);
        }

        public void AwardTo(PersonOfInterest newHolder)
        {
            if (holder != null)
            {
                holder.heldTitles.Remove(this);
            }
            holder = newHolder;
            newHolder.heldTitles.Add(this);
        }

        public static void Tick()
        {
            foreach (Title title in _all)
            {
                if (title.holder != null)
                {
                    title.holder.wealth += title.wealthGain;
                }
            }
        }
    }
}