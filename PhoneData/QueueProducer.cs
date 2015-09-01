﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Sensors;
using Windows.Devices.Geolocation;

using System.Diagnostics;

namespace PhoneData
{
    public class QueueProducer
    {
        //private static readonly Lazy<QueueProducer> lazy = new Lazy<QueueProducer>(() => new QueueProducer());

        //public static QueueProducer Instance { get { return lazy.Value; } }

        private EventQueue _queue;

        private DataSubscription _dataSubscription;

        public QueueProducer(EventQueue queue)
        {
            _dataSubscription = new DataSubscription();
            _queue = queue;
        }

        public void SubscribeAndProduce()
        {
            _dataSubscription.SubscribeToAccelerometer(OnAccelerometerReading);
            _dataSubscription.SubscribeToGeolocation(OnGeopositionReading);
        }

        private void OnAccelerometerReading(AccelerometerReadingChangedEventArgs args)
        {
            AccelerometerReading reading = args.Reading;
            DateTimeOffset ts = reading.Timestamp;
            EventItem accX = new EventItem { StreamName = "accX", Timestamp = ts, Value = reading.AccelerationX };
            EventItem accY = new EventItem { StreamName = "accY", Timestamp = ts, Value = reading.AccelerationY };
            EventItem accZ = new EventItem { StreamName = "accZ", Timestamp = ts, Value = reading.AccelerationZ };

            IList<EventItem> events = new[] { accX, accY, accZ };
            Debug.WriteLine(DateTime.Now);
            SendToQueue(events);
        }

        public void OnGeopositionReading(PositionChangedEventArgs args)
        {
            Geoposition geo = args.Position;

            double lat = geo.Coordinate.Point.Position.Latitude;
            double lng = geo.Coordinate.Point.Position.Longitude;

            DateTimeOffset ts = geo.Coordinate.Timestamp;

            EventItem latEvent = new EventItem { StreamName = "lat", Timestamp = ts, Value = lat };
            EventItem lngEvent = new EventItem { StreamName = "long", Timestamp = ts, Value = lng };

            IList<EventItem> events = new[] { latEvent, lngEvent };

            SendToQueue(events);
        }

        private void SendToQueue(IList<EventItem> events)
        {
            foreach (EventItem eventItem in events)
            {
                _queue.Add(eventItem);
            }
        }

    }
}
