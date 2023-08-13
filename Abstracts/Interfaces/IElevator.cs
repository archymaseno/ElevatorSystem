namespace ElevatorSystem.Abstracts.Interfaces
{
    public interface IElevator
    {
        int ID { get; }
        int CurrentFloor { get; }
        Direction Direction { get; }
        int CurrentLoad { get; }
        int MaxCapacity { get; }
        int CurrentCapacity { get; }
        void AddPassengers(List<Passenger> passengers);
        void MoveToFloor(int targetFloor);
        void DisplayElevatorStatus(string moveOrDrop);
        void UpdateCapacity(int peopleCount);
        void UpdateMovementStatus();
    }
}
