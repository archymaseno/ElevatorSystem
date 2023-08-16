namespace ElevatorSystem.Models
{
    public class Elevator : IElevator
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

        public Elevator(int id, int maxCapacity, int maxFloorCount)
        {
            ID = id;
            CurrentFloor = 1;
            Direction = Direction.None;
            MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
            Passengers = new List<Passenger>();
            this.maxFloorCount = maxFloorCount;

        }
        public void ChangeDirectionAtExtremes()// It still has passengers
        {
            if (CurrentFloor == 1 || CurrentFloor == maxFloorCount)
            {
                Direction = Direction == Direction.Up ? Direction.Down : Direction.Up;
            }
        }
        public bool NotFull(int targetFloor)
        {
            return CurrentCapacity < MaxCapacity;
        }
        public bool IsInbound(int targetFloor)
        {
            bool Inbound = false; //Inbound if the direction is up or down whether stopped at floor or moving
            if (((CurrentFloor > targetFloor) && (Direction == Direction.Down)) || ((CurrentFloor < targetFloor) && (Direction == Direction.Up)))
            {
                Inbound = true;
            }
            else
            {
                Inbound = false;
            }


            return Inbound;
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
                MovementStatus = ElevatorMovementStatus.Moving;
                for (int floor = CurrentFloor; floor <= targetFloor; floor++)
                {
                    CurrentFloor = floor;
                    if (ShouldDropOff())
                    {
                        DropOffPassengers();
                        break;
                    }
                    else
                    {
                        DisplayElevatorStatus("move");
                    }
                    Thread.Sleep(1500);
                }
            }
            else if (targetFloor < CurrentFloor)
            {
                Direction = Direction.Down;
                MovementStatus = ElevatorMovementStatus.Moving;
                for (int floor = CurrentFloor; floor >= targetFloor; floor--)
                {
                    CurrentFloor = floor;

                    if (ShouldDropOff())
                    {
                        DropOffPassengers();
                        break;
                    }
                    else
                    {
                        DisplayElevatorStatus("move");
                    }
                    Thread.Sleep(1500);
                }
            }
            else //Already at the target floors
            {

                DropOffPassengers(); // Drop off passengers immediately
            }

            UpdateMovementStatus();
            ChangeDirectionAtExtremes();

        }
        public bool ShouldDropOff()
        {
            return Passengers.Any(passenger => passenger.DestinationFloor == CurrentFloor);
        }
        public void DropOffPassengers()
        {
            List<Passenger> passengersToDropOff = Passengers.Where(passenger => passenger.DestinationFloor == CurrentFloor)
                .OrderBy(passenger => Math.Abs(passenger.DestinationFloor - CurrentFloor))
                .ThenBy(passenger => Direction == Direction.Up ? passenger.DestinationFloor >= CurrentFloor : passenger.DestinationFloor <= CurrentFloor).ToList();

            //if (passengersToDropOff.Count > 0)
            {
                foreach (Passenger passenger in passengersToDropOff)
                {
                    Passengers.Remove(passenger);
                    CurrentCapacity--;
                }
                CurrentLoad = Passengers.Count;

                // Continue to next drop-off if there are remaining passengers
                if (Passengers.Any())
                {
                    int nextDestination;
                    if (Direction == Direction.Up)
                    {
                        nextDestination = Passengers.Max(passenger => passenger.DestinationFloor);
                        if (passengersToDropOff.Count > 0)
                        {
                            DisplayElevatorStatus("drop"); // Display elevator status after drop-off                       
                        }
                        MoveToFloor(nextDestination);
                    }
                    else if (Direction == Direction.Down)
                    {
                        nextDestination = Passengers.Min(passenger => passenger.DestinationFloor);
                        if (passengersToDropOff.Count > 0)
                        {
                            DisplayElevatorStatus("drop"); // Display elevator status after drop-off                       
                        }
                        MoveToFloor(nextDestination);
                    }
                }
                else
                {
                    if (passengersToDropOff.Any()) // probably the last dropped passenger
                    {
                        int nextDestination = passengersToDropOff.Min(passenger => passenger.DestinationFloor);
                        DisplayElevatorStatus("drop"); // Display elevator status after drop-off      
                        MoveToFloor(nextDestination);
                    }
                    Direction = Direction.None;
                    MovementStatus = ElevatorMovementStatus.AtFloor;
                }

            }
        }
        public void DisplayElevatorStatus(string moveOrDrop)
        {
            switch (moveOrDrop)
            {
                case "move":
                    Console.WriteLine($" << Elevator {ID} < Going: {Direction}< Status :{MovementStatus} < Floor: {CurrentFloor} < Passengers: {CurrentCapacity}/ {MaxCapacity} Dropping at Floors: {string.Join(", ", Passengers.Select(passenger => passenger.DestinationFloor))}");
                    break;
                case "drop":
                    Console.WriteLine($" == Elevator {ID} < Dropping Passengers at Floor: {CurrentFloor}");

                    break;
                default: break;
            }

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
                MovementStatus = ElevatorMovementStatus.AtFloor;
            }
            else if (Direction == Direction.Up)
            {
                MovementStatus = ElevatorMovementStatus.Moving;
            }
            else if (Direction == Direction.Down)
            {
                MovementStatus = ElevatorMovementStatus.Moving;
            }
        }
    }
}
