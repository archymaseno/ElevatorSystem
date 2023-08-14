Elevator System Console Application
This console application simulates the movement of elevators within a building. It is designed to efficiently manage elevator movement, handle passenger requests, and provide real-time status updates. The goal is to move people between floors as efficiently as possible.

Features
Elevator Status Display: The application allows you to view the status of each elevator in the building. This includes the current floor, movement direction, and the number of passengers it's carrying.

Elevator Interaction: Users can interact with the elevators by calling them to specific floors. Passengers waiting on each floor can be set, and the application will assign the nearest available elevator to serve the request.

Multiple Floors and Elevators: The system supports multiple floors and elevators. This ensures that passengers can efficiently move between different floors in the building.

Getting Started
Clone the Repository: Clone this repository to your local machine using your preferred Git client or by downloading the ZIP file.

Open in Visual Studio: Open the solution file (ElevatorSystem.sln) using Visual Studio.

Build the Solution: Build the solution to ensure all dependencies are properly restored.

Run the Application: Set the ElevatorSystem project as the startup project and run the application. The console will display a menu of options to interact with the elevator system.

Usage
Select Floor and Set Passengers:

Enter the pick-up floor number and the number of waiting passengers.
For each waiting passenger, enter their destination floor.
The system will automatically call the nearest available elevator and assign passengers to it.
Show Elevator Status:

View the current status of each elevator in the building.
Information displayed includes elevator ID, current floor, movement direction, and passenger count.
Show Floor Status:

View the status of each floor, including elevator count and the number of waiting passengers.
Exit:

Choose this option to exit the application.
Testing
Unit tests are included to ensure the reliability and correctness of the code. The tests cover critical functionalities and edge cases to verify the behavior of the elevator system.

To run the tests:

Open Test Explorer: In Visual Studio, navigate to Test > Test Explorer.

Run All Tests: Click on the "Run All" button in the Test Explorer to execute all unit tests.

Contributing
Contributions to this project are welcome! If you encounter issues, have suggestions for improvements, or want to add new features, please feel free to open an issue or submit a pull request.

License
This project is licensed under the MIT License. You can find the full license text in the LICENSE file.

This console application aims to provide an efficient and interactive simulation of elevator movement and passenger handling within a building. Please refer to the provided code and the README for further details on usage and setup.