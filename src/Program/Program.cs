using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using System.Runtime.InteropServices;

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
            
            IPicture result2 = pipe2.Send(picture);
            provider.SavePicture(result2,@"beer3.jpg");
            
        }
    }
}
