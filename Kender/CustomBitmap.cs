using System;
using System.Drawing;
using System.IO;
using Image = System.Drawing.Image;

namespace Kender
{

    /// <summary>
    /// This class represents a simple bitmap and makes use of
    /// the customColour class <see cref="CustomColour"/>.
    /// </summary>
    public class CustomBitmap
    {
        // Width of bitmap.
        private readonly int width;
        // Height of bitmap.
        private readonly int height;
        // bytes of bitmap.
        private readonly byte[] imageBytes;

        public CustomBitmap(int width, int height)
        {
            this.width = width;
            this.height = height;
            imageBytes = new byte[width * height * 4];
        }

        /// <summary>
        /// Sets a pixel to an 8-bit RGB colour. <see cref="CustomColour"/>.
        /// </summary>
        /// <param name="x">X Coordinate.</param>
        /// <param name="y">Y Coordinate.</param>
        /// <param name="colour">RGB colour to set the pixel too.</param>
        public void setPixel(int x, int y, CustomColour colour)
        {
            int offset = ((height - y - 1) * width + x) * 4;
            imageBytes[offset + 0] = colour.blueColour;
            imageBytes[offset + 1] = colour.greenColour;
            imageBytes[offset + 2] = colour.redColour;
            imageBytes[offset + 3] = 0xff;
        }

        /// <summary>
        /// Gets the stored bytes of the bitmap.
        /// </summary>
        /// <returns>The bytes of the bitmap.</returns>
        public byte[] getBitmapBytes()
        {
            const int imageHeaderSize = 54;
            byte[] bmpBytes = new byte[imageHeaderSize + imageBytes.Length];
            bmpBytes[0] = (byte)'B';
            bmpBytes[1] = (byte)'M';
            bmpBytes[14] = 40;
            Array.Copy(BitConverter.GetBytes(bmpBytes.Length), 0, bmpBytes, 2, 4);
            Array.Copy(BitConverter.GetBytes(imageHeaderSize), 0, bmpBytes, 10, 4);
            Array.Copy(BitConverter.GetBytes(width), 0, bmpBytes, 18, 4);
            Array.Copy(BitConverter.GetBytes(height), 0, bmpBytes, 22, 4);
            Array.Copy(BitConverter.GetBytes(32), 0, bmpBytes, 28, 2);
            Array.Copy(BitConverter.GetBytes(imageBytes.Length), 0, bmpBytes, 34, 4);
            Array.Copy(imageBytes, 0, bmpBytes, imageHeaderSize, imageBytes.Length);
            return bmpBytes;
        }

        public int getWidth() { return width; }
        public int getHeight() { return height; }

        /// <summary>
        /// Exports the bitmap.
        /// </summary>
        /// <param name="path">Path to export to.</param>
        public void Save(string path)
        {
            byte[] bytes = getBitmapBytes();
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Exports bitmap as JPEG
        /// </summary>
        /// <param name="path">Path to export to.</param>
        public void saveAsJPEG(string path)
        {
            byte[] bytes = getBitmapBytes();
            using MemoryStream ms = new(bytes);
            using Image img = Bitmap.FromStream(ms);
            img.Save(path);
        }
    }
}
