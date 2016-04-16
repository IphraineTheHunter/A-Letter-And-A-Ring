using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class ActiveEvent
    {
        public ActiveEvent()
        {
            name = "Goblins";
            CityContext.context._events.Add(this);
            power = 70;
            foreach (PersonOfInterest poi in CityContext.context._pois)
            {
                poi.affectedByEvents.Add(this);
            }
        }

        public string name;
        public int power;

        public void End()
        {
            CityContext.context._events.Remove(this);
        }

        public static void Tick()
        {
            foreach (ActiveEvent activeEvent in CityContext.context._events.ToArray())
            {
                if (activeEvent.power < 100)
                {
                    activeEvent.power += 5;
                }
            }
            if (CityContext.context.random.RollXdY(1, 100) < 10)
            {
                ActiveEvent ae = new ActiveEvent();
                ae.name = "Toxite Riots";
            }
        }

        public void Reduce(int goalPower)
        {
            power -= goalPower;
            if (CityContext.context.random.RollXdY(1, 100) > power)
            {
                End();
            }
        }
    }

    
}
