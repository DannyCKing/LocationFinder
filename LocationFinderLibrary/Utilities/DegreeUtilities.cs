using LocationFinderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationFinderLibrary.Utilities
{
    public class DegreeUtilities
    {
        /// <summary>
        ///  This function will convert a degrees in the format of degrees, minutes and seconds into a decimal
        ///  degree format.
        ///  
        /// This function allows for a integer value for the degree and minute portions and a double for the seconds portion.
        /// </summary>
        /// <param name="degrees">The degrees portion of the coordinate</param>
        /// <param name="minute">The minutes portion of the coordiate</param>
        /// <param name="seconds">The seconds portion of the coordinate</param>
        /// <returns>A double respresenting the latitude or longitude value in decimal format</returns>
        public static double ConvertDegreeMinuteSecondsToDecimalDegrees(int degrees, int minute, double seconds, Direction direction)
        {
            // add degree portion
            double result = degrees;

            // add minute portion
            result += minute / 60D; // added D to avoid integer division

            // add second portion
            result += seconds / (60 * 60);

            // check for direction
            if (direction == Direction.South || direction == Direction.West)
            {
                // make result negative for south latitude and west longitude
                result = result * -1;
            }

            return result;
        }

        /// <summary>
        /// This purpose of this function is to get the approximate elevation of the surface of the earth at a particular latitude
        /// because the earth is a spheroid.  
        /// </summary>
        /// <param name="latitude">The latitude of the location</param>
        /// <returns>A int representing the radius of the earth in feet at a given location</returns>
        public static double GetElevationAtLocation(double latitude)
        {
            double difference = Constants.RADIUS_OF_EARTH_IN_FEET_AT_EQUATOR - Constants.RADIUS_OF_EARTH_IN_FEET_AT_POLES;

            double degreesFromPole = Constants.MAX_LATITUDE - latitude;

            // this returns a double 0 to 1 where 0 means it is one of the poles and 1 is at the equator
            double locationToPole = degreesFromPole / Constants.MAX_LATITUDE;

            double feetAboveRadiusAtPole = locationToPole * difference;

            double result = Constants.RADIUS_OF_EARTH_IN_FEET_AT_POLES + feetAboveRadiusAtPole;

            return result;
        }

        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double ConvertToDegrees(double angle)
        {
            return angle * (180 / Math.PI);
        }

        public static double Cosine(double angle)
        {
            var radians = ConvertToRadians(angle);
            var result = Math.Cos(radians);
            return result;
        }

        public static double Sin(double angle)
        {
            var radians = ConvertToRadians(angle);
            var result = Math.Sin(radians);
            return result;
        }

        /// <summary>
        /// This method's goal is to get the distance only from within the view of the XY plane, and the Z plane will be ignored
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double GetXYDistanceDifference(GeographicLocation point1, GeographicLocation point2)
        {
            var X_Difference = point1.X - point2.X;
            var Y_Difference = point1.Y - point2.Y;

            return Math.Sqrt(Math.Pow(X_Difference, 2) + Math.Pow(Y_Difference, 2));
        }

        /// <summary>
        /// This method will give your the absolute distance between two cartesian points
        /// </summary>
        /// <param name="point1">Point 1</param>
        /// <param name="point2">Point 2</param>
        /// <returns></returns>
        internal static double GetXYZDistance(GeographicLocation point1, GeographicLocation point2)
        {
            var X_Difference = point1.X - point2.X;
            var Y_Difference = point1.Y - point2.Y;
            var Z_Difference = point1.Z - point2.Z;

            return Math.Sqrt(Math.Pow(X_Difference, 2) + Math.Pow(Y_Difference, 2) + Math.Pow(Z_Difference, 2));
        }
    }
}
