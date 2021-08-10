using System;

namespace Core.Entities
{
    public class RentPrice
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public TimeSpan Time { get; set; }
        public float Value { get; set; }

        public virtual Product Product { get; set; }
    }
}
