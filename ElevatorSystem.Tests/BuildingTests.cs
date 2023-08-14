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
        
        [TestMethod]
        public void Test_SetWaitingPassengers_InvalidFloor_ShouldNotAddPassengers()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int invalidFloorNumber = 12; // Invalid floor number
            List<int> destinations = new List<int> { 4, 5 };
            bool exceptionIsThrown=false;
            int floorWaitingPassengers=0;
            
            // Act
            try
            {
                building.SetWaitingPassengers(invalidFloorNumber, destinations);
                floorWaitingPassengers = building.Floors[invalidFloorNumber - 1].WaitingPassengers.Count;
            }
            catch (ArgumentOutOfRangeException)
            {
                exceptionIsThrown = true;
            }

            // Assert            
            Assert.IsTrue(exceptionIsThrown, "Expected ArgumentOutOfRangeException was not thrown.");
            Assert.AreEqual(0, floorWaitingPassengers, "Waiting passengers count should not have changed.");
        }

        [TestMethod]
        public void Test_SetWaitingPassengers_SameFloor_ShouldNotAddPassengers()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int floorNumber = 3;
            List<int> destinations = new List<int> { 3, 5 }; // Same floor as waiting

            // Act
            building.SetWaitingPassengers(floorNumber, destinations);

            // Assert
            Assert.AreEqual(1, building.Floors[floorNumber - 1].WaitingPassengers.Count);
        }

        [TestMethod]
        public void Test_SetWaitingPassengers_InvalidDestination_ShouldNotAddPassengers()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int floorNumber = 5;
            List<int> destinations = new List<int> { 15, 7 }; // Invalid destination

            // Act
            building.SetWaitingPassengers(floorNumber, destinations);

            // Assert
            Assert.AreEqual(1, building.Floors[floorNumber - 1].WaitingPassengers.Count);
        }

        [TestMethod]
        public void Test_SetWaitingPassengers_AddToMultipleFloors_ShouldAddPassengers()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int floorNumber1 = 3;
            int floorNumber2 = 7;
            List<int> destinations1 = new List<int> { 4, 5 };
            List<int> destinations2 = new List<int> { 8, 9 };

            // Act
            building.SetWaitingPassengers(floorNumber1, destinations1);
            building.SetWaitingPassengers(floorNumber2, destinations2);

            // Assert
            Assert.AreEqual(destinations1.Count, building.Floors[floorNumber1 - 1].WaitingPassengers.Count);
            Assert.AreEqual(destinations2.Count, building.Floors[floorNumber2 - 1].WaitingPassengers.Count);
        }

        [TestMethod]
        public void Test_SetWaitingPassengers_EmptyDestinations_ShouldNotAddPassengers()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            int floorNumber = 9;
            List<int> destinations = new List<int>(); // Empty destinations

            // Act
            building.SetWaitingPassengers(floorNumber, destinations);

            // Assert
            Assert.AreEqual(0, building.Floors[floorNumber - 1].WaitingPassengers.Count);
        }

        [TestMethod]
        public void Test_FindBestElevator_NoAvailableElevator_ShouldReturnNull()
        {
            // Arrange
            Building building = new Building(10, 0, 5); // Create a building with zero elevators

            // Act
            Elevator bestElevator = building.FindBestElevator(6);

            // Assert
            Assert.IsNull(bestElevator);
        }

        [TestMethod]
        public void Test_FindBestElevator_ElevatorDirectionUp_ShouldReturnInboundElevator()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            building.SetWaitingPassengers(5, new List<int> { 7 });

            // Act
            Elevator bestElevator = building.FindBestElevator(7);

            // Assert
            Assert.IsNotNull(bestElevator);
            // Add your assertion for elevator ID here based on your implementation
        }

        [TestMethod]
        public void Test_FindBestElevator_ElevatorDirectionDown_ShouldReturnInboundElevator()
        {
            // Arrange
            Building building = new Building(10, 3, 5);
            building.SetWaitingPassengers(7, new List<int> { 3 });

            // Act
            Elevator bestElevator = building.FindBestElevator(3);

            // Assert
            Assert.IsNotNull(bestElevator);
            // Add your assertion for elevator ID here based on your implementation
        }



    }
}