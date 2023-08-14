namespace ElevatorSystem.Tests
{
    [TestClass]
    public class BuildingTests
    {
        [TestMethod]
        public void Building_FindBestElevator_ShouldReturnClosestElevator()
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
        public void Building_SetWaitingPassengers_ShouldAddPassengersToFloor()
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

        [TestMethod]
        public void Building_SetWaitingPassengers_InvalidFloor_ShouldNotAddPassengers()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int floorNumber = 12; // Invalid floor number
            List<int> destinations = new List<int> { 4, 5 };

            // Act
            building.SetWaitingPassengers(floorNumber, destinations);

            // Assert
            Assert.AreEqual(0, building.Floors[floorNumber - 1].WaitingPassengers.Count);
        }

        [TestMethod]
        public void Building_ShowFloorStatus_ShouldDisplayFloorStatus()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            building.SetWaitingPassengers(1, new List<int> { 2, 3 });
            building.SetWaitingPassengers(3, new List<int> { 6, 8 });

            // Act
            // Capture the console output to verify it
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                building.ShowFloorStatus();
                string expectedOutput = "Floor: 1  Elevators: 0  Waiting Passengers: 2";
                Assert.IsTrue(sw.ToString().Contains(expectedOutput));
            }
        }

        [TestMethod]
        public void Building_FindBestElevator_NoAvailableElevators_ShouldReturnNull()
        {
            // Arrange
            Building building = new Building(10, 1, 5); // Only one elevator with capacity 5
            building.SetWaitingPassengers(3, new List<int> { 6, 8 });

            // Act
            Elevator bestElevator = building.FindBestElevator(6);

            // Assert
            Assert.IsNull(bestElevator);
        }
    }
}