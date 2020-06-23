using LocationFinderLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationFinderLibrary.Models
{
    public class Longitude
    {
        public double LongitudeValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Allows you to construct a longitude with a decimal longitude.
        /// A negative value represents longitudes west of the prime meridian.
        /// Ex: 40.7689, -21.444
        /// </summary>
        /// <param name="longitude">The decimal value of the longitude.</param>
        public Longitude(double longitude)
        {
            LongitudeValue = longitude;
        }

        /// <summary>
        /// Constructor for longitudes with the format of degrees, minutes, seconds, direction
        /// </summary>
        /// <param name="degrees">The degrees portion of the coordinate</param>
        /// <param name="minute">The minutes portion of the coordiate</param>
        /// <param name="seconds">The seconds portion of the coordinate</param>
        /// <param name="direction">The direction(East or West)</param>
        public Longitude(int degrees, int minutes, double seconds, Direction direction)
        {
            if (direction == Direction.North || direction == Direction.South)
            {
                throw new InvalidOperationException("Cannot create a longitude with a North or South direction");
            }

            LongitudeValue = DegreeUtilities.ConvertDegreeMinuteSecondsToDecimalDegrees(degrees, minutes, seconds, direction);
        }

        public static bool IsValidLongitude(string degreeString)
        {
            double degreeValue;
            if (double.TryParse(degreeString, out degreeValue))
            {
                return degreeValue >= -180 && degreeValue <= 180;
            }
            else
            {
                return false;
            }
        }
    }
}
