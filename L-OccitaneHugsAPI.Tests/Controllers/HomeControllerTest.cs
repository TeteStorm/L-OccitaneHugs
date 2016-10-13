using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using L_OccitaneHugsAPI;
using L_OccitaneHugsAPI.Controllers;

namespace L_OccitaneHugsAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
