global using ElevatorSystem;
global using ElevatorSystem.Services;
global using ElevatorSystem.Abstracts.Interfaces;
global using ElevatorSystem.Data.Enums;
global using ElevatorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            int numberOfElevators = 3;
            int maxElevatorCapacity = 5;
            int numberOfFloors = 10;
            IBuilding building = new Building(numberOfFloors, numberOfElevators, maxElevatorCapacity);
            ElevatorController controller = new ElevatorController(building);
            controller.Run();


        }
    }
}

