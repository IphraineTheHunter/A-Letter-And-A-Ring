using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class PersonOfInterest
    {
        public string name;
        public POIGoal currentGoal;
        public List<ActiveEvent> affectedByEvents = new List<ActiveEvent>();
        public int prestige;
        public int wealth;
        public Quest offeredQuest;

        public PersonOfInterest()
        {
            name = "PersonOfInterest" + CityContext.context._pois.Count;
            prestige = 100;
            wealth = 100;
            CityContext.context._pois.Add(this);
            foreach (ActiveEvent ae in CityContext.context._events)
            {
                affectedByEvents.Add(ae);
            }

            ChooseNewGoal();
            OfferNewQuest();
        }

        public Quest HireAdventurer()
        {
            Quest offeredQuest = new Quest();
            offeredQuest.offerer = this;
            return offeredQuest;
        }

        public POIGoal ChooseNewGoal()
        {
            currentGoal = new POIGoal(this);
            return currentGoal;
        }

        public Quest OfferNewQuest()
        {
            offeredQuest = new Quest(this);
            return offeredQuest;
        }
    }
}
