global using ElevatorSystem;
global using ElevatorSystem.Service;
global using ElevatorSystem.Entities;
global using ElevatorSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using ElevatorSystem.Interfaces;

namespace ElevatorSystem
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            int elevatorCount = 2;
            int elevatorCapacity = 5;
            int floorCount = 10;
            ElevatorController controller = new ElevatorController(elevatorCount, elevatorCapacity, floorCount);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Select Floor and Set Passengers");
                Console.WriteLine("2. Show Elevator Status");
                Console.WriteLine("3. Show Floor Status");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("    Enter the requesting floor number: ");
                        int requestingFloor = int.Parse(Console.ReadLine());

                        List<int> destinations = new List<int>();

                        while (true)
                        {
                            Console.Write("    Enter number of waiting passengers (or enter 0 to call the elevator): ");
                            int waitingPassengers = int.Parse(Console.ReadLine());

                            if (waitingPassengers == 0)
                            {
                                // Exit the loop and call the elevator
                                break;
                            }

                            for (int i = 0; i < waitingPassengers; i++)
                            {
                                Console.Write($"    Enter destination for waiting passenger {i + 1}: ");
                                int passengerDestination = int.Parse(Console.ReadLine());
                                destinations.Add(passengerDestination);
                            }

                            // Automatically call the elevator after setting all destinations
                            controller.SetWaitingPassengers(requestingFloor, destinations);
                            Console.WriteLine();
                            controller.CallElevator(requestingFloor, destinations.Count);
                            break; // Exit the loop
                        }
                        break;
                    case 2:
                        controller.ShowStatus();
                        break;
                    case 3:
                        controller.ShowFloorStatus();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("****Invalid option. Try again.***");
                        break;
                }
            }

        }
    }
}