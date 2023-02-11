using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kender
{
    public class Vector
    {
        public double x, y, z;
        public Vector() { }
        public Vector(double i, double j, double k)
        {
            x = i;
            y = j;
            z = k;
        }

        //The magnitude calculating the length of the vecotr and take the swuare root of that.
        public double magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        // Take the magnitude and if it is not zero devide the vecot nby the length and that gives a normalised vector
        public void normalise()
        {
            double mag = magnitude();
            if (mag != 0)
            {
                x /= mag;
                y /= mag;
                z /= mag;
            }
        }

        // The dot product
        public double dot(Vector a)
        {
            return x * a.x + y * a.y + z * a.z;
        }

        // Subtract one vector from another vector
        public Vector sub(Vector a)
        {
            return new Vector(x - a.x, y - a.y, z - a.z);
        }

        // Add one vector to another vector.
        public Vector add(Vector a)
        {
            return new Vector(x + a.x, y + a.y, z + a.z);
        }

        // Vector multiplier
        public Vector mul(double d)
        {
            return new Vector(d * x, d * y, d * z);
        }
        // Debigging
        public void print()
        {
            Debug.WriteLine("x=" + x + ", y=" + y + ", z=" + z);
        }
    }
}
