using System;
using Assets.Scripts._PersonOfInterest;
using System.Collections.Generic;

namespace Assets.Scripts._cityScripts
{
    public class Lackey
    {
        public PersonOfInterest lord;
        public int skill = RandomCustom.instance.RollXdY(5,20);
        private POIGoal assignedTask;
        public static List<Lackey> _all = new List<Lackey>();
        private List<LackeyPersonality> personalities = new List<LackeyPersonality>();

        public Lackey(PersonOfInterest lord)
        {
            _all.Add(this);
            PledgeAllegianceTo(lord);
            AskForTask();
        }

        public void PledgeAllegianceTo(PersonOfInterest poi)
        {
            if (lord != null)
            {
                lord.lackeys.Remove(this);
            }
            lord = poi;
            lord.lackeys.Add(this);
            AskForTask();
        }

        public void AssignTask(POIGoal task)
        {
            assignedTask = task;
        }

        public static void Tick()
        {
            foreach (Lackey lackey in _all)
            {
                lackey.assignedTask.AddEffortPoints(lackey.skill);
                lackey.assignedTask.Progress(lackey.skill);
            }
        }

        private void AskForTask()
        {
            assignedTask = lord.currentGoal;
        }
    }
}