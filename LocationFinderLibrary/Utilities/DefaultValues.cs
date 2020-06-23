using LocationFinderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationFinderLibrary.Utilities
{
    public class DefaultValues
    {
        public static List<GeographicLocation> GetDefaultLocations(GeographicLocation referencePoint)
        {
            return new List<GeographicLocation>
            {
                new GeographicLocation("Disneyland", 33.812092,-117.918987, 0, referencePoint),
                new GeographicLocation("Disneyworld", 28.375312,-81.549428, 0, referencePoint),
                new GeographicLocation("Golden Gate Bridge", 37.819467,-122.478477, 200, referencePoint),
                new GeographicLocation("Top Eiffel Tower", 48.858278, 2.294594, 220, referencePoint),
                new GeographicLocation("Big Ben", 51.500716, -0.124562, 315, referencePoint),
                new GeographicLocation("Taj Mahal", 27.174820, 78.042131, 0, referencePoint),
                new GeographicLocation("The Pyramids Of Giza", 29.976936, 31.132564, 0, referencePoint),
                new GeographicLocation("Machu Picchu", -13.163444, -72.544941, 7972, referencePoint),
                new GeographicLocation("Mt. Everest", 27.988118, 86.925416, 29029, referencePoint),

                new GeographicLocation("Bottom Floor of COB", 40.770978, -111.889233, 0, referencePoint),
                new GeographicLocation("Top Floor of COB", 40.770978, -111.889231, 450, referencePoint),

                //new GeographicLocation("South Temple and State Street", 40.7694, -111.891, 0, referencePoint),
                new GeographicLocation("Vivint Smart Home", 40.7694, -111.901, 0, referencePoint),
                new GeographicLocation("Catherdal of the Madeline", 40.7694, -111.8815, 0, referencePoint),
                new GeographicLocation("Conference Center", 40.7733, -111.891, 0, referencePoint),
                new GeographicLocation("Little America", 40.7573, -111.891, 0, referencePoint),
                new GeographicLocation("Indian Ocean(opposite SLC)", -40.769295, 68.109033, 0, referencePoint),
                

                // These point are added to test the edge cases - if the latitude and longitude were mapped 
                // to X,Y,Z, these represent the three axises

                // these two point represent the extremes of the X axis
                new GeographicLocation("Gulf of Guinea", 0.00, 0.00, 0, referencePoint),
                new GeographicLocation("Internation Dateline and Equator", 0.00, 180.00, 0, referencePoint),

                // these two point represent the extremes of the Y axis
                new GeographicLocation("Indian Ocean", 0.00, 90, 0, referencePoint),
                new GeographicLocation("Near Galapagos Islands", 0.00, -90, 0, referencePoint),

                // The south pole to the north pole would represent the Z-axis
                new GeographicLocation("North Pole", 90, 0, 0, referencePoint),
                new GeographicLocation("South Pole", -90, 0, 9300, referencePoint),
            };
        }
    }
}
