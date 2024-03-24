// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StadiumAnalytics;


//Get the appsettings from json configuration
var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

//Get the Sensor DB SQL connection string setting
string connectionString = config.GetConnectionString("SensorDbConnection");


var optionsBuilder = new DbContextOptionsBuilder<SensorDbContext>();
optionsBuilder.UseSqlServer(connectionString);

/*
 * 1. Create a DBContext, initilaize sensor event publisher & Subscriber and register the event handler
 * 2. create the sensor data object from the console user inputs after validation.
 * 3. Simulate the event trigger by invoking the publisher event with senor data object
 * 4. Feedback to the user until he provides right sensor data format 
*/
using (var dbContext = new SensorDbContext(optionsBuilder.Options))
{

    var sensorEventPublisher = new SensorEventPublisher();
    var sensorEventSubscriber = new SensorEventSubscriber(dbContext);

    sensorEventPublisher.SensorEvent += sensorEventSubscriber.OnSensorEvent;
    SensorData sensorData;
    Console.WriteLine("Please Enter Sensor data information for GateName, Date Timestamp {format eg: 2024-03-22 20:03:22.120), EventType : Entry\\Exit, Number of people ");


    while (!TakeSensorDataFromConsole(out sensorData))
    {
        Console.WriteLine("Please Enter Sensor data in correct Format, please retry with right values or type ctrl + C to exit");
    }

    try {
        
        sensorEventPublisher.InvokeSensorEvent(sensorData);

        Console.WriteLine("Sensor data Published successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Something went wrong during sensor event publish");
        Console.WriteLine(ex.ToString());
    }

}

Console.ReadLine();

//Take the sensor event data from console user input
bool TakeSensorDataFromConsole(out SensorData sensorData)
{
    bool parseCheck = true;
    string? gateName;
    DateTime timeStamp;
    int numOfPeople;
    string entryExit;


    Console.WriteLine("Gate Name:");
    gateName = Console.ReadLine().Trim();
    parseCheck = String.IsNullOrEmpty(gateName);

    Console.WriteLine("Time Stamp:");
    parseCheck = DateTime.TryParse(Console.ReadLine().Trim(), out timeStamp);

    Console.WriteLine("Number of people:");
    parseCheck = int.TryParse(Console.ReadLine().Trim(), out numOfPeople); ;

    Console.WriteLine("Entry or Exit:");
    entryExit = Console.ReadLine().Trim();
    parseCheck = entryExit.Equals("Entry") || entryExit.Equals("Exit");

    if (parseCheck)
    {
        sensorData = new SensorData
        {
            GateName = gateName,
            TimeStamp = timeStamp,
            EventType = entryExit,
            NumOfPeople = numOfPeople
        };
       
    }
    else
    {
        sensorData = null;
    }

    return parseCheck;

}

