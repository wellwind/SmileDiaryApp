using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmileDiaryApp;
using SmileDiaryApp.BadgeCheckers;

namespace SmileDiaryApp.BadgeCheckers.Tests
{
    [TestClass]
    public class BadgeCheckerTest
    {
        [TestMethod]
        public void 連續達到100分的天數_高達100分時天數加1()
        {
            var badgeData = new BadgeData();
            badgeData.Smile100Days = 5;    
            var score = 99.99999;
            var checker = new Smile100DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(6, badgeData.Smile100Days);
        }

        [TestMethod]
        public void 連續達到100分的天數_未達100分時天數加0()
        {
            var badgeData = new BadgeData();
            badgeData.Smile100Days = 5;
            var score = 99;
            var checker = new Smile100DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(5, badgeData.Smile100Days);
        }

        [TestMethod]
        public void 連續超過90分的天數_超過90分時天數加1()
        {
            var badgeData = new BadgeData();
            badgeData.Smile90Days = 5;
            var score = 90;
            var checker = new Smile90DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(6, badgeData.Smile90Days);
        }

        [TestMethod]
        public void 連續超過90分的天數_未達90分時天數加0()
        {
            var badgeData = new BadgeData();
            badgeData.Smile90Days = 5;
            var score = 89.99999;
            var checker = new Smile90DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(5, badgeData.Smile90Days);
        }

        [TestMethod]
        public void 連續超過80分的天數_超過80分時天數加1()
        {
            var badgeData = new BadgeData();
            badgeData.Smile80Days = 5;
            var score = 80;
            var checker = new Smile80DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(6, badgeData.Smile80Days);
        }

        [TestMethod]
        public void 連續超過80分的天數_未達80分時天數加0()
        {
            var badgeData = new BadgeData();
            badgeData.Smile80Days = 5;
            var score = 79.99999;
            var checker = new Smile80DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(5, badgeData.Smile80Days);
        }

        [TestMethod]
        public void 連續未滿50分的天數_未滿50分時天數加1()
        {
            var badgeData = new BadgeData();
            badgeData.SmileLessThen50Days = 5;
            var score = 49.9999;
            var checker = new SmileLessThen50DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(6, badgeData.SmileLessThen50Days);
        }

        [TestMethod]
        public void 連續未滿50分的天數_大於50分時天數加0()
        {
            var badgeData = new BadgeData();
            badgeData.SmileLessThen50Days = 5;
            var score = 50;
            var checker = new SmileLessThen50DaysChecker();
            checker.Check(score, badgeData);

            Assert.AreEqual(5, badgeData.SmileLessThen50Days);
        }
    }
}
