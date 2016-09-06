using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts.Quests;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest.Goals.SubTypes
{
    public class WealthGoal : POIGoal
    {
        public WealthGoal(PersonOfInterest poi) : base(poi)
        {
            this.goalReward = new WealthReward(this);
        }

        public override Quest GetNextQuest(PersonOfInterest poi, int difficulty)
        {
            Quest.Type type = getRandomQuestType(3, 1, 5, 2);
            Quest.Condition condition = Quest.Condition.Proof;
            return new Quest(poi, difficulty, type, condition);
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
                goal.holder.wealth += goal.effortPoints * 30;
            }
        }
    }
}
