using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts._cityScripts
{
    public class Project
    {
        public enum Type {Public, Conquest};

        public Type type;
        internal int timeLimit = 5;
        public City city;

        public Project(City city)
        {
            this.city = city;
            city.projects.Add(this);
        }

        public void Complete()
        {
            city.projects.Remove(this);
        }
    }
}
