namespace ElevatorSystem.Services
{
    public class ElevatorController : IElevatorController
    {
        private readonly IBuilding building;
        public ElevatorController(IBuilding building)
        {
            this.building = building;
        }
        public void Run()
        {
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
                        Console.Write("  Enter the pick-up floor number: ");
                        int requestingFloor = int.Parse(Console.ReadLine());

                        List<int> destinations = new List<int>();

                        while (true)
                        {
                            Console.Write("  Enter number of waiting passengers (or enter 0 to call the elevator): ");
                            int waitingPassengers = int.Parse(Console.ReadLine());

                            if (waitingPassengers == 0)
                            {
                                // Exit the loop and call the elevator
                                break;
                            }

                            for (int i = 0; i < waitingPassengers; i++)
                            {
                                int passengerDestination;

                                while (true)
                                {
                                    Console.Write($"  Enter destination for waiting passenger {i + 1}: ");
                                    passengerDestination = int.Parse(Console.ReadLine());

                                    if (passengerDestination < 1 || passengerDestination > building.Floors.Count)
                                    {
                                        Console.WriteLine($"  Invalid destination. Please enter a valid floor number between 1 and {building.Floors.Count}.");
                                    }
                                    else
                                    {
                                        // Valid destination entered, break out of the loop
                                        break;
                                    }
                                }

                                destinations.Add(passengerDestination);
                            }

                            // Automatically call the elevator after setting all destinations--- we can allow manual calling inorder but this will not be efficient
                            building.SetWaitingPassengers(requestingFloor, destinations);
                            Console.WriteLine();
                            CallElevator(requestingFloor, destinations.Count);
                            break; // Exit the loop
                        }
                        break;
                    case 2:
                        ShowElevatorStatus();
                        break;
                    case 3:
                        building.ShowFloorStatus();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("!!  Invalid option. Try again.");
                        break;
                }
            }
        }
        public void ShowElevatorStatus()
        {
            Console.WriteLine();
            Console.WriteLine("---- --------------------------------------------------------------------------------- ----"); // Add dashes
            Console.WriteLine($"Elevator Status");
            Console.WriteLine($"----------------");
            foreach (Elevator elevator in building.Elevators)
            {
                Console.WriteLine($"Elevator {elevator.ID}  at Floor: {elevator.CurrentFloor}  Going: {elevator.Direction}  Load: {elevator.CurrentCapacity}/{elevator.MaxCapacity}");
            }

            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        public void CallElevator(int targetFloor, int passengerCount)
        {
            // find the closest, notfull, moving toward caller,-whether stoppedtodrop or moving
            Elevator bestElevator = building.FindBestElevator(targetFloor);
            if (bestElevator != null)
            {
                // Determine the direction based on the target floor and the elevator's current floor
                Direction elevatorDirection = targetFloor > bestElevator.CurrentFloor ? Direction.Up : targetFloor < bestElevator.CurrentFloor ? Direction.Down : bestElevator.Direction;

                // Call the elevator
                bestElevator.MoveToFloor(targetFloor);
                Console.WriteLine($" == Elevator {bestElevator.ID} has arrived at floor {targetFloor}");

                // Get passengers from the floor
                List<Passenger> waitingPassengers = building.Floors[targetFloor - 1].WaitingPassengers;

                // Calculate the number of passengers to add to the elevator based on capacity
                int availableCapacity = bestElevator.MaxCapacity - bestElevator.CurrentCapacity;
                int passengersToAddCount = Math.Min(availableCapacity, waitingPassengers.Count);
                List<Passenger> passengersToAdd = waitingPassengers.Take(passengersToAddCount).ToList();

                if (passengersToAdd.Count > 0)
                {
                    // Check capacity and add passengers to the elevator
                    bestElevator.AddPassengers(passengersToAdd);

                    // Remove the added passengers from the waiting list
                    foreach (Passenger passenger in passengersToAdd)
                    {
                        building.Floors[targetFloor - 1].WaitingPassengers.Remove(passenger);
                    }

                    // Automatically call the MoveToFloor method to drop passengers at their destination floors
                    // This is the only place where MoveToFloor should be called.
                    bestElevator.DropOffPassengers();
                    //bestElevator.MoveToFloor(targetFloor);

                    // Display elevator status after adding passengers
                    // bestElevator.DisplayElevatorStatus();
                }
                else
                {
                    Console.WriteLine("!! Elevator is already at maximum capacity.");
                }
            }
            else
            {
                Console.WriteLine("!! No available elevators to service the request.");
            }
        }
    }
}
