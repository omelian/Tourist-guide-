using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Moq;
using TouristGuide.BLL.DBContext;
using TouristGuide.BLL.Interfaces;
using TouristGuide.BLL.Services;
using TouristGuide.DAL.Interfaces;
using TouristGuide.DAL.Repositories;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;
using TouristGuide.UI.Models;
using TouristGuide.UI.Controllers;

namespace TouristGuide.Tests
{
    [TestClass]
    public class GuestBlTesting
    {
       [TestMethod]
        public void CanViewRegisterPage()
        {
            // Arrange
            var controller = new AccountController(null);

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.AreEqual("Register", result.ViewName);
        }

        [TestMethod]
        public void Can_View_List_Of_Restaurants()
        {
            List<ProfileListViewModel> datas = new List<ProfileListViewModel>();

            datas.Add(new ProfileListViewModel { ProfileId = 0, Name = "Chas Poisty", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 1, Name = "Lol", City = "London", Street = "Bandery St", Number = "4d" });

            ListOfRestaurantController target = new ListOfRestaurantController(null);
            JsonResult actual = target.GetRestaurants() as JsonResult;
            List<int> result = actual.Data as List<int>;


            if (result != null)Assert.AreEqual(datas[0], result[0]);
        }

        [TestMethod]
        public void Can_Get_Page()
        {
            List<ProfilePageViewModel> pages = new List<ProfilePageViewModel>();
            pages.Add(new ProfilePageViewModel
            {
                Name = "Chas Poisty",
                City = "Lviv",
                Street = "Bandery St",
                Number = "3b"
            });

            ListOfRestaurantController target = new ListOfRestaurantController(null);
            JsonResult actual = target.GetRestaurants() as JsonResult;
            List<int> result = actual.Data as List<int>;
            if (result != null) Assert.AreEqual(pages[0], result[0]);
        }

        [TestMethod]
        public void GetProfiles()
        {
            List<Profile> profiles = new List<Profile>();
            profiles.Add(new Profile
            {
                Name = "Chas Poisty"
            }
            );

            var mock = new Mock<UnitOfWork>();
           // mock.Setup(a => a.GetAllRestaurants()).Returns(new List<Profile>());

            var controller = new ListOfRestaurantController(mock.Object);

            var result = controller.GetAllRestaurants();

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void CanGetLocations()
        {
            var mock = new Mock<DataBaseManager>();
            mock.Setup(a => a.LocationManager.GetAll()).Returns(new List<Location>());

            List<Location> locations = new List<Location>();
            locations.Add(new Location
            {
                LocationId = 2
            }
           );

            GuestBL guestBl = new GuestBL(mock.Object);

            var result = guestBl.GetAllLocations();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestViewRestaurants()
        {
            var controller = new ListOfRestaurantController(null);

            var result = controller.GetRestaurants() as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanViewLoginPage()
        {
            // arrange
            var controller = new AccountController(null);

            //act
            var result = controller.Login("") as ViewResult;

            //assert
            Assert.AreEqual("Login", result.ViewName);
        }
      
    }
}
