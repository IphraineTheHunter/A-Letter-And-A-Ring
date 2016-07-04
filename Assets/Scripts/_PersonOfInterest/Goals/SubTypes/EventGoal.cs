using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;
using Assets.Scripts._cityScripts.Quests;

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

        public override Quest GetNextQuest(PersonOfInterest poi, int difficulty)
        {
            Quest.Type type = getRandomQuestType(4, 4, 1, 2);
            Quest.Condition condition = Quest.Condition.Proof;
            return new Quest(poi, difficulty, type, condition);
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
                goal.activeEvent.Reduce(goal.effortPoints / 4);
            }
        }
    }
}
