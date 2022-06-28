using System.Runtime.Serialization;

namespace NearestPositions.BusinessLayer.Models
{
    /// <summary>
    /// Model for Location
    /// </summary>

    [DataContract]
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

