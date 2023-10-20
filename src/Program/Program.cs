using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using System.Runtime.InteropServices;
using System.IO;
using Ucu.Poo.Twitter;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");
            FilterNegative negative = new FilterNegative();
            FilterGreyscale greyscale = new FilterGreyscale();
            PipeNull pipeLast = new PipeNull();
            PipeSerial pipe2 = new PipeSerial(negative, pipeLast);
            PipeSerial pipe1 = new PipeSerial(greyscale, pipe2);

            pipe1.Send(picture);
            pipe2.Send(picture);

            IPicture result = pipe1.Send(picture);
            provider.SavePicture(result,@"beer2.jpg");
            
            var twitter = new TwitterImage();
            string path = File.Exists(@"beer2.jpg") ? @"beer2.jpg" : @"beer2.jpg";
            Console.WriteLine(twitter.PublishToTwitter("Primer Filtro", path));   
            
            IPicture picture2 = provider.GetPicture(@"beer2.jpg");
            
            IPicture result2 = pipe2.Send(picture2);
            provider.SavePicture(result2,@"beer3.jpg");
            
            string path2 = File.Exists(@"beer3.jpg") ? @"beer3.jpg" : @"beer3.jpg";
            Console.WriteLine(twitter.PublishToTwitter("Segundo filtro", path2));  
        }
    }
}
