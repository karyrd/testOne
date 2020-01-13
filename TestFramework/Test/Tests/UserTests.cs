﻿using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Driver;
using TestFramework.Environment;
using TestFramework.Page;
using TestFramework.Test.Base;
using TestFramework.Test.Services;

namespace TestFramework.Test.Tests
{
    [TestFixture]
    public class UserTests : TestBase
    {
        private IWebDriver _driver;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = Settings.GetConfiguration();
            _driver = DriverController.Driver(Settings.Browser);
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CloseBrowser()
        {
            DriverController.CloseBrowser();
        }

        [Test]
        public void LoginTest()
        {
            Test(() =>
            {
                var user = UserCreator.CreateUser(_configuration);
                var homePage = new HomePage(_driver);
                var userName = homePage
                    .GoTo()
                    .GoToLoginPage(user)
                    .GoToProfilePage()
                    .GetLoggedUser();
                Assert.AreEqual(user.Name, userName);
            });
        }

        [Test]
        public void ChangeNameTest()
        {
            Test(() =>
            {
                var user = UserCreator.CreateUser(_configuration);
                var homePage = new HomePage(_driver);
                var userName = homePage
                    .GoTo()
                    .GoToLoginPage(user)
                    .GoToProfilePage()
                    .ChangeName(user.NameChange)
                    .GetLoggedUser();

                homePage
                    .GoTo()
                    .GoToProfilePage()
                    .ChangeName(user.Name);

                Assert.AreEqual(user.NameChange, userName);
            });
        }

        [Test]
        public void CommentTest()
        {
            Test(() =>
            {
                var user = UserCreator.CreateUser(_configuration);
                var homePage = new HomePage(_driver);
                var commentResult = homePage
                    .GoTo()
                    .GoToLoginPage(user)
                    .GoToItemsPage()
                    .OpenItem()
                    .LeaveComment(user.CommentText)
                    .LeaveCommentResult();

                Assert.AreEqual(user.CommentResult, commentResult);
            });
        }

        [Test]
        public void RegionTest()
        {
            Test(() =>
            {
                var homePage = new HomePage(_driver);
                string regionOne = homePage
                    .GoTo()
                    .GetRegion();
                string regionTwo = homePage
                    .ChangeRegion()
                    .GetRegion();

                Assert.AreNotEqual(regionOne, regionTwo);
            });
        }
    }
}
