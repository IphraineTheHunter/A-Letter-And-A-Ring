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
    public class TitleTests
    {
        /*
        Given void
        When a tick passes
        Then each title should make wealth for it's holder
        */
        [Test]
        public void WealthGain()
        {
            City city = new City();
            Greedy poi = new Greedy(city);
            Title title = new Title(poi);
            poi.wealth = 0;
            title.wealthGain = 100;

            Title.Tick();

            Assert.AreEqual(100, poi.wealth);
        }
    }
}
