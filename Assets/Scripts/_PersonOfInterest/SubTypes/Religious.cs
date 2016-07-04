using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.Scripts._PersonOfInterest
{
    public class Religious : PersonOfInterest
    {
        public Religious(City city): base(city)
        {

        }

        public override POIGoal ChooseNewGoal()
        {
            if (city.activeEvents.Any(x => x.power > 40 && x.effect.pietyChange < 0))
            {
                ActiveEvent chosenEvent = city.activeEvents.Where(x => x.power > 40 && x.effect.pietyChange < 0)
                    .OrderBy(x => x.effect.pietyChange).First();
                currentGoal = new EventGoal(this, chosenEvent);
            }
            else if (wealth < 20)
            {
                currentGoal = new WealthGoal(this);
            }
            else
            {
                //TODO: intelligently pick from existing titles
                currentGoal = new TitleGoal(this, new Title());
            }
            return currentGoal;
        }
    }
}
