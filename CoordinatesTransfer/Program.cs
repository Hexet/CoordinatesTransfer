using System;
using System.Linq;
using GeoAPI;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.Geometries;
using ProjNet.IO.CoordinateSystems;

namespace CoordinatesTransfer
{
    class Program
    {
        static double[][] CoordinateSystemTranslation(string from, string to, double[][] points)
        {
            var csFact = new CoordinateSystemFactory();
            var ctFact = new CoordinateTransformationFactory();

            var From = csFact.CreateFromWkt(from);
            var To = csFact.CreateFromWkt(to);

            var utm33 = ProjectedCoordinateSystem.WGS84_UTM(33, true);

            var trans = ctFact.CreateFromCoordinateSystems(From, To);

            double[][] tpoints = trans.MathTransform.TransformList(points).ToArray();

            return tpoints;
        }
        static void Main(string[] args)
        {
            string utm35ETRS = "PROJCS[\"ETRS89 / ETRS-TM35\",GEOGCS[\"ETRS89\",DATUM[\"D_ETRS_1989\",SPHEROID[\"GRS_1980\",6378137,298.257222101]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",0],PARAMETER[\"central_meridian\",27],PARAMETER[\"scale_factor\",0.9996],PARAMETER[\"false_easting\",500000],PARAMETER[\"false_northing\",0],UNIT[\"Meter\",1]]";
            string wgs84 = "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\", 6378137, 298.257223563,AUTHORITY[\"EPSG\", \"7030\"]],AUTHORITY[\"EPSG\", \"6326\"]],PRIMEM[\"Greenwich\", 0,AUTHORITY[\"EPSG\", \"8901\"]],UNIT[\"degree\", 0.0174532925199433,AUTHORITY[\"EPSG\", \"9122\"]],AUTHORITY[\"EPSG\", \"4326\"]]";
            string pulkovo1995 = "GEOGCS[\"Pulkovo 1995\",DATUM[\"Pulkovo_1995\",SPHEROID[\"Krassowsky 1940\", 6378245, 298.3,AUTHORITY[\"EPSG\", \"7024\"]],TOWGS84[24.47, -130.89, -81.56, -0, -0, 0.13, -0.22],AUTHORITY[\"EPSG\", \"6200\"]],PRIMEM[\"Greenwich\", 0,AUTHORITY[\"EPSG\", \"8901\"]],UNIT[\"degree\", 0.0174532925199433,AUTHORITY[\"EPSG\", \"9122\"]],AUTHORITY[\"EPSG\", \"4200\"]]";
            string gsk2011 = "GEOGCS[\"GSK 2011\",DATUM[\"GSK_2011\",SPHEROID[\"GSK-2011\", 6378136.5, 298.2564151,AUTHORITY[\"EPSG\", \"7683\"]],TOWGS84[0.013, -0.092, -0.03, -0.001738, 0.003559, -0.004263, 0.0074]],PRIMEM[\"Greenwich\", 0],UNIT[\"degree\", 0.0174532925199433]]";

            double[][] points =
            {
                new[] {56, 16.0}, new[] {90.392, 67.224},
                new[] {20.78002815042, 50.25299100927 },
                new[] {29.133, 63.772}, new[] {24.111, 67.416},
                new[] {89.615, 43.567}, new[] {59.701, 39.485}
            };

            double[][] tpoints = CoordinateSystemTranslation(pulkovo1995, wgs84, points);

            Console.WriteLine("finish");

        }
    }
}
