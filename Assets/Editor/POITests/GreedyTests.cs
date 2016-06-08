using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;

namespace Assets.POITests
{
    [TestFixture]
    public class GreedyTests
    {
        City city;
        Greedy poi;
        [SetUp]
        public void Setup()
        {
            city = new City();
            poi = new Greedy(city);
        }

        /*
        Given poi is greedy
        and the poi has low wealth
        When the poi chooses a new goal,
        the goal will be a wealth goal
        */
        [Test]
        public void LowWealth()
        {
            poi.wealth = 0;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<WealthGoal>(poi.currentGoal);
        }

        /*
        Given poi is greedy
        and the poi has high wealth
        and the poi is effected by a strong event that makes them lose wealth
        When the poi chooses a new goal,
        the goal will be an event goal to remove that event
        */
        [Test]
        public void CostlyEvent()
        {
            poi.wealth = 300;
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 100;
            ae.effect.wealthChange = -100;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae, goal.activeEvent);
        }

        /*
        Given poi is greedy
        and the poi has high wealth
        and the poi is effected by several strong events that makes them lose wealth
        When the poi chooses a new goal,
        the goal will be an event goal to remove the event that has the highest power multiplied by wealth loss
        */
        [Test]
        public void ManyCostlyEvents()
        {
            poi.wealth = 300;
            ActiveEvent ae1 = new ActiveEvent(city);
            ActiveEvent ae2 = new ActiveEvent(city);
            ActiveEvent ae3 = new ActiveEvent(city);
            ae1.power = 100;
            ae1.effect.wealthChange = -100;
            ae2.power = 100;
            ae2.effect.wealthChange = -200;
            ae3.power = 100;
            ae3.effect.wealthChange = -300;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae3, goal.activeEvent);
        }

        /*
        Given the poi is greedy
        and the poi has high wealth
        and there are no powerful and costly events in the city,
        When the poi chooses a new goal,
        the goal will be to gain a new title
        */
        [Test]
        public void Titles()
        {
            poi.wealth = 300;
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;
            ae.effect.wealthChange = -1;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
        }
    }
}
