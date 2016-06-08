using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest.Goals.SubTypes
{
    public class EventGoal : POIGoal
    {
        public ActiveEvent activeEvent;
        public EventGoal(PersonOfInterest poi, ActiveEvent activeEvent) : base(poi)
        {
            this.activeEvent = activeEvent;
            this.goalReward = new EventReward(this);
        }

        public class EventReward : POIGoal.Reward
        {
            public new EventGoal goal;

            public EventReward(EventGoal goal)
            {
                this.goal = goal;
            }

            public override void Apply()
            {
                goal.activeEvent.Reduce(25);
            }
        }
    }
}
