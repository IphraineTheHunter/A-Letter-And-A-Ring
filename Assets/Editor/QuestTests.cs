using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using Assets;


[TestFixture]
class QuestTests
{
    private Quest quest;
    private PersonOfInterest poi;
    private POIGoal goal;
    private ActiveEvent activeEvent;


    [SetUp]
    public void Setup()
    {
        RandomCustom stub = Substitute.For<RandomCustom>();
        stub.RollXdY(1, 100).ReturnsForAnyArgs(0);
            
        CityContext.context = new CityContext();
        CityContext.context.random = stub;

        poi = new PersonOfInterest();
        activeEvent = new ActiveEvent();
        poi.affectedByEvents.Add(activeEvent);
        goal = Substitute.For<POIGoal>();
        poi.currentGoal = goal;
        quest = new Quest(poi);
        poi.offeredQuest = quest;
            
    }
        
    [Test]
    public void CompletingAQuestProgressesTheOfferersGoal()
    {
        quest.Complete();
        poi.currentGoal.Received().Progress(25);
    }
}

