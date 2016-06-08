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
    public class ActiveEventTests
    {
        /*
        Given a low random number
        When an ActiveEvent is reduced
        Then its power goes down 
        */
        [Test]
        public void ActiveEventWeakened()
        {
            RandomCustom rand = Substitute.For<RandomCustom>();
            rand.RollXdY(1, 100).Returns(0);
            RandomCustom.instance = rand;

            City city = new City();
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 100;

            ae.Reduce(10);

            Assert.AreEqual(90, ae.power);
        }

        /*
        Given a high random number
        When an ActiveEvent is reduced
        Then it will be removed from the city
        */
        [Test]
        public void ActiveEventEnds()
        {
            RandomCustom rand = Substitute.For<RandomCustom>();
            rand.RollXdY(1, 100).Returns(100);
            RandomCustom.instance = rand;

            City city = new City();
            ActiveEvent ae = new ActiveEvent(city);
            ae.power = 100;

            ae.Reduce(10);

            CollectionAssert.DoesNotContain(city.activeEvents, ae);
        }
    }
}
