using NUnit.Framework;
using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Tests
{
    [TestFixture]
    public class QuestTests
    {
        
        [SetUp]
        public void Setup()
        {
            CityContext.context = new CityContext();
        }

        [Test]
        public void NewQuestAddsToCityContext()
        {
            Quest quest = new Quest();

            Assert.AreEqual(1, CityContext.context._quests.Count);
        }
        
    }
}