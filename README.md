## Getting Started

To run the Stadium Analytics application locally, follow these steps:

1. Download the project files to your local machine.
2. Navigate to the project directory using your preferred file explorer or command line.
3. Install dependencies:

    ```bash
    dotnet restore
    ```

4. Execute the SQL create file SensorDataDB.sql to create the necessary database tables. You can use a SQL Server management tool (e.g., SSMS) or the command-line tool to execute the SQL script.

5. Configure the database connection:

    - Open the `appsettings.json` file.
    - Update the `ConnectionStrings` section with your SQL Server connection string.

6. Run the console application SensorEventTracker to add the sensor events to SQL DB. Please follow the console help. It has publisher & subscriber implementation to add new Sensor event to DB


7. Run StadiumAnalytics Web API application and test the Get API using Swagger UI. The API fetches the total number of people entered or exited for a given date time duration.

8. Sensor Data has common interfaces & Classes defined.
