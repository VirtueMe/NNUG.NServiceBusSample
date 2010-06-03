namespace NNUG.Messages
{
    using System;

    using NServiceBus;

    public class ProsjektMessage : IMessage
    {
        public string Navn { get; set; }
        public string Antall { get; set; }
        public DateTime Dato { get; set; }
    }
}
