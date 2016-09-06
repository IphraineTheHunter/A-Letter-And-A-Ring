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
    public class LackeyTests
    {
        private City city;
        private Greedy lord;
        private Lackey lackey;
        private int LACKEY_SKILL = 20;
        

        [SetUp]
        public void setUp()
        {
            city = new City();
            lord = new Greedy(city);
            lackey = new Lackey(lord);
            lackey.skill = LACKEY_SKILL;
        }

        /*
        Given a lackey has been assigned to a task
        When time ticks
        Then the task will be progressed by the lackey's skill
        */
        [Test]
        public void LackeyProgressesAssignedTask()
        {
            lackey.AssignTask(lord.currentGoal);

            Lackey.Tick();

            Assert.AreEqual(LACKEY_SKILL, lord.currentGoal.getProgress());
        }

        /*
        Given a lackey has been assigned to a task
        When time ticks,
        Then the lackey's skill will be added as effort points to the task
        */
        [Test]
        public void LackeyAddsEffortValueToAssignedTask()
        {
            lackey.AssignTask(lord.currentGoal);

            Lackey.Tick();

            Assert.AreEqual(LACKEY_SKILL, lord.currentGoal.GetEffortPoints());
        }
    }
}
