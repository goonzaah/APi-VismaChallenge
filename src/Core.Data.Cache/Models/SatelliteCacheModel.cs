using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data.Cache.Models
{
    public class SatelliteCacheModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
