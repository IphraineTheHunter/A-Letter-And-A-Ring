using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest
{
    public class Greedy : PersonOfInterest
    {
        public Greedy(City city) : base(city)
        {

        }
        public override POIGoal ChooseNewGoal()
        {
            if (wealth < 100)
            {
                currentGoal = new WealthGoal(this, 300);
            }
            else if (city.activeEvents.Any(x => x.power * x.effect.wealthChange < -300))
            {
                ActiveEvent chosenEvent = city.activeEvents.Where(x => x.power * x.effect.wealthChange < -300).OrderBy(x => x.effect.wealthChange).First();
                currentGoal = new EventGoal(this, chosenEvent);
            }
            else
            {
                //TODO: intelligently pick from existing Titles
                currentGoal = new TitleGoal(this, new Title());
            }
            return currentGoal;
        }
    }
}
