using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.Scripts._PersonOfInterest
{
    public class Pride : PersonOfInterest
    {
        public Pride(City city): base(city)
        {

        }

        public override POIGoal ChooseNewGoal()
        {
            if (city.activeEvents.Any(x => x.power > 60))
            {
                ActiveEvent chosenEvent = city.activeEvents.Where(x => x.power > 60).OrderByDescending(x => x.power).First();
                currentGoal = new EventGoal(this, chosenEvent);
            }
            else if (wealth < 150)
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
