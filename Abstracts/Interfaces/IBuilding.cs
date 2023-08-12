namespace ElevatorSystem.Abstracts.Interfaces
{
    public interface IBuilding
    {
        //Building should have 
        //List of Elevators
        //List of floors
        //and ElevatorController service

        List<Elevator> Elevators { get; }
        List<Floor> Floors { get; }
        Elevator FindBestElevator(int targetFloor);
        void ShowFloorStatus();

        void SetWaitingPassengers(int floorNumber, List<int> destinations);

    }
}
