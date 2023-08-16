using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Tests
{
    [TestClass]
    public class ElevatorTests
    {
        [TestMethod]
        public void MoveToFloor_ShouldChangeElevatorDirectionAndFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);

            // Act
            elevator.MoveToFloor(5);

            // Assert
            Assert.AreEqual(Direction.Up, elevator.Direction);
            Assert.AreEqual(5, elevator.CurrentFloor);
        }
             
        [TestMethod]
        public void Test_AddPassengers_ShouldIncreaseCurrentCapacity()
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

        [TestMethod]
        public void Test_DropOffPassengers_ShouldRemovePassengersAndChangeCapacity()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            elevator.AddPassengers(new List<Passenger> { new Passenger(3), new Passenger(5) });

            // Act
            elevator.MoveToFloor(5);
            elevator.DropOffPassengers();

            // Assert
            Assert.AreEqual(0, elevator.CurrentCapacity);
        }

        [TestMethod]
        public void Test_UpdateCapacity_ShouldUpdateCurrentLoad()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);

            // Act
            elevator.UpdateCapacity(8);

            // Assert
            Assert.AreEqual(8, elevator.CurrentLoad);
        }

        [TestMethod]
        public void Test_UpdateMovementStatus_ShouldUpdateStatusWhenEmpty()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);

            // Act
            elevator.UpdateMovementStatus();

            // Assert
            Assert.AreEqual(ElevatorMovementStatus.Stopped, elevator.MovementStatus);
        }

        [TestMethod]
        public void Test_UpdateMovementStatus_ShouldUpdateStatusWhenAtFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            elevator.MoveToFloor(5);
            elevator.AddPassengers(new List<Passenger> {new Passenger(4),new Passenger(1) });

            // Act
            elevator.MoveToFloor(1);
            elevator.UpdateMovementStatus();

            // Assert
            Assert.AreEqual(ElevatorMovementStatus.Stopped, elevator.MovementStatus);
        }

        [TestMethod]
        public void Test_UpdateMovementStatus_ShouldUpdateStatusWhenMovingUp()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            elevator.AddPassengers(new List<Passenger>() { new Passenger(9), new Passenger(10) });
            elevator.MoveToFloor(8);

            // Act
            elevator.UpdateMovementStatus();

            // Assert
            Assert.AreEqual(ElevatorMovementStatus.Moving, elevator.MovementStatus);
        }

        [TestMethod]
        public void Test_UpdateMovementStatus_ShouldUpdateStatusWhenMovingDown()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            elevator.MoveToFloor(3);
            elevator.AddPassengers(new List<Passenger>() { new Passenger(2), new Passenger(1) });

            // Act
            elevator.UpdateMovementStatus();

            // Assert
            Assert.AreEqual(ElevatorMovementStatus.Moving, elevator.MovementStatus);
        }

        [TestMethod]
        public void Test_ChangeDirectionAtExtremes_ShouldChangeDirectionWhenAtTopFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);


            // Act
            elevator.MoveToFloor(20);

            // Assert
            Assert.AreEqual(Direction.Down, elevator.Direction);
        }

        [TestMethod]
        public void Test_ChangeDirectionAtExtremes_ShouldChangeDirectionWhenAtBottomFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 10, 20);
            elevator.MoveToFloor(3);

            // Act
            elevator.MoveToFloor(1);

            // Assert
            Assert.AreEqual(Direction.Up, elevator.Direction);
        }
    }
}
