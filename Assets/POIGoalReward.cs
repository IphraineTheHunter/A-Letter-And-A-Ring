using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class POIGoalReward
    {
        public POIGoal goal;

        public int prestige;
        public int wealth;
        public Lackey lackey;
        public Title title;
        public List<ActiveEvent> relatedActiveEvents = new List<ActiveEvent>();
        public List<int> activeEventChanges = new List<int>();
        
        public POIGoalReward()
        {

        }

        public POIGoalReward(PersonOfInterest poi, POIGoal iGoal)
        {
            prestige = 0;
            wealth = 0;
            lackey = null;
            title = null;
            goal = iGoal;
            if (poi.affectedByEvents.Any())
            {
                ActiveEvent ae = poi.affectedByEvents.First();
                relatedActiveEvents.Add(ae);
                activeEventChanges.Add(ae.power);
            }
            
        }

        private void AwardTo(PersonOfInterest poi)
        {
            Debug.Log("NPC goal reward given to " + poi.name);
            poi.prestige += prestige;
            poi.wealth += wealth;
        }

        public virtual void Apply()
        {
            for (int index = 0; index < relatedActiveEvents.Count; index++)
            {
                relatedActiveEvents[index].Reduce(activeEventChanges[index]);
            }
            foreach (PersonOfInterest poi in goal.involvedPeople)
            {
                AwardTo(poi);
                poi.ChooseNewGoal();
            }
        }
    }
}
