using System;
using NNUG.Messages;
using NServiceBus;

namespace NNUG.ProsjektClient
{
    public class ProsjektClientApplication : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            string navn = string.Empty;
            string timer = string.Empty;

            Console.WriteLine("Legg inn prosjekt navn (tom for å avslutte):");        
            while (string.IsNullOrEmpty((navn = Console.ReadLine())) == false)
            {
                Console.WriteLine("Legg inn prosjekt timer (tom for å avslutte):");
                timer = Console.ReadLine();

                if (String.IsNullOrEmpty(timer))
                {
                    break;
                }

                Bus.Send(new ProsjektMessage { Dato = DateTime.Now, Navn = navn, Antall = timer });
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
