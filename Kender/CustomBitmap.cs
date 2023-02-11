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
        private readonly int width;
        private readonly int height;
        private readonly byte[] imageBytes;

        public CustomBitmap(int width, int height)
        {
            this.width = width;
            this.height = height;
            imageBytes = new byte[width * height * 4];
        }

        public void setPixel(int x, int y, CustomColour color)
        {
            int offset = ((height - y - 1) * width + x) * 4;
            //TODO: Had to swap offset of red and blue
            imageBytes[offset + 2] = color.redColour;
            imageBytes[offset + 1] = color.greenColour;
            imageBytes[offset + 0] = color.blueColour;
        }

        public byte[] getBitmapBytes()
        {
            const int imageHeaderSize = 54;
            byte[] bmpBytes = new byte[imageBytes.Length + imageHeaderSize];
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

        public void Save(string filename)
        {
            byte[] bytes = getBitmapBytes();
            File.WriteAllBytes(filename, bytes);
        }

        public void saveAsJPEG(string filename)
        {
            byte[] bytes = getBitmapBytes();
            using MemoryStream ms = new(bytes);
            using Image img = Bitmap.FromStream(ms);
            img.Save(filename);
        }
    }
}
