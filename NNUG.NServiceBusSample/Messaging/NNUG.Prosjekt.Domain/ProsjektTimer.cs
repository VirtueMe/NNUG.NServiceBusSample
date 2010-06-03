namespace NNUG.Prosjekt.Domain
{
    using System;

    public class ProsjektTimer
    {
        public string Id { get; set; }
        public DateTime Dato { get; set; }
        public string Prosjekt { get; set; }
        public string Timer { get; set; }
    }
}
