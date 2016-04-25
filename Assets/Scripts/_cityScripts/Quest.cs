using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Quest
    {
        public PersonOfInterest offerer;
        public int difficulty;

        public Quest()
        {
            CityContext.context._quests.Add(this);
            difficulty = 25;
        }

        public Quest(PersonOfInterest poi)
        {
            CityContext.context._quests.Add(this);
            difficulty = 25;
            offerer = poi;
        }

        public void Complete()
        {
            offerer.currentGoal.Progress(difficulty);

            //reward player
            offerer.wealth -= difficulty * 80;
            CityContext.context._playerMap.wealth += difficulty * 80;

            CityContext.Tick();

            offerer.OfferNewQuest();

            CityContext.context._quests.Remove(this);
        }
    }
}
