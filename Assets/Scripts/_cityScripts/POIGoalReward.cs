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

        public POIGoalReward(PersonOfInterest poi, POIGoal goal, ActiveEvent activeEvent)
        {
            prestige = 0;
            wealth = 0;
            lackey = null;
            title = null;
            this.goal = goal;
            relatedActiveEvents.Add(activeEvent);
            activeEventChanges.Add(activeEvent.power / 2);

        }

        public POIGoalReward(PersonOfInterest poi, POIGoal goal, Title title)
        {
            prestige = 0;
            wealth = 0;
            lackey = null;
            this.title = title;
            this.goal = goal;
        }

        public POIGoalReward(PersonOfInterest poi, POIGoal goal, Lackey lackey)
        {
            prestige = 0;
            wealth = 0;
            this.lackey = lackey;
            this.title = null;
            this.goal = goal;
        }

        public POIGoalReward(PersonOfInterest poi, POIGoal goal, POIGoal.Type type)
        {
            prestige = 0;
            wealth = 0;
            lackey = null;
            title = null;
            this.goal = goal;

            switch (type)
            {
                case POIGoal.Type.Event:
                    ActiveEvent ae = CityContext.context._events[CityContext.context.random.RollXdY(1, CityContext.context._events.Count)];
                    relatedActiveEvents.Add(ae);
                    activeEventChanges.Add(ae.power / 2);
                    break;
                case POIGoal.Type.Lackey:
                    lackey = new Lackey();
                    break;
                case POIGoal.Type.Power:
                    prestige = CityContext.context.random.RollXdY(4, 60);
                    break;
                case POIGoal.Type.Title:
                    title = new Title();
                    break;
                case POIGoal.Type.Wealth:
                    wealth = CityContext.context.random.RollXdY(4, 60);
                    prestige = wealth / 10;
                    break;
                default:
                    break;
            }
        }

        private void AwardTo(PersonOfInterest poi)
        {
            Debug.Log("NPC goal reward given to " + poi.name);
            poi.prestige += prestige;
            poi.wealth += wealth;
            if (title != null)
            {
                title.AwardTo(poi);
            }
            if (lackey != null)
            {
                lackey.PledgeAllegianceTo(poi);
            }
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
            }
        }
    }
}
