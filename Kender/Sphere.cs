﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Radios;

namespace Kender
{
    /// <summary>
    /// This class represents a sphere object that can be rendered to the screen. It inherits from the Vector class <see cref="Vector"/>.
    /// </summary>
    class Sphere : Vector
    {
        /// <summary>
        /// The default X value.
        /// </summary>
        private const double DEFAULT_X_VALUE = 0;
        /// <summary>
        /// The default Y value.
        /// </summary>
        private const double DEFAULT_Y_VALUE = 0;
        /// <summary>
        /// The default Z value.
        /// </summary>
        private const double DEFAULT_Z_VALUE = 0;
        /// <summary>
        /// The default radius value.
        /// </summary>
        private const double DEFAULT_RADIUS_VALUE = 100;
        /// <summary>
        /// The default colour.
        /// </summary>
        private readonly CustomColour DEFAULT_COLOUR = new(255, 255, 255);


        
        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        public double Radius { get; set; }
        /// <summary>
        /// The colour of the sphere.
        /// </summary>
        public CustomColour Colour { get; set; }

        /// <summary>
        /// The default sphere constructor adds a new sphere with default values to the scene.
        /// </summary>
        public Sphere()
        {
            this.x = DEFAULT_X_VALUE;
            this.y = DEFAULT_Y_VALUE;
            this.z = DEFAULT_Z_VALUE;
            Radius = DEFAULT_RADIUS_VALUE;
            Colour = DEFAULT_COLOUR;
        }

        /// <summary>
        /// The custom sphere constructor adds a new sphere with custom properies to the scene.
        /// </summary>
        /// <param name="x">The double X value relative to the screen.</param>
        /// <param name="y">The double Y value relative to the screen.</param>
        /// <param name="z">The double Z value relative to the screen.</param>
        /// <param name="radius">The double radius of the sphere.</param>
        /// <param name="colour"> A RGB colour that uses the CustomColour class <see cref="CustomColour"/>.</param>
        public Sphere(double x, double y, double z, double radius, CustomColour colour)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            Radius = radius;
            Colour = colour;
        }

        /// <summary>
        /// Calculates ray-sphere intersection.
        /// </summary>
        /// <param name="o">ray origin</param>
        /// <param name="d">ray direction</param>
        /// <returns>True if the sphere has intersected the sphere at one of the two itersection points.</returns>
        public bool Intersect(Vector o, Vector d)
        {
            Vector v = new Vector(o.x - this.x, o.y - this.y, o.z - this.z);
            double a = d.dot(d);
            double b = 2 * (v.x * d.x + v.y * d.y + v.z * d.z);
            double c = v.dot(v) - this.Radius * this.Radius;

            double discriminant = b * b - 4 * a * c;

            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return t1 > 0 || t2 > 0;
        }

        /// <summary>
        /// Calculate the distance of the sphere.
        /// </summary>
        /// <returns>double distance of the sphere.</returns>
        public double Distance()
        {
            return Math.Sqrt((this.x * this.x) + (this.y * this.y) + (this.z * this.z));
        }

        /// <summary>
        /// Generates the diffuse shading of the sphere.
        /// </summary>
        /// <param name="o">The ray origin.</param>
        /// <param name="d">The ray direction.</param>
        /// <param name="Light">The light source.</param>
        /// <returns>A RGB colour with shading applied.</returns>
        public CustomColour calculateShading(Vector o, Vector d, Vector Light) {
            Vector v = new Vector(o.x - this.x, o.y - this.y, o.z - this.z);
            double a = d.dot(d);
            double b = 2 * (v.x * d.x + v.y * d.y + v.z * d.z);
            double c = v.dot(v) - this.Radius * this.Radius;

            double disc = b * b - 4 * a * c;

            double t = (-b - Math.Sqrt(disc)) / (2 * a);
            Vector p = o.add(d.mul(t));
            Vector Lv = Light.sub(p);
            Lv.normalise();
            Vector n = p.sub(this);
            n.normalise();

            double dp = Lv.dot(n);
            double col;

            if (dp < 0)
            {
                col = 0;
            }
            else
            {
                col = dp;
            }
            if (col > 1)
            {
                col = 1;
            }

            return new CustomColour((byte)(col * Colour.redColour), (byte)(col * Colour.greenColour), (byte)(col * Colour.blueColour));
        }
    }
}
