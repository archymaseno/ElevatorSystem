﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Interfaces
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
        void DisplayElevatorStatus();
        void UpdateCapacity(int peopleCount);
        void UpdateMovementStatus();
    }
}
