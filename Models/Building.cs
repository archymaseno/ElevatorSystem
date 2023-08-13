using ElevatorSystem.Abstracts.Interfaces;

namespace ElevatorSystem.Models
{
    public class Building : IBuilding
    {
        public List<Elevator> Elevators { get; }

        public List<Floor> Floors { get; }

        public Building(int numberOfFloors, int numberOfElevators, int maxCapacity)
        {
            Elevators = new List<Elevator>();
            Floors = new List<Floor>();
            for (int i = 1; i <= numberOfFloors; i++)
            {
                Floors.Add(new Floor(i)); ;
            }
            for (int i = 1; i <= numberOfElevators; i++)
            {
                Elevators.Add(new Elevator(i, maxCapacity, numberOfFloors));
            }
        }

        public Elevator FindBestElevator(int targetFloor)
        {

            // We check for all available elevators where we check the carrying to maxcapacity  
            List<Elevator> NotFullElevators = Elevators.Where(elevator => elevator.NotFull(targetFloor)).ToList();

            if (NotFullElevators.Count == 0)
            {
                return null; // No available elevators
            }

            Elevator? bestElevator = null;
            Elevator? alternativeElevator = null;
            int shortestDistance = int.MaxValue; // Initialize with a large value


            foreach (Elevator elevator in NotFullElevators)  //lets get them based on how close they are to the calling flooor
            {
                int distance = Math.Abs(elevator.CurrentFloor - targetFloor);

                //The elevator should closest the be headed towards the target floor as option 1 or stopped as option2 otherwise we have to just pick the inbound which is not close
                if ((distance < shortestDistance) && ((elevator.IsInbound(targetFloor)) || (elevator.Direction == Direction.None))) //we get the best elevator based on direction and distance
                {
                    bestElevator = elevator;
                    shortestDistance = distance;
                }
                else //We miseed one evaluators,we need to get second best based on distance either inbound or stopped
                {
                    if ((distance < shortestDistance) & (elevator.Direction == Direction.None))//It is far but so far is the only available lift
                    {
                        bestElevator = elevator;
                    }
                }

            }

            return bestElevator;
        }

        public void SetWaitingPassengers(int floorNumber, List<int> destinations)
        {
            if (floorNumber < 1 || floorNumber > Floors.Count)
            {
                Console.WriteLine("!!  Invalid floor number.  ");
                return;
            }

            Floor floor = Floors[floorNumber - 1]; // Get the specific floor

            floor.WaitingPassengers.Clear(); // Clear existing waiting passengers

            foreach (int destination in destinations)
            {
                floor.WaitingPassengers.Add(new Passenger(destination));
            }
            Console.WriteLine();
            Console.WriteLine($" > Setting {destinations.Count} waiting passengers on floor {floorNumber}");
            Console.WriteLine();
        }

        public void ShowFloorStatus()
        {
            Console.WriteLine();
            Console.WriteLine($"Floor Status");
            Console.WriteLine($"----------------");
            foreach (Floor floor in Floors)
            {
                Console.WriteLine($" > Floor: {floor.Number}  Elevators: {GetElevatorCountOnFloor(floor.Number)}  Waiting Passengers: {floor.WaitingPassengers.Count}");
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        private int GetElevatorCountOnFloor(int floorNumber)
        {
            return Elevators.Count(elevator => elevator.CurrentFloor == floorNumber);
        }
    }
}
