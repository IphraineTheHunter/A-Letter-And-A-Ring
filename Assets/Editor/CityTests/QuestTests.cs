using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using Assets.Scripts._cityScripts;
using Assets.Scripts._PersonOfInterest;

namespace Assets.CityTests
{
    [TestFixture]
    class QuestTests
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
        When a player accepts a new quest
        Then their wealth should increase
        And the offerer's wealth should decrease by the same amount
        */
        [Test]
        public void AcceptingingAQuestCostsMoney()
        {
            poi.wealth = 0;
            CityContext.context._playerMap.wealth = 0;

            poi.offeredQuest.Accept();

            Assert.AreEqual(-125, poi.wealth);
            Assert.AreEqual(125, CityContext.context._playerMap.wealth);
        }

        /*
        Given void
        When a player completes a quest for a poi,
        Then that poi's goal should progress
        */
        [Test]
        public void CompletingAQuestProgressesTheOfferersGoal()
        {
            Assert.AreEqual(0, poi.currentGoal.getProgress());

            poi.offeredQuest.Complete();

            Assert.AreEqual(25, poi.currentGoal.getProgress());
        }

        /*
        Given void
        When a player completes a quest for a poi,
        Then that poi's wealth should decrease
        and the player's wealth should increase by the same amount
        */
        [Test]
        public void CompletingAQuestGivesThePlayerWealthFromTheOfferer()
        {
            CityContext.context._playerMap.wealth = 0;
            poi.wealth = 0;

            poi.offeredQuest.Complete();
            
            Assert.AreEqual(500, CityContext.context._playerMap.wealth);
            Assert.AreEqual(-500, poi.wealth);
        }
    }
}
