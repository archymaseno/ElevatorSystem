using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Interfaces
{
    public interface IElevatorController
    {
        void ShowStatus();
        void ShowFloorStatus();
        void CallElevator(int targetFloor, int passengerCount);
        Elevator FindBestElevator(int targetFloor);
        void SetWaitingPassengers(int floorNumber, List<int> destinations);
    }
}
