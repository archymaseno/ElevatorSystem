using ElevatorSystem.Entities;
using ElevatorSystem.Interfaces;

namespace ElevatorSystem.Service
{
    public class Elevator:IElevator
    {
        public int ID { get; private set; }
        public int Capacity { get; private set; }
        public int CurrentFloor { get; private set; }
        public Direction Direction { get; private set; }
        public int CurrentLoad { get; private set; }
        public List<Passenger> Passengers { get; private set; }
        public int MaxCapacity { get; private set; }
        public int CurrentCapacity { get; private set; }
        public int TargetFloor { get; private set; }
        public ElevatorMovementStatus MovementStatus { get; private set; }
        private int maxFloorCount;
        
        public Elevator(int id, int maxCapacity,int maxFloorCount)
        {
            ID = id;
            CurrentFloor = 1;
            Direction = Direction.Up;
            MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
            Passengers = new List<Passenger>();
            this.maxFloorCount = maxFloorCount;
            
        }
        public void ChangeDirectionAtExtremes()
        {
            if (CurrentFloor == 1 || CurrentFloor == maxFloorCount)
            {
                Direction = Direction == Direction.Up ? Direction.Down : Direction.Up;
            }
        }

        public bool IsAvailable(int targetFloor)
        {
            return CurrentCapacity < MaxCapacity &&       ((Direction == Direction.Up && CurrentFloor <= targetFloor) ||  (Direction == Direction.Down && CurrentFloor >= targetFloor));
        }

        public void AddPassengers(List<Passenger> passengers)
        {
            if (CurrentCapacity + passengers.Count > MaxCapacity)
            {
                Console.WriteLine("Elevator is at maximum capacity. Cannot add more passengers.");
                return;
            }
            Passengers.AddRange(passengers);
            CurrentCapacity += passengers.Count;
        }
        public void MoveToFloor(int targetFloor)
        {
            if (targetFloor > CurrentFloor)
            {
                Direction = Direction.Up;
                for (int floor = CurrentFloor + 1; floor <= targetFloor; floor++)
                {
                    CurrentFloor = floor;
                    DisplayElevatorStatus();
                    if (ShouldDropOff())
                    {
                        DropOffPassengers();
                        DisplayElevatorStatus(); // Display elevator status after drop-off
                    }
                }
            }
            else if (targetFloor < CurrentFloor)
            {
                Direction = Direction.Down;
                for (int floor = CurrentFloor - 1; floor >= targetFloor; floor--)
                {
                    CurrentFloor = floor;
                    DisplayElevatorStatus();
                    if (ShouldDropOff())
                    {
                        DropOffPassengers();
                        DisplayElevatorStatus(); // Display elevator status after drop-off
                    }
                }
            }
            else
            {
                MovementStatus = ElevatorMovementStatus.Stopped;
                Direction = Direction.Up; // Reset direction when reaching the target floor
                                          // ... (existing logic when already on the target floor)
                DropOffPassengers(); // Drop off passengers immediately
            }

            UpdateMovementStatus();
            // Change direction if elevator reaches the first or last floor
            // Call the method to change direction if elevator reaches the first or last floor
            ChangeDirectionAtExtremes();

        }
        public bool ShouldDropOff()
        {
            return Passengers.Any(passenger => passenger.DestinationFloor == CurrentFloor);
        }
        public void DropOffPassengers()
        {
            List<Passenger> passengersToDropOff = Passengers
                .Where(passenger => passenger.DestinationFloor == CurrentFloor)
                .OrderBy(passenger => Math.Abs(passenger.DestinationFloor - CurrentFloor))
                .ThenBy(passenger =>
                    Direction == Direction.Up
                        ? passenger.DestinationFloor >= CurrentFloor
                        : passenger.DestinationFloor <= CurrentFloor)
                .ToList();

            foreach (Passenger passenger in passengersToDropOff)
            {
                Passengers.Remove(passenger);
                CurrentCapacity--;
            }

            CurrentLoad = Passengers.Count;

            // Continue to next drop-off if there are remaining passengers
            if (Passengers.Any())
            {
                int nextDestination = Passengers.Min(passenger => passenger.DestinationFloor);
                MoveToFloor(nextDestination);
            }
        }
        public void DisplayElevatorStatus()
        {
            string movementStatus = Direction == Direction.Up || Direction == Direction.Down ? "Moving" : "Stopped";
            Console.WriteLine($" << Elevator {ID} < Direction: {Direction}< MovementStatus :{movementStatus} < Current Floor: {CurrentFloor} < Passengers: {CurrentCapacity}/ {MaxCapacity} Dropping at Floors: {string.Join(", ", Passengers.Select(passenger => passenger.DestinationFloor))}");
        }

        public void UpdateCapacity(int peopleCount)
        {
            CurrentLoad = peopleCount;
        }

        public void UpdateMovementStatus()
        {
            if (Passengers.Count == 0)
            {
                MovementStatus = ElevatorMovementStatus.Stopped;
            }
            else if (Passengers.Any(passenger => passenger.DestinationFloor == CurrentFloor))
            {
                MovementStatus = ElevatorMovementStatus.InFloorDropOff;
            }
            else if (Direction == Direction.Up)
            {
                MovementStatus = ElevatorMovementStatus.MovingUp;
            }
            else if (Direction == Direction.Down)
            {
                MovementStatus = ElevatorMovementStatus.MovingDown;
            }
        }
    }
}