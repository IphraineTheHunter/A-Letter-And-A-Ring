using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest.Goals.SubTypes
{
    public class ProjectGoal : POIGoal
    {
        public Project project;
        public ProjectGoal(PersonOfInterest poi, Project project) : base(poi)
        {
            this.project = project;
            this.timeLimit = project.timeLimit;
            this.goalReward = new ProjectReward(this);
        }

        public class ProjectReward : POIGoal.Reward
        {
            public new ProjectGoal goal;

            public ProjectReward(ProjectGoal goal)
            {
                this.goal = goal;
            }

            public override void Apply()
            {
                goal.holder.prestige += 200;
            }
        }
    }
}
