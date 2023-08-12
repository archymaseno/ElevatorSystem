namespace ElevatorSystem.Models
{
    public class Floor
    {
        public int Number { get; }
        public List<Passenger> WaitingPassengers { get; } // List of waiting passengers

        public Floor(int number)
        {
            Number = number;
            WaitingPassengers = new List<Passenger>();
        }
    }
}
