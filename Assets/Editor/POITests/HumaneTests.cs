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
    public class HumaneTests
    {
        City city;
        Humane poi;
        Greedy ruler;
        [SetUp]
        public void Setup()
        {
            city = new City();
            ruler = new Greedy(city);
            city.title = new Title(ruler);
            poi = new Humane(city);
        }

        /*
        Given the poi is humane
        and a moderately powerful event is hurting the city's economy
        When the poi chooses a new goal
        Then the new goal will be an Event goal
        and the goal's objective will be to reduce that event
        */
        [Test]
        public void DamagingEvent()
        {
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 60;
            ae.effect.wealthChange = -40;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae, goal.activeEvent);
        }

        /*
        Given the poi is humane
        and several moderately powerful events are hurting the city's economy
        When the poi chooses a new goal
        Then the new goal will be an Event goal
        and the goal's objective will be to reduce the event that is hurting the city's economy the most
        */
        [Test]
        public void DamagingEvents()
        {
            ActiveEvent ae1 = new ActiveEvent(city);
            ActiveEvent ae2 = new ActiveEvent(city);
            ActiveEvent ae3 = new ActiveEvent(city);
            ae1.power = 60;
            ae1.effect.wealthChange = -40;
            ae2.power = 60;
            ae2.effect.wealthChange = -50;
            ae3.power = 60;
            ae3.effect.wealthChange = -60;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<EventGoal>(poi.currentGoal);
            EventGoal goal = (EventGoal)poi.currentGoal;
            Assert.AreEqual(ae3, goal.activeEvent);
        }

        /*
        Given the poi is humane
        and there are no even moderately powerful event is hurting the city's economy
        and the city is ruled by a tyrant
        When the poi chooses a new goal
        Then the new goal will be a title goal
        and the objective will be the city's title
        */
        [Test]
        public void DeposeTyrant()
        {
            ActiveEvent ae1 = new ActiveEvent(city);
            ae1.power = 20;
            ae1.effect.wealthChange = -40;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 70;
            ae2.effect.wealthChange = 0;
            city.title.holder.tyrany = 100;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
            TitleGoal goal = (TitleGoal)poi.currentGoal;
            Assert.AreEqual(city.title, goal.title);
        }

        /*
        Given the poi is humane
        and there are no even moderately powerful event is hurting the city's economy
        and the city's ruler is not a tyrant
        and the poi's wealth is low
        When the poi chooses a new goal
        Then the new goal will be an Wealth goal
        */
        [Test]
        public void LowWealth()
        {
            ActiveEvent ae1 = new ActiveEvent(city);
            ae1.power = 20;
            ae1.effect.wealthChange = -40;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 70;
            ae2.effect.wealthChange = 0;
            city.title.holder.tyrany = 0;
            poi.wealth = 0;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<WealthGoal>(poi.currentGoal);
        }

        /*
        Given the poi is humane
        and there are no even moderately powerful event is hurting the city's economy
        and the city's ruler is not a tyrant
        and the poi's wealth is high
        and there is a public works project ongoing in the city
        When the poi chooses a new goal
        Then the new goal will be an Project goal
        with the objective to complete the project
        */
        [Test]
        public void PublicWorksProject()
        {
            ActiveEvent ae1 = new ActiveEvent(city);
            ae1.power = 20;
            ae1.effect.wealthChange = -40;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 70;
            ae2.effect.wealthChange = 0;
            city.title.holder.tyrany = 0;
            poi.wealth = 200;
            Project project = new Project(city);
            project.type = Project.Type.Public;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<ProjectGoal>(poi.currentGoal);
            ProjectGoal goal = (ProjectGoal)poi.currentGoal;
            Assert.AreEqual(project, goal.project);
        }

        /*
        Given the poi is humane
        and there are no even moderately powerful event is hurting the city's economy
        and the city's ruler is not a tyrant
        and the poi's wealth is high
        and there are no public works project ongoing in the city
        When the poi chooses a new goal
        Then the new goal will be a Title goal
        */
        [Test]
        public void Titles()
        {
            ActiveEvent ae1 = new ActiveEvent(city);
            ae1.power = 20;
            ae1.effect.wealthChange = -40;
            ActiveEvent ae2 = new ActiveEvent(city);
            ae2.power = 70;
            ae2.effect.wealthChange = 0;
            city.title.holder.tyrany = 0;
            poi.wealth = 200;
            Project project = new Project(city);
            project.type = Project.Type.Conquest;

            poi.ChooseNewGoal();

            Assert.IsInstanceOf<TitleGoal>(poi.currentGoal);
        }
    }
}
