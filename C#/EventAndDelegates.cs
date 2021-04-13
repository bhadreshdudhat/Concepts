using System;

namespace EventAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var video = new Video() { Title="bbbbb1"};
            var videoEncoder = new VideoEncoder();//publisher
            var mailService1 = new MailService1();//subscriber 1
            var mailService2 = new MailService2();//subscriber 2
            //var mailService3 = new MailService3();//subscriber 3 can be easily added by just adding new class

            videoEncoder.VideoEncoded += mailService1.OnVideoEncoded;// publisher & subscriber signature must match
            videoEncoder.VideoEncoded += mailService2.OnVideoEncoded;// publisher & subscriber signature must match
            videoEncoder.Encode(video);

            Console.WriteLine("Done");
            Console.ReadLine();

        }
    }

    public class Video
    {
        public string Title { get; set; }
    }

    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    public class VideoEncoder
    {
        //public delegate void VEEventHandler(object source, VideoEventArgs args);
        //public event VEEventHandler VideoEncoded_;
        public event EventHandler<VideoEventArgs> VideoEncoded;

        public void Encode(Video v)
        {
            Console.WriteLine("video encoding ......");


            OnVideoEncoded(v);// on completing event should be called
        }

        protected virtual void OnVideoEncoded(Video v)
        {
            if (VideoEncoded != null)
            {
                VideoEncoded(this, new VideoEventArgs() { Video = v });
            }
        }
    }

    public class MailService1
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("Mail sent...." + e.Video.Title);
        }
    }

    public class MailService2
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("Mail sent  2...." + e.Video.Title);
        }
    }
}
