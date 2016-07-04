using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._PersonOfInterest;

namespace Assets.Scripts._cityScripts
{
    public class City
    {
        public List<PersonOfInterest> peopleOfInterest = new List<PersonOfInterest>();
        public List<ActiveEvent> activeEvents = new List<ActiveEvent>();
        public int averageWealth;
        public int disparity;
        public int piety;
        public string name;
        public Title title;
        public List<Project> projects = new List<Project>();

        public void Tick()
        {
            if (RandomCustom.instance.PercentChanceOfSuccess(7))
            {
                ActiveEvent ae = new ActiveEvent(this);
                ae.name = "Toxite Riots";
            }
        }
    }
}
