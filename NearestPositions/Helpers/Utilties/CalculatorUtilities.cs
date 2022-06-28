using NearestPositions.BusinessLayer.Models;

namespace NearestPositions.Helpers.Utilties
{
    public class CalculatorUtilities
    {
        internal static double DistanceCalculator(Location x, Location y)
        {
            var dX = x.Latitude * (Math.PI / 180.0);
            var numX = x.Longitude * (Math.PI / 180.0);

            var dY = y.Latitude * (Math.PI / 180.0);
            var numY = y.Longitude * (Math.PI / 180.0) - numX;

            var dXY = Math.Pow(Math.Sin((dX - dY) / 2.0), 2.0) +
                     Math.Cos(dX) * Math.Cos(dY) * Math.Pow(Math.Sin(numY / 2.0), 2.0);


            var result = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(dXY), Math.Sqrt(1.0 - dXY)));

            return result;
        }

    }
}

