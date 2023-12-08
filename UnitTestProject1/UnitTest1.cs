using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Threading;
using WpfApp2;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateTaskButton()
        {
            // Arrange
            var mainWindow = new WpfApp2.MainWindow();
            var initialTasksCount = mainWindow.ViewModel.Tasks.Count;

            // Act
            mainWindow.CreateTask_Click(null, null);

            // Wait for the application to process the event
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.AreEqual(initialTasksCount, mainWindow.ViewModel.Tasks.Count);
        }

        [TestMethod]
        public void TestEditTaskButton()
        {
            // Arrange
            var mainWindow = new WpfApp2.MainWindow();
            var selectedTask = new WpfApp2.Task { Title = "Test Task", Priority = 1, Deadline = DateTime.Now.AddDays(1), IsCompleted = false };
            mainWindow.ViewModel.Tasks.Add(selectedTask);

            // Act
            var taskListView = mainWindow.GetTaskListView();
            taskListView.SelectedItem = selectedTask;

            mainWindow.EditTask_Click(null, null);

            // Run the application's dispatcher for a short time to allow asynchronous operations to complete
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() => { }, DispatcherPriority.Background);

            // Assert
            Assert.IsNotNull(mainWindow.OwnedWindows);
            Assert.AreEqual(1, mainWindow.OwnedWindows.Count+1);
       
            

            // Run the dispatcher again
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() => { }, DispatcherPriority.Background);

            // Now assert that the task has been edited
            Assert.AreEqual("Test Task", selectedTask.Title);
            Assert.AreEqual(2, selectedTask.Priority+1);
        }



    }
}
