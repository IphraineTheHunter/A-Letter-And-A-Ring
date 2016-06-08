using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Scripts._cityScripts
{
    public class ActiveEvent
    {
        public static List<ActiveEvent> _all = new List<ActiveEvent>();

        public ActiveEvent(City city)
        {
            name = "Goblins";
            _all.Add(this);
            power = 70;
            this.city = city;
            city.activeEvents.Add(this);
            effect = new ActiveEventEffect();
        }

        public string name;
        public int power;
        public ActiveEventEffect effect;
        public City city;

        public void End()
        {
            _all.Remove(this);
            city.activeEvents.Remove(this);
        }

        public static void Tick()
        {
            foreach (ActiveEvent activeEvent in _all.ToArray())
            {
                if (activeEvent.power < 100)
                {
                    activeEvent.power += 5;
                }
            }
        }

        public void Reduce(int goalPower)
        {
            power -= goalPower;
            if (RandomCustom.instance.RollXdY(1, 100) > power)
            {
                End();
            }
        }
    }

    
}
