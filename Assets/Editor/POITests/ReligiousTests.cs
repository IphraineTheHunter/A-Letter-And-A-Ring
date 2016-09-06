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
    public class ReligiousTests
    {
        City city;
        Religious poi;
        [SetUp]
        public void Setup()
        {
            city = new City();
            poi = new Religious(city);
        }

        /*
        Given the poi is religious
        and there is a moderately powerful event that is harming city piety
        When the poi chooses a new goal,
        the new goal will be an event goal to remove that event
        */
        [Test]
        public void HereticalEvent()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 50;
            ae.effect.pietyChange = -10;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae, goal.activeEvent);
        }

        /*
        Given the poi is religious
        and there are several moderately powerful events that are harming city piety
        When the poi chooses a new goal,
        the new goal will be an event goal to remove the event that harms city piety the most
        */
        [Test]
        public void HereticalEvents()
        {
            ActiveEvent ae1 = new ActiveEvent(city);
            ActiveEvent ae2 = new ActiveEvent(city);
            ActiveEvent ae3 = new ActiveEvent(city);
            ae1.power = 50;
            ae1.effect.pietyChange = -10;
            ae2.power = 50;
            ae2.effect.pietyChange = -20;
            ae3.power = 50;
            ae3.effect.pietyChange = -30;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae3, goal.activeEvent);
        }

        /*
        Given the poi is religious
        and there are no even moderately powerful events that are harming city piety
        and the poi's wealth is low,
        When the poi chooses a new goal,
        the new goal will be a wealth goal
        */
        [Test]
        public void LowWealth()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 50;
            ae.effect.pietyChange = 0;
            poi.wealth = 0;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<WealthGoal>(poi.currentGoal);
        }

        /*
        Given the poi is religious
        and there are no even moderately powerful events that are harming city piety
        and the poi's wealth is high,
        When the poi chooses a new goal,
        the new goal will be a title goal
        */
        [Test]
        public void Titles()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 50;
            ae.effect.pietyChange = 0;
            poi.wealth = 500;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
        }
    }
}
