using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest.Goals.SubTypes
{
    public class WealthGoal : POIGoal
    {
        public int wealth;
        public WealthGoal(PersonOfInterest poi, int wealth) : base(poi)
        {
            this.wealth = wealth;
            this.goalReward = new WealthReward(this);
        }

        public class WealthReward : POIGoal.Reward
        {
            public new WealthGoal goal;

            public WealthReward(WealthGoal goal)
            {
                this.goal = goal;
            }

            public override void Apply()
            {
                goal.holder.wealth += goal.wealth;
            }
        }
    }
}
