using System;
using System.Runtime.Serialization;

namespace NearestPositions.BusinessLayer.Models
{
    /// <summary>
    /// Model for Position
    /// </summary>

    [DataContract]
    public class Position
    {
        public int? PositionId { get; set; } //Int32
        public string? VehicleRegistraton { get; set; } //Null Teminated ASCII String
        public float Latitude { get; set; } //Float(4 byte floating-point number)
        public float Longitude { get; set; } //Float(4 byte floating-point number)
        public UInt64 RecordedTimeUTC { get; set; } //	 UInt64(number of seconds since Epoch)
    }
}

