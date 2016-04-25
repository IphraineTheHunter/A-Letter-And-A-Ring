using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using Assets;

[TestFixture]
public class GoalTests {
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
        goal = new POIGoal(poi, POIGoal.Type.Event);
        poi.currentGoal = goal;
        quest = new Quest();
        poi.offeredQuest = quest;

    }

    [Test]
    public void CompletingAGoalAppliesReward()
    {
        goal.goalReward = Substitute.For<POIGoalReward>();

        goal.Progress(100);

        goal.goalReward.Received().Apply();
    }

    [Test]
    public void NotCompletingAGoalWithinTimeLineFails()
    {
        for (int i = 0; i < 10; i++)
        {
            POIGoal.Tick();
        }

        Assert.IsFalse(CityContext.context._goals.Contains(goal));
        Assert.AreNotEqual(goal, poi.currentGoal);
    }
}
