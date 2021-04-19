
using System.Collections.Generic;

namespace PolyMorphicBehaviourUsingInterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var encoder = new VideoEncoder();
            encoder.RegisterNotificationChannel(new MailNotificationChannel());
            encoder.RegisterNotificationChannel(new SmsNotificationChannel());
            encoder.Encode(new Video());
        }
    }

    public class Video
    {
        public string Title { get; set; }
    }
    public class Message
    {
    }

    public class VideoEncoder
    {
        private readonly IList<INotificationChannel> _notificationChannels;

        public VideoEncoder()
        {
            _notificationChannels = new List<INotificationChannel>();
        }

        public void Encode(Video video)
        {
            // Video encoding logic

            foreach (var channel in _notificationChannels)
                channel.Send(new Message()); //PolyMorphic Behaviour
        }

        public void RegisterNotificationChannel(INotificationChannel channel)
        {
            _notificationChannels.Add(channel);
        }
    }


    public interface INotificationChannel
    {
        void Send(Message message);
    }

    public class SmsNotificationChannel : INotificationChannel
    {
        public void Send(Message message)
        {
            System.Console.WriteLine("sending sms........");
        }
    }

    public class MailNotificationChannel : INotificationChannel
    {
        public void Send(Message message)
        {
            System.Console.WriteLine("sending mail........");
        }
    }
}
