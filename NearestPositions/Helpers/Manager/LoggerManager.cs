using System;
using NearestPositions.BusinessLayer.Models;

namespace NearestPositions.Helpers.Manager
{
    public class LoggerManager
    {
        internal static void Logger(string message)
        {
            Console.WriteLine(message);
        }
    }
}

