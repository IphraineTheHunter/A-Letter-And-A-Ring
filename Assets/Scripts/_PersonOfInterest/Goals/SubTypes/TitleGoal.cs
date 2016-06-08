using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest.Goals.SubTypes
{
    public class TitleGoal : POIGoal 
    {
        public Title title;
        public TitleGoal(PersonOfInterest poi, Title title) : base(poi)
        {
            this.title = title;
            this.goalReward = new TitleReward(this);
        }

        public class TitleReward : POIGoal.Reward
        {
            public new TitleGoal goal;

            public TitleReward(TitleGoal goal)
            {
                this.goal = goal;
            }

            public override void Apply()
            {
                goal.title.AwardTo(goal.holder);
            }
        }
    }
}
