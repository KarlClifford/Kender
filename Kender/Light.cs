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
        /// The default Light constructor adds a new Light with default values to the scene.
        /// </summary>
        public Light()
        {
            this.x = DEFAULT_X_VALUE;
            this.y = DEFAULT_Y_VALUE;
            this.z = DEFAULT_Z_VALUE;
        }

        /// <summary>
        /// The custom Light constructor adds a new Light with custom properies to the scene.
        /// </summary>
        /// <param name="x">The double X value relative to the screen.</param>
        /// <param name="y">The double Y value relative to the screen.</param>
        /// <param name="z">The double Z value relative to the screen.</param>
        public Light(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
