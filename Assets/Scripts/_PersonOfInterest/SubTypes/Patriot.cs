using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.Scripts._PersonOfInterest
{
    public class Patriot : PersonOfInterest
    {
        public Patriot(City city): base(city)
        {

        }

        public override POIGoal ChooseNewGoal()
        {
            if (city.projects.Any(x => x.type == Project.Type.Conquest)){
                //TODO: intelligently pick which open conquest project to join
                currentGoal = new ProjectGoal(this, city.projects.Where(x => x.type == Project.Type.Conquest).First());
            }
            else if (wealth < 150)
            {
                currentGoal = new WealthGoal(this);
            }
            else if (city.activeEvents.Any(x => x.power > 60))
            {
                currentGoal = new EventGoal(this, city.activeEvents.Where(x => x.power > 60).OrderByDescending(x => x.power).First());
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
