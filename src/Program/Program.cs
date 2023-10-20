using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"src\Program\beer.jpg");
            FilterNegative negative = new FilterNegative();
            FilterGreyscale greyscale = new FilterGreyscale();
            PipeNull pipeLast = new PipeNull();
            PipeSerial pipe2 = new PipeSerial(negative, pipeLast);
            PipeSerial pipe1 = new PipeSerial(greyscale, pipe2);

            pipe1.Send(picture);
            pipe2.Send(picture);

            PictureProvider prov = new PictureProvider();
            IPicture pic = prov.SavePicture(picture, @"C:\Users\gmour\Documents\pic.png"); 
        }
    }
}
