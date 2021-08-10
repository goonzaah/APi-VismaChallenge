using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Satellite
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
