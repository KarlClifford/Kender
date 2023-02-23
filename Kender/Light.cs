using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Radios;

namespace Kender
{
    /// <summary>
    /// This class represents a light object that can be rendered to the screen. It inherits from the Vector class <see cref="Vector"/>.
    /// </summary>
    class Light : Vector
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
        //private const double DEFAULT_RADIUS_VALUE = 100;
        /// <summary>
        /// The default colour.
        /// </summary>
        //private readonly CustomColour DEFAULT_COLOUR = new(255, 255, 255);


        
        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        //public double Radius { get; set; }
        /// <summary>
        /// The colour of the sphere.
        /// </summary>
        //public CustomColour Colour { get; set; }

        /// <summary>
        /// The default sphere constructor adds a new sphere with default values to the scene.
        /// </summary>
        public Light()
        {
            this.x = DEFAULT_X_VALUE;
            this.y = DEFAULT_Y_VALUE;
            this.z = DEFAULT_Z_VALUE;
            //Radius = DEFAULT_RADIUS_VALUE;
            //Colour = DEFAULT_COLOUR;
        }

        /// <summary>
        /// The custom sphere constructor adds a new sphere with custom properies to the scene.
        /// </summary>
        /// <param name="x">The double X value relative to the screen.</param>
        /// <param name="y">The double Y value relative to the screen.</param>
        /// <param name="z">The double Z value relative to the screen.</param>
        /// <param name="radius">The double radius of the sphere.</param>
        /// <param name="colour"> A RGB colour that uses the CustomColour class <see cref="CustomColour"/>.</param>
        public Light(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            //Radius = radius;
            //Colour = colour;
        }
    }
}
