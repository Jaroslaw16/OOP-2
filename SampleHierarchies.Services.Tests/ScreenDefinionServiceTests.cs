using SampleHierarchies.Data;
using SampleHierarchies.Services;

namespace SampleHierarchies.Services.Tests
{
    [TestClass]
    public class ScreenDefinionServiceTests
    {
        [TestMethod]
        public void Read_ValidJsonPath_ReturnsNonNullScreenDefinition()
        {
            /// Arrange
            string jsonPath = "MainScreen.json";
            /// Act
            var result = ScreenDefinionService.Read(jsonPath);
            /// Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Show_ValidJsonPathAndLineId_WritesDataToConsole()
        {
            /// Arrange
            string jsonPath = "MainScreen.json";
            string expectedOutput = "Your available choices are: \n0. Exit \n1. Animals \nPlease enter your choice: \r\n";
            int lineId = 0;
            StringWriter sw = new StringWriter();
            /// Act
            Console.SetOut(sw);
            ScreenDefinionService.Show(jsonPath, lineId);
            /// Assert
            Assert.AreEqual(expectedOutput.ToString().Trim(), sw.ToString().Trim());
        }
        [TestMethod]
        public void Write_ValidSettings_WritesDataToFile()
        {
            string jsonPath = "MainScreenForUT.json"; 
            var settings = new ScreenDefinition(); 

            ScreenDefinionService.Write(settings, jsonPath);
            Assert.IsTrue(File.Exists(jsonPath));
        }
    }
}
