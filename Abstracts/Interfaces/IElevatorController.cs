namespace ElevatorSystem.Abstracts.Interfaces
{
    public interface IElevatorController
    {
        void ShowElevatorStatus();
        void CallElevator(int targetFloor, int passengerCount);
        void Run();
    }
}
