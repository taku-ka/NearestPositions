// See https://aka.ms/new-console-template for more information

using System.Collections.ObjectModel;
using System.Diagnostics;
using NearestPositions.BusinessLayer.Models;
using NearestPositions.Helpers.Manager;
using NearestPositions.Helpers.Utilties;

namespace NearestPositions
{
    class Program
    {
        static void Main()
        {
            const string file = @"DataLayer/VehiclePositions.dat";

            //the 10 vehicles
            ObservableCollection<Location> vehicles = DataManager.LoadVechicles();

            //vehicle 2M positions from binary data
            ObservableCollection<Position> positions = DataManager.ReadFile(file);

            SearchNearestNeighbour(positions, vehicles);

            Console.ReadKey();
        }

        /// <summary>
        /// Search Nearest Neighbour using KDTree
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="vehicles"></param>
        static void SearchNearestNeighbour(ObservableCollection<Position> positions, ObservableCollection<Location> vehicles)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                KdTree kdTree = new KdTree(positions.Count);
                foreach (var point in positions)
                {
                    kdTree.Add(new double[] { point.Latitude, point.Longitude });
                }

                LoggerManager.Logger($"______ KdTree {stopwatch.ElapsedMilliseconds} ms ______ \n\t Time Complexity: O(n log n)");

                stopwatch.Restart();
                for (int i = 0; i < vehicles.Count; i++)
                {
                    Node node = kdTree.FindNearest(new double[] { vehicles[i].Latitude, vehicles[i].Longitude });
                    Console.WriteLine("Latitude: {0}, Longitude: {1}  \t:\t Latitude: {2}, Longitude: {3}", vehicles[i].Latitude, vehicles[i].Longitude, node.x[0], node.x[1]);
                }

            }
            catch (Exception ex)
            {
                LoggerManager.Logger(ex.Message);
            }
            finally
            {
                stopwatch.Stop();
                LoggerManager.Logger($"______ KdTree {stopwatch.ElapsedMilliseconds} ms ______ \n\t Time Complexity: O(n log n)");
            }
        }
    }
}

