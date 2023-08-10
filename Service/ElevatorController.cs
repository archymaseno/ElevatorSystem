using ElevatorSystem.Enums;
using ElevatorSystem.Interfaces;

namespace ElevatorSystem.Service
{
    public class ElevatorController:IElevatorController
    {
        private List<Elevator> elevators;
        private List<Floor> floors;

        public ElevatorController(int elevatorCount, int elevatorCapacity, int floorCount )
        {
            elevators = new List<Elevator>();
            for (int i = 0; i < elevatorCount; i++)
            {
                elevators.Add(new Elevator(i + 1, elevatorCapacity, floorCount));
            }

            floors = new List<Floor>();
            for (int i = 0; i < floorCount; i++)
            {
                floors.Add(new Floor(i + 1));
            }
        }
        public void ShowStatus()
        {
            Console.WriteLine();
            Console.WriteLine("---- --------------------------------------------------------------------------------- ----"); // Add dashes
            Console.WriteLine($"Elevator Status");
            Console.WriteLine($"----------------");
            foreach (Elevator elevator in elevators)
            {
                Console.WriteLine($"Elevator {elevator.ID}  Current Floor: {elevator.CurrentFloor}  Direction: {elevator.Direction}  Load: {elevator.CurrentCapacity}/{elevator.MaxCapacity}");
            }

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        public void ShowFloorStatus()
        {
            Console.WriteLine();
            Console.WriteLine($"Floor Status");
            Console.WriteLine($"----------------");
            foreach (Floor floor in floors)
            {
                Console.WriteLine($"Floor {floor.Number}  Elevators: {GetElevatorCountOnFloor(floor.Number)}  Waiting Passengers: {floor.WaitingPassengers.Count}");
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        private int GetElevatorCountOnFloor(int floorNumber)
        {
            return elevators.Count(elevator => elevator.CurrentFloor == floorNumber);
        }
        public void CallElevator(int targetFloor, int passengerCount)
        {
            Elevator bestElevator = FindBestElevator(targetFloor);
            if (bestElevator != null)
            {

                // Determine the direction based on the target floor and the elevator's current floor
                Direction elevatorDirection = targetFloor > bestElevator.CurrentFloor
            ? Direction.Up
            : targetFloor < bestElevator.CurrentFloor
                ? Direction.Down
                : bestElevator.Direction;

                // Call the elevator
                bestElevator.MoveToFloor(targetFloor);

                // Call the elevator
                bestElevator.MoveToFloor(targetFloor);
                Console.WriteLine($"Elevator {bestElevator.ID} has arrived at floor {targetFloor}");

                // Get passengers from the floor
                List<Passenger> waitingPassengers = floors[targetFloor - 1].WaitingPassengers;

                // Calculate the number of passengers to add to the elevator based on capacity
                int availableCapacity = bestElevator.MaxCapacity - bestElevator.CurrentCapacity;
                int passengersToAddCount = Math.Min(availableCapacity, Math.Min(passengerCount, waitingPassengers.Count));
                List<Passenger> passengersToAdd = waitingPassengers.Take(passengersToAddCount).ToList();

                if (passengersToAdd.Count > 0)
                {
                    // Check capacity and add passengers to the elevator
                    bestElevator.AddPassengers(passengersToAdd);

                    // Remove the added passengers from the waiting list
                    foreach (Passenger passenger in passengersToAdd)
                    {
                        floors[targetFloor - 1].WaitingPassengers.Remove(passenger);
                    }

                    // Automatically call the MoveToFloor method to drop passengers at their destination floors
                    bestElevator.MoveToFloor(targetFloor);

                    // Display elevator status after adding passengers
                    bestElevator.DisplayElevatorStatus();
                }
                else
                {
                    Console.WriteLine("Elevator is already at maximum capacity.");
                }
            }
            else
            {
                Console.WriteLine("No available elevators to service the request.");
            }


        }
        public Elevator FindBestElevator(int targetFloor)
        {
            List<Elevator> availableElevators = elevators
         .Where(elevator => elevator.IsAvailable(targetFloor))
         .ToList();

            if (availableElevators.Count == 0)
            {
                return null; // No available elevators
            }

            Elevator? bestElevator = null;
            int shortestDistance = int.MaxValue; // Initialize with a large value

            foreach (Elevator elevator in availableElevators)
            {
                int distance = Math.Abs(elevator.CurrentFloor - targetFloor);

                if (elevator.MovementStatus == ElevatorMovementStatus.Stopped ||
                    (elevator.MovementStatus == ElevatorMovementStatus.MovingUp && elevator.Direction == Direction.Up && elevator.CurrentFloor <= targetFloor) ||
                    (elevator.MovementStatus == ElevatorMovementStatus.MovingDown && elevator.Direction == Direction.Down && elevator.CurrentFloor >= targetFloor))
                {
                    if (distance < shortestDistance)
                    {
                        bestElevator = elevator;
                        shortestDistance = distance;
                    }
                }
            }

            return bestElevator;
        }
        public void SetWaitingPassengers(int floorNumber, List<int> destinations)
        {
            if (floorNumber < 1 || floorNumber > floors.Count)
            {
                Console.WriteLine("**** Invalid floor number. *****");
                return;
            }

            Floor floor = floors[floorNumber - 1]; // Get the specific floor

            floor.WaitingPassengers.Clear(); // Clear existing waiting passengers

            foreach (int destination in destinations)
            {
                floor.WaitingPassengers.Add(new Passenger(destination));
            }
            Console.WriteLine();
            Console.WriteLine($"    Setting {destinations.Count} waiting passengers on floor {floorNumber}");
            Console.WriteLine();
            // Automatically call the nearest elevator after setting waiting passengers
            // CallElevator(floorNumber, destinations.Count);
        }

    }
}