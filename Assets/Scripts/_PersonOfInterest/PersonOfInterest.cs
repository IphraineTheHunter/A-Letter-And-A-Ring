using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._cityScripts.Quests;

namespace Assets.Scripts._PersonOfInterest
{
    public abstract class PersonOfInterest
    {
        public static List<PersonOfInterest> _all = new List<PersonOfInterest>();

        public string name;
        public POIGoal currentGoal;
        public City city;
        public int prestige;
        public int wealth;
        public Quest offeredQuest;
        public List<Title> heldTitles = new List<Title>();
        public List<Lackey> lackeys = new List<Lackey>();
        public List<PersonOfInterest> rivals = new List<PersonOfInterest>();
        public int tyrany;

        public PersonOfInterest(City city)
        {
            name = "PersonOfInterest" + _all.Count;
            prestige = 100;
            wealth = 400;
            _all.Add(this);
            this.city = city;

            ChooseNewGoal();
            OfferNewQuest();
        }

        public abstract POIGoal ChooseNewGoal();

        public Quest OfferNewQuest()
        {
            //ToDo: choose a varying difficulty
            offeredQuest = currentGoal.GetNextQuest(this, 100);
            return offeredQuest;
        }
    }
}
