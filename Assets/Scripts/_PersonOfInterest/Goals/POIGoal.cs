using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts._cityScripts;

namespace Assets.Scripts._PersonOfInterest
{
    public abstract class POIGoal
    {
        public static List<POIGoal> _all = new List<POIGoal>();
        private int progress;
        private int required;
        public Reward goalReward;
        public PersonOfInterest holder;

        private int age;
        internal int timeLimit;

        //public Type type;
        //public enum Type { Title, Power, Wealth, Lackey, Event, Project}

        public abstract class Reward
        {
            public POIGoal goal;
            public int prestige;
            public int cost;
            public int tyrannyChange;

            public abstract void Apply();

        }

        public POIGoal(PersonOfInterest poi)
        {
            progress = 0;
            required = 100;
            age = 0;
            timeLimit = 10;
            holder = poi;

            _all.Add(this);
        }

        public virtual void Progress(int change)
        {
            progress += change;
            if (progress >= required)
            {
                Complete();
            }
        }

        public int getProgress()
        {
            return progress;
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
            _all.Remove(this);
            holder.ChooseNewGoal();
        }

        public static void Tick()
        {
            foreach (POIGoal goal in POIGoal._all.ToArray())
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
