namespace ElevatorSystem.Abstracts.AbstractClasses
{
    public abstract class ElevatorBase
    {
        //Fields  and //Properties
        public int ID { get; protected set; }
        public int CurrentCapacity { get; protected set; }
        public int CurrentFloor { get; protected set; }
        public Direction Direction { get; protected set; }
        public int MaxCapacity { get; protected set; }
        public List<Passenger> Passengers { get; protected set; }
       public  ElevatorMovementStatus MovementStatus { get; protected set; }

        //methods
        public abstract void MoveToFloor(int targetFloor);
        public void AddPassengers(List<Passenger> passengers)
        {
            if (passengers == null)
            {
                throw new ArgumentNullException(nameof(passengers));
            }
            if (CurrentCapacity + passengers.Count > MaxCapacity)
            {
                throw new ElevatorCapcityExceptions("Elevator is at maximum capacity. Cannot add more passengers.");
            }
            CurrentCapacity += passengers.Count;
            Passengers.AddRange(passengers);
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



    }
}