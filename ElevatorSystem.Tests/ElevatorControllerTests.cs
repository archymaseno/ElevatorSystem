using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Tests
{
    [TestClass]
    public class ElevatorControllerTests
    {
        [TestMethod]
        public void CallElevator_ValidRequest_ShouldMoveElevatorToRequestedFloor()
        {
            // Arrange
            IBuilding building = new Building(10, 3, 5);
            IElevatorController elevatorController = new ElevatorController(building);
            int targetFloor = 7;
            int passengerCount = 3;

            // Act
            elevatorController.CallElevator(targetFloor, passengerCount);

            // Assert
            Elevator elevator = building.Elevators.First();
            Assert.AreEqual(targetFloor, elevator.CurrentFloor);
        }

       

        [TestMethod]
        public void CallElevator_NoAvailableElevators_ShouldNotMoveElevator()
        {
            // Arrange
            IBuilding building = new Building(10, 3, 5);
            IElevatorController elevatorController = new ElevatorController(building);
            int targetFloor = 7;
            int passengerCount = 3;

            // Set all elevators to full capacity
            foreach (var elevator1 in building.Elevators)
            {
                while (elevator1.CurrentCapacity < elevator1.MaxCapacity)
                {
                    elevator1.AddPassengers(new System.Collections.Generic.List<Passenger> { new Passenger(1) });
                }
            }

            // Act
            elevatorController.CallElevator(targetFloor, passengerCount);

            // Assert
            Elevator elevator = building.Elevators.First();
            Assert.AreNotEqual(targetFloor, elevator.CurrentFloor);
        }

      
    }
}
