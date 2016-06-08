using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._PersonOfInterest;

namespace Assets.Scripts._cityScripts
{
    public class Quest
    {
        public static List<Quest> _all = new List<Quest>();

        public PersonOfInterest offerer;
        public int difficulty;

        public Quest()
        {
            Quest._all.Add(this);
            difficulty = 0;
        }

        public Quest(PersonOfInterest poi, int difficulty)
        {
            Quest._all.Add(this);
            this.difficulty = difficulty;
            offerer = poi;
            
            
        }

        public void Accept()
        {
            offerer.wealth -= difficulty * 5;
            CityContext.context._playerMap.wealth += difficulty * 5;
        }

        public void Complete()
        {
            offerer.currentGoal.Progress(difficulty);

            //reward player
            offerer.wealth -= difficulty * 20;
            CityContext.context._playerMap.wealth += difficulty * 20;

            CityContext.Tick();

            offerer.OfferNewQuest();

            Quest._all.Remove(this);
        }
    }
}
