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

            // We check for all available elevators where we check the cuarrying capacity  
            List<Elevator> NotFullElevators = Elevators.Where(elevator => elevator.NotFull(targetFloor)).ToList();

            if (NotFullElevators.Count == 0)
            {
                return null; // No available elevators
            }

            Elevator? bestElevator = null;
            int shortestDistance = int.MaxValue; // Initialize with a large value


            foreach (Elevator elevator in NotFullElevators)
            {
                int distance = Math.Abs(elevator.CurrentFloor - targetFloor);

                //The elevator should also be headed towards the target floor
                if (elevator.IsInbound(targetFloor))
                {
                    if (distance < shortestDistance)
                    {
                        bestElevator = elevator;
                        shortestDistance = distance;
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
            // Automatically call the nearest elevator after setting waiting passengers
            // CallElevator(floorNumber, destinations.Count);
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
