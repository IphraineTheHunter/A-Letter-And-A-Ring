using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest;
using Assets.Scripts._PersonOfInterest.Goals.SubTypes;


namespace Assets.CityTests
{
    [TestFixture]
    public class GoalRewardsTests
    {
        City city;
        Greedy poi;

        [SetUp]
        public void Setup()
        {
            CityContext.context = new CityContext();
            CityContext.context._playerMap = new PlayerMap();
            city = new City();
            poi = new Greedy(city);
        }

        /*
        Given void
        When a Wealth goal reaches 100% progress
        Then the POI's wealth should increases by the given amount
        */
        [Test]
        public void WealthGoalReward()
        {
            poi.currentGoal = new WealthGoal(poi, 300);
            poi.wealth = 0;

            poi.currentGoal.Progress(100);

            Assert.AreEqual(300, poi.wealth);
        }

        /*
        Given void
        When an Event goal reaches 100% progress
        Then the event's power should increases by 25
        */
        [Test]
        public void EventGoalReward()
        {
            ActiveEvent ae = new ActiveEvent(city);
            poi.currentGoal = new EventGoal(poi, ae);
            ae.power = 100;

            poi.currentGoal.Progress(100);

            Assert.AreEqual(75, ae.power);
        }

        /*
        Given void
        When a Title goal reaches 100% progress
        Then the title should now be included on the goal holder's Titles list
        And the title should no longer be included on the previous owner's Titles list
        And the title's "holder" attribute should now point to the goal holder
        */
        [Test]
        public void TitleGoalReward()
        {
            Greedy oldOwner = new Greedy(city);
            Title title = new Title(oldOwner);
            poi.currentGoal = new TitleGoal(poi, title);

            CollectionAssert.Contains(oldOwner.heldTitles, title);

            poi.currentGoal.Progress(100);

            CollectionAssert.Contains(poi.heldTitles, title);
            CollectionAssert.DoesNotContain(oldOwner.heldTitles, title);
            Assert.AreEqual(poi, title.holder);
        }

        /*
        Given void
        When a Project goal reaches 100% progress,
        Then the goal holder should gain 200 prestige
        */
        [Test]
        public void ProjectGoalReward()
        {
            Project project = new Project(city);
            poi.currentGoal = new ProjectGoal(poi, project);
            poi.prestige = 0;

            poi.currentGoal.Progress(100);

            Assert.AreEqual(200, poi.prestige);
        }
    }
}
