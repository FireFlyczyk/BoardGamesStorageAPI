## **Board Games Storage API**
This is a small pet 🐶 project called Board Games Storage API. It provides a simple API for managing board games stored in a database. The project is built using ASP.NET Core and follows the RESTful architectural style.

## Features
The Board Games Storage API provides the following features:

+ **GetBoardGames**: Retrieves a list of all board games stored in the database.
+ **GetSingleBoardGame**: Retrieves a single board game by its unique identifier.
+ **EditBoardGame**: Updates the information of a board game.
+ **AddBoardGame**: Adds a new board game to the database.
+ **DeleteBoardGame**: Removes a board game from the database.

## Technologies Used
The project utilizes the following technologies and frameworks:

+ **ASP.NET Core**: The web framework used to build the API.
+ **Entity Framework Core**: The ORM used for database access and management.
+ **Swagger**: Provides API documentation and an interactive UI for testing.
+ **Dependency Injection**: The project uses dependency injection to manage the application's services and components. This helps decouple the code and makes it more modular and testable.
+ **Logging**: The project utilizes logging to record and handle errors or exceptions that occur during the execution of the API methods.
+ **AutoMapper**: Simplifies object mapping between DTOs and entity models.

## Project Structure

The project consists of several components:

+ **Controllers**: Contains the API endpoints and their respective actions.
+ **DTO**: Defines the Data Transfer Objects used for input and output.
+ **Data**: Contains the repository interface and its implementation for data access.
+ **Models**: Defines the entity model representing a board game.
+ **Profiles**: Contains AutoMapper profile for object mapping configuration.
+ **Program.cs:** The entry point of the application.

## How to Run the Project

To run the Board Games Storage API, follow these steps:

1. Ensure you have the .NET Core SDK installed on your machine.
2. Clone the project repository to your local machine.
3. Open a command prompt or terminal and navigate to the project's root directory.
4. Run the command dotnet run to build and run the application.
5. The API will be accessible at **https://localhost:{port}/boardgames**, where **{port}** is the specified port number.

## API Documentation

The API is self-documented using Swagger. Once the application is running, you can access the Swagger UI by navigating to https://localhost:{port}/swagger/index.html. This will provide detailed information about the available endpoints, request/response schemas, and the ability to test the API directly from the UI.

Feel free to explore and interact with the Board Games Storage API! 😄
