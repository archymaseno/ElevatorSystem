namespace ElevatorSystem.Tests
{
    [TestClass]
    public class BuildingTests
    {
        [TestMethod]
        public void Test_FindBestElevator_ShouldReturnClosestElevator()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            building.SetWaitingPassengers(3, new List<int> { 6, 8 });

            // Act
            Elevator bestElevator = building.FindBestElevator(6);

            // Assert
            Assert.IsNotNull(bestElevator);
            Assert.AreEqual(1, bestElevator.ID); // Adjust the expected elevator ID
        }

        [TestMethod]
        public void Test_SetWaitingPassengers_ShouldAddPassengersToFloor()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int floorNumber = 2;
            List<int> destinations = new List<int> { 4, 5 };

            // Act
            building.SetWaitingPassengers(floorNumber, destinations);

            // Assert
            Assert.AreEqual(destinations.Count, building.Floors[floorNumber - 1].WaitingPassengers.Count);
        }

       

    
    }
}