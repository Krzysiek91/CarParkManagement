# CarParkManagement

A simple car park management system 

## Setup Instructions

1. Clone the repository
2. Open `CarParkManagement.sln` in Visual Studio
3. Build solution
4. Run the `CarParkManagement.Api` project (F5)
5. Swagger UI opens at `https://localhost:7175/swagger/index.html` (configurable in launchSetting.json)

## Data Storage

The API uses in-memory collection initially. It exposes interface so it can be easily replaced with real DB (e.g. SQL Server). 
Parking spaces are automatically initialized when the application starts.

## API Endpoints

- `POST /parking` - Parks vehicle on first available space
- `GET /parking` - Returns available and taken spaces
- `POST /parking/exit` - Calculate parking charge and removes vehicle from the car park

## Assumptions

- 100 spaces total.
- Allocate first available space.
- Logging out of scope 
- Vehicle registration number can only be parked once at any given time
- Logging, Auth, and persitence beyond application runtime are out of the scope for this exercise
- Only some unit tests will be added.
- Integration and other test types are out of scope
- Basic global exception handling is used (built-in ASP.NET)

## Design Decisions 

- Use in-memory ConcurrentDicionary for data storage 
- Introduce repository and service abstractions - Decoupling of concerns, easier replacement in the future 
- Chose a simple implementation taking into account recommended 2-3 hours time for completion while keeping the solution easy to extend. 



