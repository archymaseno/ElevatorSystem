namespace ElevatorSystem.Models
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
