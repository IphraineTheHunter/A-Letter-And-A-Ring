using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class POIGoal
    {
        private int progress;
        private int required;
        public int power;
        public POIGoalReward goalReward;
        public List<PersonOfInterest> involvedPeople = new List<PersonOfInterest>();
        public ActiveEvent eventToEliminate;

        private int age;
        private int timeLimit;

        public POIGoal()
        {

        }

        public POIGoal(PersonOfInterest poi)
        {
            progress = 0;
            required = 100;
            power = 25;
            age = 0;
            timeLimit = 10;
            involvedPeople.Add(poi);
            goalReward = new POIGoalReward(poi, this);

            CityContext.context._goals.Add(this);
        }

        public virtual void Progress(int change)
        {
            progress += change;
            if (progress >= required)
            {
                Complete();
            }
        }

        private void Complete()
        {
            goalReward.Apply();
            End();
        }

        private void Fail()
        {
            End();
        }

        private void End()
        {
            CityContext.context._goals.Remove(this);
            foreach (PersonOfInterest poi in involvedPeople)
            {
                poi.ChooseNewGoal();
            }
        }

        public static void Tick()
        {
            foreach (POIGoal goal in CityContext.context._goals.ToArray())
            {
                goal.age++;
                if (goal.age >= goal.timeLimit)
                {
                    goal.Fail();
                }
            }
        }
    }
}
