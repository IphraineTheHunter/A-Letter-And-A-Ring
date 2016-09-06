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
    public class PrideTests
    {
        City city;
        Pride poi;
        [SetUp]
        public void Setup()
        {
            city = new City();
            poi = new Pride(city);
        }

        /*
        Given the poi is a pride
        And there is a sufficiently powerful active event
        When the poi chooses a new goal
        Then the goal will be an event goal
        And the goal will be to end the event
        */
        [Test]
        public void DamagingEvent()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae, goal.activeEvent);
        }

        /*
        Given the poi is a pride
        And there are several sufficiently powerful active events
        When the poi chooses a new goal
        Then the goal will be an event goal
        And the goal will be to end the most powerful
        */
        [Test]
        public void DamagingEvents()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 90;
            ActiveEvent ae3 = new ActiveEvent(city);
            ae3.power = 80;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae2, goal.activeEvent);
        }

        /*
        Given the poi is a pride
        And there is not a sufficiently powerful active event
        And the poi has low wealth
        When the poi chooses a new goal
        Then the goal will be a wealth goal
        */
        [Test]
        public void LowWealth()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 30;
            poi.wealth = 0;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<WealthGoal>(poi.currentGoal);
        }

        /*
        Given the poi is a pride
        And there is not a sufficiently powerful active event
        And the poi has high wealth
        When the poi chooses a new goal
        Then the goal will be a title goal
        */
        [Test]
        public void Titles()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 30;
            poi.wealth = 500;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
        }
    }
}
