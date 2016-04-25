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
        public List<Title> heldTitles = new List<Title>();
        public List<Lackey> lackeys = new List<Lackey>();

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
            POIGoal.Type goalType;
            if (affectedByEvents.Where(x => x.power > 70).Count() > 0)
            {
                goalType = POIGoal.Type.Event;
            }
            else if (wealth < 20)
            {
                goalType = POIGoal.Type.Wealth;
            }
            else
            {
                goalType = POIGoal.Type.Title;
            }
            currentGoal = new POIGoal(this, goalType);
            return currentGoal;
        }

        public Quest OfferNewQuest()
        {
            offeredQuest = new Quest(this);
            wealth -= offeredQuest.difficulty * 20;
            return offeredQuest;
        }
    }
}
