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
    public class PowerMongerTests
    {
        City city;
        PowerMonger poi;
        [SetUp]
        public void Setup()
        {
            city = new City();
            poi = new PowerMonger(city);
        }

        /*
        Given the poi is a power monger
        And there is a sufficiently powerful active event that is damaging the city's power base
        When the poi chooses a new goal
        Then the goal will be an event goal
        And the goal will be to end the event that is damaging the power base
        */
        [Test]
        public void DamagingEvent()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;
            ae.effect.powerChange = -5;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae, goal.activeEvent);
        }

        /*
        Given the poi is a power monger
        And there are several sufficiently powerful active event that is damaging the city's power base
        When the poi chooses a new goal
        Then the goal will be an event goal
        And the goal will be to end the event that is damaging the power base the most
        */
        [Test]
        public void DamagingEvents()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;
            ae.effect.powerChange = -5;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 70;
            ae2.effect.powerChange = -10;
            ActiveEvent ae3 = new ActiveEvent(city);
            ae3.power = 70;
            ae3.effect.powerChange = -3;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae2, goal.activeEvent);
        }

        /*
        Given the poi is a power monger
        And there is not a sufficiently powerful active event that is damaging the city's power base
        And the poi has low wealth
        When the poi chooses a new goal
        Then the goal will be a wealth goal
        */
        [Test]
        public void LowWealth()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 20;
            ae.effect.powerChange = -5;
            poi.wealth = 0;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<WealthGoal>(poi.currentGoal);
        }

        /*
        Given the poi is a power monger
        And there is not a sufficiently powerful active event that is damaging the city's power base
        And the poi has high wealth
        When the poi chooses a new goal
        Then the goal will be a title goal
        */
        [Test]
        public void Titles()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 20;
            ae.effect.powerChange = -5;
            poi.wealth = 500;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
        }
    }
}
