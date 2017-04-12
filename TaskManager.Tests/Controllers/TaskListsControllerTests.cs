namespace TaskManager.Tests.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TaskManager.Controllers;

    [TestClass]
    public class TaskListsControllerTests
    {
        [TestMethod]
        public void CreateGetTest()
        {
            // Arrange
            var controller = new TaskListsController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            if (result == null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(string.Empty, result.ViewName);
            }
        }

        [TestMethod]
        public async Task TaskItemIndexTest()
        {
            // Arrange
            const int TestListId = 3;
            var controller = new TaskListsController();

            // Act
            var result = await controller.TaskItemIndex(TestListId) as RedirectToRouteResult;

            // Assert
            if (result == null)
            {
                Assert.Fail();
            }
            else
            {
                foreach (var resultItem in result.RouteValues)
                {
                    if (resultItem.Key == "action")
                    {
                        Assert.AreEqual("TaskItemIndex", resultItem.Value);
                    }

                    if (resultItem.Key == "controller")
                    {
                        Assert.AreEqual("TaskItems", resultItem.Value);
                    }

                    if (resultItem.Key == "tid")
                    {
                        Assert.AreEqual(TestListId, resultItem.Value);
                    }
                }
            }
        }

        [TestMethod]
        public async Task TaskItemReadonlyIndexTest()
        {
            // Arrange
            const int TestListId = 3;
            var controller = new TaskListsController();

            // Act
            var result = await controller.TaskItemReadonlyIndex(TestListId) as RedirectToRouteResult;
            
            // Assert
            if (result == null)
            {
                Assert.Fail();
            }
            else
            {
                foreach (var resultItem in result.RouteValues)
                {
                    if (resultItem.Key == "action")
                    {
                        Assert.AreEqual("TaskItemReadonlyIndex", resultItem.Value);
                    }

                    if (resultItem.Key == "controller")
                    {
                        Assert.AreEqual("TaskItems", resultItem.Value);
                    }

                    if (resultItem.Key == "tid")
                    {
                        Assert.AreEqual(TestListId, resultItem.Value);
                    }
                }
            }
        }
    }
}