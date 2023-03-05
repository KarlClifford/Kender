using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Radios;

namespace Kender
{
    /// <summary>
    /// This class represents a ray object used for ray intersection.
    /// </summary>
    class Ray
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
        /// The default X direction.
        /// </summary>
        private const double DEFAULT_X_DIRECTION = 0;
        /// <summary>
        /// The default Y direction.
        /// </summary>
        private const double DEFAULT_Y_DIRECTION = 0;
        /// <summary>
        /// The default Z direction.
        /// </summary>
        private const double DEFAULT_Z_DIRECTION = 1;

        /// <summary>
        /// The origin of the ray.
        /// </summary>
        public Vector origin;
        /// <summary>
        /// The direction of the ray.
        /// </summary>
        public Vector direction;

        /// <summary>
        /// The default Ray constructor adds a new Ray with default value.
        /// </summary>
        public Ray()
        {
            origin = new Vector(DEFAULT_X_VALUE, DEFAULT_Y_VALUE, DEFAULT_Z_VALUE);
            direction = new Vector(DEFAULT_X_DIRECTION, DEFAULT_Y_DIRECTION, DEFAULT_Z_DIRECTION);
        }

        /// <summary>
        /// The custom ray constructor adds a new ray with custom properies.
        /// </summary>
        /// <param name="Cx">The origin X value of the ray.</param>
        /// <param name="Cy">The origin Y value of the ray.</param>
        /// <param name="Cz">The origin Z value of the ray.</param>
        /// <param name="Dx">The X direction of the ray.</param>
        /// <param name="Dy">The Y direction of the ray.</param>
        /// <param name="Dz">The Z direction of the ray.</param>
        public Ray(double Cx, double Cy, double Cz, double Dx, double Dy, double Dz)
        {
            origin = new Vector(Cx, Cy, Cz);
            direction = new Vector(Dx, Dy, Dz);
        }
    }
}
