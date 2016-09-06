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
    public class PatriotTests
    {
        City city;
        Patriot poi;
        [SetUp]
        public void Setup()
        {
            city = new City();
            poi = new Patriot(city);
        }

        /*
        Given the poi is a patriot
        And there is an ongoing conquest project in the city
        When the poi chooses a new goal
        Then the goal will be a project goal to complete the conquest project
        */
        [Test]
        public void ConquestProject()
        {
            Project project = new Project(city);
            project.type = Project.Type.Conquest;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<ProjectGoal>(poi.currentGoal);
            ProjectGoal goal = (ProjectGoal)poi.currentGoal;
            Assert.AreEqual(project, goal.project);
        }

        /*
        Given a poi is a patriot
        And there is not an ongoing conquest project in the city
        And the poi's wealth is low
        When the poi chooses a new goal
        Then the goal will be a wealth goal
        */
        [Test]
        public void LowWealth()
        {
            poi.wealth = 0;
            poi.ChooseNewGoal();

            Assert.IsInstanceOf<WealthGoal>(poi.currentGoal);
        }

        /*
        Given a poi is a patriot
        And there is not an ongoing conquest project in the city
        And the poi's wealth is high
        And there is a powerful event affecting the city
        When the poi chooses a new goal
        Then the goal will be an event goal
        */
        [Test]
        public void DamagingEvent()
        {
            poi.wealth = 500;
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae, goal.activeEvent);
        }

        /*
        Given a poi is a patriot
        And there is not an ongoing conquest project in the city
        And the poi's wealth is high
        And there are several powerful events affecting the city
        When the poi chooses a new goal
        Then the goal will be an event goal
        and that goal will be to eliminate the event with the highest power
        */
        [Test]
        public void DamagingEvents()
        {
            poi.wealth = 500;
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 70;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 90;
            ActiveEvent ae3 = new ActiveEvent(city);
            ae3.power = 65;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae2, goal.activeEvent);
        }

        /*
        Given a poi is a patriot
        And there is not an ongoing conquest project in the city
        And the poi's wealth is high
        and there are no powerful events affecting the city
        When the poi chooses a new goal
        Then the goal will be a Title goal
        */
        [Test]
        public void Titles()
        {
            poi.wealth = 500;
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 30;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
        }
    }
}
