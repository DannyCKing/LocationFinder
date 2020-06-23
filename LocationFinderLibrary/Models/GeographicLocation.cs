using LocationFinderLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace LocationFinderLibrary.Models
{
    /// <summary>
    /// Geographic Location meant to store the Latitude and Longitude and elevation of a point
    /// </summary>
    public class GeographicLocation
    {
        public string Name
        {
            get;
            private set;
        }

        public Latitude Latitude
        {
            get;
            private set;
        }

        public Longitude Longitude
        {
            get;
            private set;
        }

        public double Elevation
        {
            get;
            private set;
        }

        public double X
        {
            get;
            private set;
        }

        public double Y
        {
            get;
            private set;
        }

        public double Z
        {
            get;
            private set;
        }

        public double BearingToReferencePoint
        {
            get;
            private set;
        }

        public double VeritcalOrientationToReferencePoint
        {
            get;
            private set;
        }

        public double DistanceToReferencePoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor to create a geographic location with a given elevation.
        /// </summary>
        /// <param name="name">The name of your location</param>
        /// <param name="latitude">The latitude of your location</param>
        /// <param name="longitude">The longitude of your location</param>
        /// <param name="elevationInFeet">The elevation of your location</param>
        public GeographicLocation(string name, double latitude, double longitude, double elevationInFeet, GeographicLocation referencePoint)
        {
            Name = name;
            Latitude = new Latitude(latitude);
            Longitude = new Longitude(longitude);
            Elevation = elevationInFeet;
            SetXYZ();
            SetReferencePointLocation(referencePoint);
        }

        /// <summary>
        /// This method will translation latitude, longitude and a given elevation into cartesian coordinates
        /// </summary>
        private void SetXYZ()
        {
            var earthRadius = DegreeUtilities.GetElevationAtLocation(Latitude.LatitudeValue);
            var totalRadius = earthRadius + Elevation;

            /// calculation based on https://stackoverflow.com/a/1185413/1466748
            X = totalRadius * DegreeUtilities.Cosine(Latitude.LatitudeValue) * DegreeUtilities.Cosine(Longitude.LongitudeValue);
            Y = totalRadius * DegreeUtilities.Cosine(Latitude.LatitudeValue) * DegreeUtilities.Sin(Longitude.LongitudeValue);
            Z = totalRadius * DegreeUtilities.Sin(Latitude.LatitudeValue);
        }

        /// <summary>
        /// This will set a reference point to compare the GeographicLocation to.
        /// </summary>
        /// <param name="startingPoint">This will set a point to compares the current geograhpic point to.</param>
        public void SetReferencePointLocation(GeographicLocation startingPoint)
        {
            if(startingPoint == null)
            {
                return;
            }

            // calculate bearing, from: https://www.movable-type.co.uk/scripts/latlong.html
            var lat2 = DegreeUtilities.ConvertToRadians(Latitude.LatitudeValue);
            var lat1 = DegreeUtilities.ConvertToRadians(startingPoint.Latitude.LatitudeValue);
            var long2 = DegreeUtilities.ConvertToRadians(Longitude.LongitudeValue);
            var long1 = DegreeUtilities.ConvertToRadians(startingPoint.Longitude.LongitudeValue);
            var y1 = Math.Sin(long2 - long1) * Math.Cos(lat2);
            var x1 = Math.Cos(lat1) * Math.Sin(lat2) -
                    Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1);
            var theta = Math.Atan2(y1, x1);
            var bearingInDegrees = DegreeUtilities.ConvertToDegrees(theta);
            bearingInDegrees = (bearingInDegrees + 360) % 360;// in degrees
            BearingToReferencePoint =  bearingInDegrees;

            // to find the angle from the ground we need to create a plane tangential to our location 
            // and project the other point on to the plane we created
            // the angle between between that other one and the project point is our angle
            // https://math.stackexchange.com/questions/100761/how-do-i-find-the-projection-of-a-point-onto-a-plane
            var a = startingPoint.X;
            var x = startingPoint.X;
            var b = startingPoint.Y;
            var y = startingPoint.Y;
            var c = startingPoint.Z;
            var z = startingPoint.Z;
            var d = X;
            var e = Y;
            var f = Z;
            var t = ((a * d) - (a * x) + (b * e) - (b * y) + (c * f) - (c * z)) / (Math.Pow(a, 2) + Math.Pow(b, 2) + Math.Pow(c, 2));

            var projectedPointX = X + (t * a);
            var projectedPointY = Y + (t * b);
            var projectedPointZ = Z + (t * c);

            var vector1_X = (X - startingPoint.X );
            var vector1_Y = (Y - startingPoint.Y );
            var vector1_Z = (Z - startingPoint.Z );

            var vector2_X = (X - projectedPointX );
            var vector2_Y = (Y - projectedPointY );
            var vector2_Z = (Z - projectedPointZ );

            var angleBetween = Vector3D.AngleBetween(new Vector3D(vector1_X, vector1_Y, vector1_Z) , new Vector3D(vector2_X, vector2_Y, vector2_Z));
            VeritcalOrientationToReferencePoint = (180 - angleBetween) - 90;// DegreeUtilities.ConvertToDegrees(angleBetween);

            // handle edge cases where above
            if(vector2_Z < 0)
            {
                VeritcalOrientationToReferencePoint = angleBetween - 90;
            }
            
            DistanceToReferencePoint = DegreeUtilities.GetXYZDistance(this, startingPoint);
        }


    }
}
