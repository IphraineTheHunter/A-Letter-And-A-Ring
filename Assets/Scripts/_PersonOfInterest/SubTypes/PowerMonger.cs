using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.Scripts._PersonOfInterest
{
    public class PowerMonger : PersonOfInterest
    {
        public PowerMonger(City city): base(city)
        {

        }

        public override POIGoal ChooseNewGoal()
        {
            if (city.activeEvents.Any(x => x.power > 60 && x.effect.powerChange < 0))
            {
                ActiveEvent chosenEvent = city.activeEvents.Where(x => x.power > 60 && x.effect.powerChange < 0)
                    .OrderBy(x => x.effect.powerChange).First();
                currentGoal = new EventGoal(this, chosenEvent);
            }
            else if (wealth < 20)
            {
                currentGoal = new WealthGoal(this, 180);
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
