using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulering
{

    class Triangulate
    {

        //Calculates the distance between the two routers. We can have this value predetermined, but it's sexier to do it dynamically.
        //This value is a length, so it cannot be negative.
        private static double CalculateDistanceBetweenRouters(Circle Router1, Circle Router2)
        {
            double Distance = (Math.Sqrt(Math.Pow(Router1.Centre.x - Router2.Centre.x, 2)) + Math.Pow(Router1.Centre.y - Router2.Centre.y, 2));

            if (Distance < 0)
            {
                Distance = Distance * (-1);
            }
            return (Distance);
        }

        //Calculates 'a', the distance from the Router1's coordinates to the line between the two possible positions for the signal source.
        //Note: We don't know the possible positions yet. However, we do know where the line between the two points is.
        //This value is a length, so it cannot be negative.
        private static double CalculateA(Circle Router1, Circle Router2, double DistanceBetweenRouters)
        {
            double a = ((Math.Pow(Router1.Radius, 2) - Math.Pow(Router2.Radius, 2) + (Math.Pow(DistanceBetweenRouters, 2))) / (2 * DistanceBetweenRouters));
            if (a < 0)
            {
                a = a * (-1);
            }
            return (a);
        }

        //Calculates 'h', the distance between where 'a' meets the line going between the possible positions, and 
        //one (or both, h is identical in both cases) of the possible positions.
        //This value is a length, so it cannot be negative.
        private static double CalculateH(Circle Router1, Circle Router2, double a)
        {
            double HTemp = (Math.Pow(Router1.Radius, 2) - Math.Pow(a, 2));

            if (HTemp < 0)
            {
                HTemp = HTemp * (-1);
            }

            return (Math.Sqrt(HTemp));
        }

        //Calculates 'P2', the set of coordinates that makes up the point where 'a' meets the line going between the two possible positions.
        private static Coords CalculateP2(Circle Router1, Circle Router2, double DistanceBetweenRouters, double a)
        {
            Coords P2 = new Coords();
            P2.x = a * (Router2.Centre.x - Router1.Centre.x) / DistanceBetweenRouters;
            P2.y = a * (Router2.Centre.y - Router1.Centre.y) / DistanceBetweenRouters;
            return P2;
        }

        //Calculates the two set of coordinates that make up the two possible positions of the source signal
        //In this case, it's where the two radius's endpoints meet.
        private static Coords[] CalculatePositionOfSource(Circle Router1, Circle Router2, Coords P2, double DistanceBetweenRouters, double h)
        {
            Coords PositionOfSource1 = new Coords();
            Coords PositionOfSource2 = new Coords();
            Coords[] PositionsOfSource = new Coords[2];
            PositionsOfSource[0] = PositionOfSource1;
            PositionsOfSource[1] = PositionOfSource2;


            PositionOfSource1.x = P2.x + h * (Router2.Centre.y - Router1.Centre.y) / DistanceBetweenRouters;
            PositionOfSource1.y = P2.y - h * (Router2.Centre.x - Router1.Centre.x) / DistanceBetweenRouters;

            PositionOfSource2.x = P2.x - h * (Router2.Centre.y - Router1.Centre.y) / DistanceBetweenRouters;
            PositionOfSource2.y = P2.y + h * (Router2.Centre.x - Router1.Centre.x) / DistanceBetweenRouters;

            return PositionsOfSource;
        }

        //Collection of function calls. Creates all variables needed and calls all the functions to determine the possible positions
        //of the source signal.
        public static Coords[] TriangulateSignalSource(Circle Router1, Circle Router2)
        {
            //Calculates all variables needed for triangulation (Distance between routers, a, h and P2)
            double DistanceBetweenRouters = CalculateDistanceBetweenRouters(Router1, Router2);
            double a = CalculateA(Router1, Router2, DistanceBetweenRouters);
            double h = CalculateH(Router1, Router2, a);
            Coords P2 = CalculateP2(Router1, Router2, DistanceBetweenRouters, a);

            Coords[] PositionsOfSignalSource = new Coords[2];
            PositionsOfSignalSource = CalculatePositionOfSource(Router1, Router2, P2, DistanceBetweenRouters, h);

            //For debugging only****************************************
            PrintEverythingForDebug(DistanceBetweenRouters, a, h, P2);
            //For debugging only****************************************

            return PositionsOfSignalSource;
        }

        private static void PrintEverythingForDebug(double DistanceBetweenRouters, double a, double h, Coords P2)
        {
            Console.WriteLine($"Distance between the routers was calculated to: {Math.Round(DistanceBetweenRouters,3)}");
            Console.WriteLine($"a was calculated to: {Math.Round(a,3)}");
            Console.WriteLine($"h was calculated to: {Math.Round(h,3)}");
            Console.WriteLine($"Coordinates of P2 were calculated to: ({Math.Round(P2.x,3)},{Math.Round(P2.y,3)})");
        }
    }
}
