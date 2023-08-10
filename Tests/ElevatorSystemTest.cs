using NUnit.Framework;
using NUnit.Framework;
using ElevatorSystem.Service;
using ElevatorSystem.Entities;
using ElevatorSystem.Enums;

namespace ElevatorSystem.Service
{
    [TestFixture]
    public class ElevatorSystemTests
    {
        [Test]
        public void Elevator_MoveToFloor_ShouldArriveAtTargetFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);

            // Act
            elevator.MoveToFloor(5);

            // Assert
            Assert.AreEqual(5, elevator.CurrentFloor);
        }

        [Test]
        public void Elevator_AddPassengers_ShouldIncreaseCurrentCapacity()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            Passenger passenger1 = new Passenger(7);
            Passenger passenger2 = new Passenger(12);

            // Act
            elevator.AddPassengers(new List<Passenger> { passenger1, passenger2 });

            // Assert
            Assert.AreEqual(2, elevator.CurrentCapacity);
        }

        [Test]
        public void Elevator_DropOffPassengers_ShouldRemovePassengersAtDestination()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            Passenger passenger1 = new Passenger(7);
            Passenger passenger2 = new Passenger(12);
            elevator.AddPassengers(new List<Passenger> { passenger1, passenger2 });

            // Act
            elevator.MoveToFloor(7);
            elevator.DropOffPassengers();

            // Assert
            Assert.AreEqual(1, elevator.CurrentCapacity);
            Assert.AreEqual(1, elevator.Passengers.Count);
            Assert.AreEqual(12, elevator.Passengers[0].DestinationFloor);
        }

        [Test]
        public void ElevatorController_FindBestElevator_ShouldReturnElevator()
        {
            // Arrange
            ElevatorController controller = new ElevatorController(2, 5, 10);
            controller.SetWaitingPassengers(3, new List<int> { 6, 8 });

            // Act
            Elevator bestElevator = controller.FindBestElevator(6);

            // Assert
            Assert.IsNotNull(bestElevator);
        }

        
    }
}
