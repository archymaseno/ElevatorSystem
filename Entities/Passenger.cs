namespace ElevatorSystem.Entities
{
    public class Passenger
    {
        public int DestinationFloor { get; set; }

        public Passenger(int destinationFloor)
        {
            DestinationFloor = destinationFloor;
        }
    }
}
