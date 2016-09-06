using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._cityScripts.Quests;

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

        public override Quest GetNextQuest(PersonOfInterest poi, int difficulty)
        {
            Quest.Type type = getRandomQuestType(2, 1, 4, 6);
            Quest.Condition condition = Quest.Condition.Proof;
            return new Quest(poi, difficulty, type, condition);
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
                goal.title.wealthGain += goal.effortPoints / 10;
                goal.title.AwardTo(goal.holder);
            }
        }
    }
}
