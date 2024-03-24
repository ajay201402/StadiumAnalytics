using System.Diagnostics;

namespace StadiumAnalytics

{

    /// <summary>
    /// Sensor Event Publisher
    /// </summary>
    public interface ISensorEventPublisher
    {
        event EventHandler<SensorEventArgs> SensorEvent;
        void InvokeSensorEvent(SensorData sensorData);
    }

    /// <summary>
    /// Sensor event Subscriber
    /// </summary>
    public interface ISensorEventSubscriber
    {
        void OnSensorEvent(object sender, SensorEventArgs e);
    }
    /// <summary>
    /// Sensor Event Publisher implementation
    /// </summary>
    public class SensorEventPublisher : ISensorEventPublisher
    {
        public event EventHandler<SensorEventArgs> SensorEvent;

        public void InvokeSensorEvent(SensorData sensorData)
        {
            SensorEvent?.Invoke(this, new SensorEventArgs(sensorData));
        }
    }
    /// <summary>
    /// Sensor Event Subscriber implementation
    /// </summary>
    public class SensorEventSubscriber : ISensorEventSubscriber
    {
        private readonly SensorDbContext _context;

        public SensorEventSubscriber(SensorDbContext context )
        {
            _context = context;
        }

        public void OnSensorEvent(object sender, SensorEventArgs e)
        {
            var sensorData = e.SensorData;
            _context.SensorData.Add(sensorData);
            _context.SaveChanges();
        }
    }
}
