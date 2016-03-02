using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The circle that is defined here has the same center coordinates as the access points' 
//coordinates in a given zone. The circles radius is given by the signal strength. 
//Alternatively, this class can be called Router. However, for the sake of mathematical understanding, 
//the name is "Circle" for now.

namespace Triangulering
{
    class Circle
    {
        public double Radius;
        public Coords Centre = new Coords(); 

        public Circle(double p1, double p2)
        {
            Centre.x = p1;
            Centre.y = p2;
        }

        public Circle()
        {
            Centre.x = 0;
            Centre.y = 0;
        }

        public void SetRouterPosition(double x, double y)
        {
            Centre.x = x;
            Centre.y = y;
        }

        public void SetSignalStrength(double SignalStrength)
        {
            Radius = SignalStrength;
        }
    }
}
