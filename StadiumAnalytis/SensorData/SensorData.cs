namespace StadiumAnalytics
{
    /// <summary>
    /// Data Model for Sensor Event
    /// </summary>
    public class SensorData
    {
        public int Id { get; set; }
        public string GateName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EventType { get; set; }
        public int NumOfPeople { get; set; }
    }
    /// <summary>
    /// Sensor event agruments 
    /// </summary>
    public class SensorEventArgs : EventArgs
    {
        public SensorData SensorData { get; }

        public SensorEventArgs(SensorData sensorData)
        {
            SensorData = sensorData;
        }
    }
}
