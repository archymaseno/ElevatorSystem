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
        public void AddPassengers_ShouldIncreaseCurrentCapacity()
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

      
    }
}
