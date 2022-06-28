using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using NearestPositions.BusinessLayer.Models;

namespace NearestPositions.Helpers.Manager
{
    public class DataManager
    {
        /// <summary>
        /// Load all vechicle positions from binary data
        /// </summary>
        /// <param name="file">binary file path</param>
        /// <returns></returns>
        internal static ObservableCollection<Position> ReadFile(string file)
        {
            ObservableCollection<Position> positions = new ObservableCollection<Position>();

            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.Default))
                {
                    // Reading character by character while you can read
                    while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                    {
                        try
                        {
                            var position = new Position
                            {
                                PositionId = binaryReader.ReadInt32(),
                                VehicleRegistraton = Encoding.ASCII.GetString(binaryReader.ReadBytes(10)), //.Substring(0,9),
                                Latitude = binaryReader.ReadSingle(),
                                Longitude = binaryReader.ReadSingle(),
                                RecordedTimeUTC = binaryReader.ReadUInt64(),
                            };

                            positions.Add(position);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                }
            }

            return positions;
        }


        /// <summary>
        /// The 10 vehicles
        /// </summary>
        /// <returns>List of the 10 vehicles</returns>
        internal static ObservableCollection<Location> LoadVechicles()
        {
            ObservableCollection<Location> vehicles = new ObservableCollection<Location>();
            vehicles.Add(new Location { Latitude = 34.544909, Longitude = -102.100843 });
            vehicles.Add(new Location { Latitude = 32.345544, Longitude = -99.123124 });
            vehicles.Add(new Location { Latitude = 33.234235, Longitude = -100.214124 });
            vehicles.Add(new Location { Latitude = 35.195739, Longitude = -95.348899 });
            vehicles.Add(new Location { Latitude = 31.895839, Longitude = -97.789573 });
            vehicles.Add(new Location { Latitude = 32.895839, Longitude = -101.789573 });
            vehicles.Add(new Location { Latitude = 34.115839, Longitude = -100.225732 });
            vehicles.Add(new Location { Latitude = 32.335839, Longitude = -99.992232 });
            vehicles.Add(new Location { Latitude = 33.535339, Longitude = -94.792232 });
            vehicles.Add(new Location { Latitude = 32.234235, Longitude = -100.222222 });

            return vehicles;
        }

    }
}

