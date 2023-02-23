using System;

namespace Kender

{
    /// <summary>
    /// This class represents a simple 8-bit colour to be used
    /// by CustomBitmap <see cref="CustomBitmap"/>.
    ///
    /// </summary>
    public struct CustomColour
    {
        /// <summary>
        /// Stores the 8-bit red value.
        /// </summary>
        public readonly byte redColour;
        /// <summary>
        /// Stores the 8-bit green value.
        /// </summary>
        public readonly byte greenColour;
        /// <summary>
        /// Store the 8-bit blue value.
        /// </summary>
        public readonly byte blueColour;

        public CustomColour(byte r, byte g, byte b)
        {
            (redColour, greenColour, blueColour) = (r, g, b);
        }
    }
}
