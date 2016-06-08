using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.Scripts._PersonOfInterest
{
    public class Humane : PersonOfInterest
    {
        public Humane(City city) : base(city)
        {

        }

        public override POIGoal ChooseNewGoal()
        {
            if (city.activeEvents.Any(x => x.power > 40 && x.effect.wealthChange < 0))
            {
                ActiveEvent chosenEvent = city.activeEvents.Where(x => x.power > 40 && x.effect.wealthChange < 0).OrderBy(x => x.effect.wealthChange).First();
                currentGoal = new EventGoal(this, chosenEvent);
            }
            else if(city.title.holder.tyrany > 25)
            {
                currentGoal = new TitleGoal(this, city.title.holder.heldTitles.OrderBy(x => x.wealthGain).First());
            }
            else if (wealth < 20)
            {
                currentGoal = new WealthGoal(this, 180);
            }
            else if (city.projects.Any(x => x.type == Project.Type.Public))
            {
                //TODO: intelligently pick from Public projects
                currentGoal = new ProjectGoal(this, city.projects.Where(x => x.type == Project.Type.Public).First());
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
