using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts._PersonOfInterest;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.Scripts._cityScripts.Quests
{
    public class Quest
    {
        public static List<Quest> _all = new List<Quest>();

        public PersonOfInterest offerer;
        public int difficulty;
        public string name;
        public enum Type { Deliver, Eliminate, Obtain, Persuade}
        public enum Condition { Stealthily, Protect, Publicly, Proof}
        public Type type;
        public Condition condition;
        
        public Quest()
        {
            Quest._all.Add(this);
            difficulty = 100;
            name = GenerateName();
            type = Type.Deliver;
            condition = Condition.Stealthily;
        }

        public Quest(PersonOfInterest poi, int difficulty, Type type, Condition condition)
        {
            Quest._all.Add(this);
            this.difficulty = difficulty;
            offerer = poi;
            this.type = type;
            this.condition = condition;
            name = GenerateName();
            
            
        }

        public void Accept()
        {
            offerer.wealth -= difficulty * 1;
            CityContext.context._playerMap.wealth += difficulty * 1;
        }

        public void Complete()
        {
            offerer.currentGoal.Progress(difficulty);

            //reward player
            offerer.wealth -= difficulty * 5;
            CityContext.context._playerMap.wealth += difficulty * 5;

            offerer.currentGoal.AddEffortPoints(difficulty);

            CityContext.Tick();

            offerer.OfferNewQuest();

            Quest._all.Remove(this);
        }

        private string GenerateName() {
            switch (type)
            {
                case Type.Deliver:
                    if (offerer.currentGoal is EventGoal)
                    {
                        return "Deliver weapon shipment";
                    }
                    else if (offerer.currentGoal is ProjectGoal)
                    {
                        return "Deliver building supplies";
                    }
                    else if (offerer.currentGoal is TitleGoal)
                    {
                        return "Deliver paperwork";
                    }
                    else if (offerer.currentGoal is WealthGoal)
                    {
                        return "Deliver trade goods";
                    }
                    else
                    {
                        return "ERROR 614";
                    }
                case Type.Eliminate:
                    if (offerer.currentGoal is EventGoal)
                    {
                        EventGoal goal = (EventGoal)offerer.currentGoal;
                        return "Clear den of " + goal.activeEvent.name;
                    }
                    else if (offerer.currentGoal is ProjectGoal)
                    {
                        return "Eliminate pests";
                    }
                    else if (offerer.currentGoal is TitleGoal)
                    {
                        return "Defeat soldiers";
                    }
                    else if (offerer.currentGoal is WealthGoal)
                    {
                        return "Clear mine";
                    }
                    else
                    {
                        return "ERROR 614";
                    }
                case Type.Obtain:
                    if (offerer.currentGoal is EventGoal)
                    {
                        EventGoal goal = (EventGoal)offerer.currentGoal;
                        return "Steal equipment from " + goal.activeEvent.name;
                    }
                    else if (offerer.currentGoal is ProjectGoal)
                    {
                        return "Obtain construction materials";
                    }
                    else if (offerer.currentGoal is TitleGoal)
                    {
                        return "Steal deed";
                    }
                    else if (offerer.currentGoal is WealthGoal)
                    {
                        return "Steal rare gem";
                    }
                    else
                    {
                        return "ERROR 614";
                    }
                case Type.Persuade:
                    if (offerer.currentGoal is EventGoal)
                    {
                        return "Convince mercenaries to lower price";
                    }
                    else if (offerer.currentGoal is ProjectGoal)
                    {
                        return "Convince architect to join project";
                    }
                    else if (offerer.currentGoal is TitleGoal)
                    {
                        return "Persuade servants to collect information";
                    }
                    else if (offerer.currentGoal is WealthGoal)
                    {
                        return "Fundraise";
                    }
                    else
                    {
                        return "ERROR 614";
                    }
                default:
                    return "ERROR 614";
            }
            
        }
    }
}
